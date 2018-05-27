using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace JingTum.Lib
{
    /// <summary>
    /// Main handler for backend system.
    /// </summary>
    /// <remarks>
    /// It creates a handle with jingtum, makes request to jingtum, subscribes event to jingtum, and gets info from jingtum.
    /// </remarks>
    public class Remote : IDisposable
    {
        private Server _server;
        private Cache<RequestCache> _requestsCache = new Cache<RequestCache>("RequestsCache", 1000 * 60 * 60 * 24);
        private Cache<bool> _txCache = new Cache<bool>("TransactionsCache", 1000 * 60 * 5);
        private Cache<PathFindAlternative> _pathsCache = new Cache<PathFindAlternative>("PathFindCache", 1000 * 60 * 5);
        private ServerStatus _status;

        /// <summary>
        /// Creates a new instance of <see cref="Remote"/> object.
        /// </summary>
        /// <param name="url">The jingtum websocket server url.</param>
        /// <param name="localSign">Whether sign transaction in local.</param>
        public Remote(string url, bool localSign = false)
        {
            if (url == null) throw new ArgumentNullException("url");

            Url = url.Trim();
            LocalSign = localSign;

            _server = new Server(this, Url);
            _status = new ServerStatus();
        }

        /// <summary>
        /// For unit test.
        /// </summary>
        /// <param name="server"></param>
        internal void SetMockServer(Server server)
        {
            _server = server;
        }

        /// <summary>
        /// Gets the jingtum websocket server url.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether sign transaction in local.
        /// </summary>
        public bool LocalSign { get; set; }

        /// <summary>
        /// Gets the path alternatives which is requested by <see cref="RequestPathFind(PathFindOptions)"/> method.
        /// </summary>
        internal Cache<PathFindAlternative> Paths
        {
            get { return _pathsCache; }
        }

        /// <summary>
        /// Connects to jingtum.
        /// </summary>
        /// <remarks>
        /// Each remote object should connect jingtum first.
        /// Now jingtum should connect manual, only then you can send request to backend.
        /// </remarks>
        /// <param name="callback">The callback.</param>
        public void Connect(MessageCallback<ConnectResponse> callback)
        {
            _server.Connect(callback, data =>
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData<ConnectResponse>>(data as string);
                return response.Result;
            });
        }

        /// <summary>
        /// Disconnects to jingtum.
        /// </summary>
        /// <remarks>
        /// Remote object can be disconnected manual, and no parameters are required.
        /// </remarks>
        public void Disconnect()
        {
             _server.Disconnect();
        }

        /// <summary>
        /// Gets a value indicating whether it's conected to jingtum.
        /// </summary>
        public bool IsConnected
        {
            get { return _server.IsConnected; }
        }

        #region handle messages
        internal void HandleMessage(string data)
        {
            var response = JsonConvert.DeserializeObject<MessageData>(data);
            switch (response.Type)
            {
                case MessageType.LedgerClosed:
                    HandleLedgerClosed(data);
                    break;
                case MessageType.ServerStatus:
                    HandleServerStatus(data);
                    break;
                case MessageType.Response:
                    HandleResponse(data);
                    break;
                case MessageType.Transaction:
                    HandleTransaction(data);
                    break;
                case MessageType.Path_Find:
                    HandlePathFind(data);
                    break;
                case MessageType.Unknown:
                default:
                    // unknown message
                    break;
            }
        }

        private void HandleLedgerClosed(string data)
        {
            if (_ledgerClosed == null) return;

            var info = JsonConvert.DeserializeObject<LedgerClosedInfo>(data);
            
            if (info.LedgerIndex > _status.LedgerIndex)
            {
                _status.LedgerIndex = info.LedgerIndex;
                _status.LedgerTime = info.LedgerTime;
                _status.ReserveBase = info.ReserveBase;
                _status.ReserveInc = info.ReserveInc;
                _status.FeeBase = info.FeeBase;
                _status.FeeRef = info.FeeRef;

                var eArgs = new LedgerClosedEventArgs { Message = data, LedgerClosed = info };
                OnLedgerClosed(eArgs);
            }
        }

        private void HandleServerStatus(string data)
        {
            var response = JsonConvert.DeserializeObject<ServerStatus>(data);
            this.UpdateServerStatus(response);
            OnServerStatusChanged(new ServerStatusEventArgs { Message = data, Status = _status });
        }

        private void UpdateServerStatus(ServerStatus status)
        {
            _status.LoadBase = status.LoadBase;
            _status.LoadFactor = status.LoadFactor;
            if (!string.IsNullOrEmpty(status.PubkeyNode))
            {
                _status.PubkeyNode = status.PubkeyNode;
            }
            _status.Status = status.Status;

            OnlineState onlineState;
            var online = Enum.TryParse(_status.Status, true, out onlineState);
            _server.SetState(online ? ServerState.Online : ServerState.Offline);
        }

        private void HandleResponse(string data)
        {
            var response = JsonConvert.DeserializeObject<ResponseData<ResultWithServerStatus>>(data);

            if(response.Status=="success" && (response.Result!=null && !string.IsNullOrEmpty(response.Result.ServerStatus)))
            {
                var serverStatusResponse  = JsonConvert.DeserializeObject<ResponseData<ServerStatus>>(data);
                UpdateServerStatus(serverStatusResponse.Result);
            }

            var requestId = response.RequestId;
            if (requestId < 0) return;

            var request = _requestsCache.Remove(requestId.ToString()) as RequestCache;
            if (request != null)
            {
                if (response.Status == "success")
                {
                    try
                    {
                        var filteredResult = request.Filter(data);
                        request.Callback(new MessageResult<object>(data, null, filteredResult));
                    }
                    catch(Exception ex)
                    {
                        request.Callback(new MessageResult<object>(data, ex));
                    }
                }
                else
                {
                    var message = response.ErrorMessage ?? response.ErrorException;
                    var requestJson = response.Request?.ToString();
                    request.Callback(new MessageResult<object>(data, new ResponseException(message) { Error = response.Error, ErrorCode = response.ErrorCode, Request=requestJson }));
                }

                if (request.ResetEvent != null)
                {
                    request.ResetEvent.Set();
                }
            }
        }

        private void HandleTransaction(string data)
        {
            if (_transactions == null) return;

            var response = JsonConvert.DeserializeObject<TransactionResponse>(data);
            var hash = response.Transaction.Hash;
            if (_txCache.Contains(hash)) return;

            _txCache.Add(hash, true);
            response.TxResult = ResponseParser.ProcessTx(response.Transaction, response.Meta, response.Transaction.Account);
            var args = new TransactionsEventArgs { Message = data, Result = response };
            OnTransactions(args);
        }

        private void HandlePathFind(string data)
        {
            if (PathFind == null) return;

            var args = new PathFindEventArgs { Message = data };
            OnPathFind(args);
        }

        internal void Submit<T>(string command, dynamic data, Func<object, T> filter, MessageCallback<T> callback, AutoResetEvent resetEvent = null)
        {
            int requestId = _server.SendMessage(command, data);
            if (requestId < 0)
            {
                callback?.Invoke(new MessageResult<T>(null, new Exception("The connection is closed. Please re-connect it.")));
            }
            
            var cache = new RequestCache();
            cache.Command = command;
            cache.Data = data;
            cache.ResetEvent = resetEvent;

            cache.Filter = (message =>
            {
                if (filter == null)
                {
                    try
                    {
                        var responseT = JsonConvert.DeserializeObject<ResponseData<T>>(message as string);
                        return responseT.Result;
                    }
                    catch
                    {
                        return message;
                    }
                }
                else
                {
                    var response = filter(message);
                    return response;
                }
            });

            cache.Callback = (result =>
            {
                if (callback == null) return;

                T tResult;
                try
                {
                    tResult = (T)result.Result;
                }
                catch
                {
                    tResult = default(T);
                }

                callback(new MessageResult<T>(result.Message, result.Exception, tResult));
            });
            _requestsCache.Add(requestId.ToString(), cache);
        }

        #endregion

        #region events
        private EventHandler<TransactionsEventArgs> _transactions;

        private void OnTransactions(TransactionsEventArgs eArgs)
        {
            _transactions?.Invoke(this, eArgs);
        }

        /// <summary>
        /// Occurs when transactions occur in the system.
        /// </summary>
        public event EventHandler<TransactionsEventArgs> Transactions
        {
            add
            {
                if (_transactions == null)
                {
                    Subscribe<object>("transactions").Submit();
                }
                _transactions += value;
            }
            remove
            {
                _transactions -= value;
                if (_transactions == null)
                {
                    Unsubscribe("transactions").Submit();
                }
            }
        }

        private EventHandler<LedgerClosedEventArgs> _ledgerClosed;

        private void OnLedgerClosed(LedgerClosedEventArgs eArgs)
        {
            _ledgerClosed?.Invoke(this, eArgs);
        }

        /// <summary>
        /// Occurs when last ledger closed in the system.
        /// </summary>
        public event EventHandler<LedgerClosedEventArgs> LedgerClosed
        {
            add
            {
                if (_ledgerClosed == null)
                {
                    Subscribe<object>("ledger").Submit();
                }
                _ledgerClosed += value;
            }
            remove
            {
                _ledgerClosed -= value;
                if (_ledgerClosed == null)
                {
                    Unsubscribe("ledger").Submit();
                }
            }
        }

        /// <summary>
        /// Occurs when server status changed in the system.
        /// </summary>
        public event EventHandler<ServerStatusEventArgs> ServerStatusChanged;

        private void OnServerStatusChanged(ServerStatusEventArgs eArgs)
        {
            ServerStatusChanged ?.Invoke(this, eArgs);
        }

        /// <summary>
        /// Occurs when path find in the system.
        /// </summary>
        public event EventHandler<PathFindEventArgs> PathFind;

        private void OnPathFind(PathFindEventArgs eArgs)
        {
            PathFind?.Invoke(this, eArgs);
        }

        /// <summary>
        /// Occurs when the connection is closed.
        /// </summary>
        public event EventHandler<EventArgs> Disconnected;

        internal void OnDisconnected()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the connection is reconnected.
        /// </summary>
        /// <remarks>
        /// It will try to reconnect to server automatically when the connection is closed.
        /// </remarks>
        public event EventHandler<EventArgs> Reconnected;
        internal void OnReconnected()
        {
            Reconnected?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region info request
        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets server info from jingtum.
        /// </summary>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<ServerInfoResponse> RequestServerInfo()
        {
            return new Request<ServerInfoResponse>(this, "server_info");
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets last closed ledger in system.
        /// </summary>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<LedgerClosedResponse> RequestLedgerClosed()
        {
            return new Request<LedgerClosedResponse>(this, "ledger_closed");
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets the ledger in system.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<LedgerResponse> RequestLedger(LedgerOptions options = null)
        {
            var request = new Request<LedgerResponse>(this, "ledger", (data) =>
            {
                var response = JsonConvert.DeserializeObject<ResponseData<LedgerResponse>>(data as string);
                var result = response.Result;

                var transactions = result?.Ledger?.Transactions;
                if (transactions != null)
                {
                    foreach (var tx in transactions)
                    {
                        if (tx.IsExpanded)
                        {
                            tx.TxResult = ResponseParser.ProcessTx(tx, tx.Meta, tx.Account);
                        }
                    }
                }
                return result;
            });

            if (options != null)
            {
                if (options.LedgerIndex != null)
                {
                    request.Message.ledger_index = options.LedgerIndex.Value;
                }

                if (options.LedgerHash != null)
                {
                    if (!Utils.IsValidHash(options.LedgerHash))
                    {
                        request.Message.Error = new InvalidHashException(options.LedgerHash, "LedgerHash", "Invalid ledger hash.");
                        return request;
                    }
                    request.Message.ledger_hash = options.LedgerHash;
                }

                if (options.Full != null)
                {
                    request.Message.full = options.Full.Value;
                }

                if (options.Expand != null)
                {
                    request.Message.expand = options.Expand.Value;
                }

                if (options.Transactions != null)
                {
                    request.Message.transactions = options.Transactions.Value;
                }

                if (options.Accounts != null)
                {
                    request.Message.accounts = options.Accounts.Value;
                }
            }

            return request;
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets one transaction information.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<TxResponse> RequestTx(TxOptions options)
        {
            var request = new Request<TxResponse>(this, "tx", (data)=>
            {
                var response = JsonConvert.DeserializeObject<ResponseData<TxResponse>>(data as string);
                response.Result.TxResult = ResponseParser.ProcessTx(response.Result, response.Result.Meta, response.Result.Account);
                return response.Result;
            });

            if (!Utils.IsValidHash(options.Hash))
            {
                request.Message.Error = new InvalidHashException(options.Hash, "Hash", "Invalid tx hash.");
                return request;
            }

            request.Message.transaction = options.Hash;
            return request;
        }

        private void SetLedgerOptions<T>(LedgerSettings options, Request<T> request)
        {
            if (options == null) return;

            if (options.LedgerIndex != null)
            {
                request.SelectLedger(options.LedgerIndex.Value);
            }
            else if (!string.IsNullOrEmpty(options.LedgerHash))
            {
                if (!Utils.IsValidHash(options.LedgerHash))
                {
                    request.Message.Error = new InvalidHashException(options.LedgerHash, "LedgerHash", "Invalid ledger hash.");
                    return;
                }
                request.SelectLedger(options.LedgerHash);
            }
            else if (options.LedgerState != null)
            {
                request.SelectLedger(options.LedgerState.Value);
            }
            else
            {
                request.SelectLedger(LedgerState.Validated);
            }
        }

        private Request<T> RequestAccount<T>(string type, IAccountOptions options, Request<T> request)
        {
            request.Command = type;

            if (options.Account != null)
            {
                if (!Utils.IsValidAddress(options.Account))
                {
                    request.Message.Error = new InvalidAddressException(options.Account, "Account", "Invalid account address.");
                    return request;
                }
                request.Message.account = options.Account;
            }

            SetLedgerOptions(options.Ledger, request);

            if (options.Peer != null)
            {
                if (!Utils.IsValidAddress(options.Peer))
                {
                    request.Message.Error = new InvalidAddressException(options.Peer, "Peer", "Invalid peer address.");
                    return request;
                }
                request.Message.peer = options.Peer;
            }

            if (options.Limit != null)
            {
                var limit = Math.Max(0, options.Limit.Value);
                limit = Math.Min(1000000000, limit);
                request.Message.limit = limit;
            }

            if (options.Marker != null)
            {
                request.Message.marker = options.Marker;
            }

            return request;
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets account info.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<AccountInfoResponse> RequestAccountInfo(AccountInfoOptions options)
        {
            var request = new Request<AccountInfoResponse>(this);
            return RequestAccount("account_info", options, request);
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets the received and sent jingtum tunms held by the account.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<AccountTumsResponse> RequestAccountTums(AccountTumsOptions options)
        {
            var request = new Request<AccountTumsResponse>(this);
            return RequestAccount("account_currencies", options, request);
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets relations connected to the account.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<AccountRelationsResponse> RequestAccountRelations(AccountRelationsOptions options)
        {
            var request = new Request<AccountRelationsResponse>(this);

            if (!Enum.IsDefined(typeof(RelationType), options.Type))
            {
                request.Message.Error = new ArgumentException("Invalid realtion type.", "Type");
                return request;
            }

            request.Message.relation_type = GetRelationType(options.Type);
            switch (options.Type)
            {
                case RelationType.Trust:
                    return RequestAccount("account_lines", options, request);
                case RelationType.Authorize:
                case RelationType.Freeze:
                    return RequestAccount("account_relation", options, request);
                default:
                    request.Message.Error = new ArgumentException("Relation should not go here.", "Type");
                    break;
            }

            return request;
        }

        private int GetRelationType(RelationType type)
        {
            switch (type)
            {
                case RelationType.Trust:
                    return 0;
                case RelationType.Authorize:
                    return 1;
                case RelationType.Freeze:
                case RelationType.Unfreeze:
                    return 3;
            }

            throw new Exception("Relation should not go here.");
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets account's current offer that is suspended on jingtum system and will be filled by other accounts.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<AccountOffersResponse> RequestAccountOffers(AccountOffersOptions options)
        {
            var request = new Request<AccountOffersResponse>(this).SetFilter(data=>
            {
                var response = JsonConvert.DeserializeObject<ResponseData<AccountOffersResponse>>(data as string);
                return response.Result;
            });
            return RequestAccount("account_offers", options, request);
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets account transactions.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<AccountTxResponse> RequestAccountTx(AccountTxOptions options)
        {
            var request = new Request<AccountTxResponse>(this, "account_tx", (data) =>
            {
                var response = JsonConvert.DeserializeObject<ResponseData<AccountTxResponse>>(data as string);
                if (response.Result.RawTransactions != null)
                {
                    response.Result.Transactions = response.Result.RawTransactions
                        .Select(tx => ResponseParser.ProcessTx(tx.Tx, tx.Meta, options.Account))
                        .ToArray();
                }
                return response.Result;
            });

            if (!Utils.IsValidAddress(options.Account))
            {
                request.Message.Error = new InvalidAddressException(options.Account, "Account", "Invalid account address.");
                return request;
            }

            request.Message.account = options.Account;
            SetLedgerOptions(options.Ledger, request);
            request.Message.ledger_index_min = options.LedgerMin == null ? 0 : options.LedgerMin.Value;
            request.Message.ledger_index_max = options.LedgerMax == null ? -1 : options.LedgerMax;

            if (options.Limit != null)
            {
                request.Message.limit = options.Limit.Value;
            }

            if (options.Offset != null)
            {
                request.Message.offset = options.Offset.Value;
            }

            if(options.Marker!=null && options.Marker.IsValid())
            {
                request.Message.marker = options.Marker;
            }

            if (options.Forward != null)
            {
                request.Message.forward = options.Forward;
            }

            return request;
        }

        /// <summary>
        /// Creates the <see cref="Request{T}"/> object and gets order book info.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<OrderBookResponse> RequestOrderBook(OrderBookOptions options)
        {
            var request = new Request<OrderBookResponse>(this, "book_offers").SetFilter(data=>
            {
                var response = JsonConvert.DeserializeObject<ResponseData<OrderBookResponse>>(data as string);
                var result = response.Result;

                if(result?.Offers != null)
                {
                    foreach(var offer in result.Offers)
                    {
                        offer.Price = ResponseParser.GetPrice(offer.TakerGets, offer.TakerPays, offer.IsSell);
                    }
                }
                return result;
            });

            var takerGets = options.Pays;
            if (!Utils.IsValidAmount0(takerGets))
            {
                request.Message.Error = new InvalidAmountException(takerGets, "Pays", "Invalid pays (taker gets) amount.");
                return request;
            }

            var takerPays = options.Gets;
            if(!Utils.IsValidAmount0(takerPays))
            {
                request.Message.Error = new InvalidAmountException(takerPays, "Gets", "Invalid gets (taker pays) amount.");
                return request;
            }

            request.Message.taker_gets = takerGets;
            request.Message.taker_pays = takerPays;
            request.Message.taker = options.Taker ?? Config.AccountOne;

            if (options.Limit != null)
            {
                request.Message.limit = options.Limit;
            }

            return request;
        }
        #endregion

        #region path find request
        /// <summary>
        /// Creates the <see cref="Reqeust"/> object and gets path from one currency to another.
        /// </summary>
        /// <param name="options">The options for this request.</param>
        /// <returns>A <see cref="Request{T}"/> object.</returns>
        public Request<PathFindResponse> RequestPathFind(PathFindOptions options)
        {
            var request = new Request<PathFindResponse>(this, "path_find", data =>
            {
                var request2 = new Request<object>(this, "path_find");
                request2.Message.subcommand = "close";
                request2.Submit();

                var response = JsonConvert.DeserializeObject<ResponseData<PathFindResponse>>(data as string);

                if (response?.Result?.RawAlternatives != null)
                {
                    response.Result.Alternatives = response.Result.RawAlternatives.Select(item=>
                    {
                        var json = JsonConvert.SerializeObject(item);
                        var key = Utils.Sha1(json);
                        var choice = item.SourceAmount;
                        _pathsCache.Add(key, item);
                        return new PathFind { Key = key, Choice = choice };
                    }).ToArray();
                }

                return response.Result;
            });

            if (!Utils.IsValidAddress(options.Account))
            {
                request.Message.Error = new InvalidAddressException(options.Account, "Account", "Invalid source account.");
                return request;
            }

            if (!Utils.IsValidAddress(options.Destination))
            {
                request.Message.Error = new InvalidAddressException(options.Destination, "Destination", "Invalid destination address.");
                return request;
            }

            if (!Utils.IsValidAmount(options.Amount))
            {
                request.Message.Error = new InvalidAmountException(options.Amount, "Amount", "Invalid amount.");
                return request;
            }

            request.Message.subcommand = "create";
            request.Message.source_account = options.Account;
            request.Message.destination_account = options.Destination;
            request.Message.destination_amount = Utils.ToAmount(options.Amount);

            return request;
        }
        #endregion

        #region subscribe
        internal Request<T> Subscribe<T>(params string[] streams)
        {
            var request = new Request<T>(this, "subscribe");
            if (streams != null && streams.Length > 0)
            {
                request.Message.streams = streams;
            }
            return request;
        }

        internal Request<object> Unsubscribe(params string[] streams)
        {
            var request = new Request<object>(this, "unsubscribe");
            if(streams!=null && streams.Length > 0)
            {
                request.Message.streams = streams;
            }
            return request;
        }

        /// <summary>
        /// Creates the account stub to subscribe events of account.
        /// </summary>
        /// <returns>A <see cref="Account"/> object.</returns>
        public Account CreateAccountStub()
        {
            return new Account(this);
        }

        /// <summary>
        /// Creates the order book stub to subscribe events of order book.
        /// </summary>
        /// <returns>A <see cref="OrderBook"/> object.</returns>
        public OrderBook CreateOrderBooksStub()
        {
            return new OrderBook(this);
        }
        #endregion

        #region transaction request
        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds normal payment transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction<PaymentTxResponse> BuildPaymentTx(PaymentTxOptions options)
        {
            var tx = new InnerTransaction<PaymentTxData, PaymentTxResponse>(this);

            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            if (!Utils.IsValidAddress(options.To))
            {
                tx.TxJson.Exception = new Exception("Invalid destination address.");
                return tx;
            }

            if (!Utils.IsValidAmount(options.Amount))
            {
                tx.TxJson.Exception = new Exception("Invalid amount.");
                return tx;
            }

            tx.TransactionType = TransactionType.Payment;
            tx.TxJson.Account = options.Account;
            tx.TxJson.Amount = Utils.ToAmount(options.Amount);
            tx.TxJson.Destination = options.To;

            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the deploy contract transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction<DeployContractTxResponse> DeployContractTx(DeployContractTxOptions options)
        {
            var tx = new InnerTransaction<ContractTxData, DeployContractTxResponse>(this);

            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid address.");
                return tx;
            }

            tx.TransactionType = TransactionType.ConfigContract;
            tx.TxJson.Account = options.Account;
            tx.TxJson.Amount = (options.Amount * 1000000).ToString("0");
            tx.TxJson.Method = 0;
            tx.TxJson.Payload = Utils.StringToHex(options.Payload);
            tx.TxJson.Args = options.Params == null ? new ArgInfo[0] : options.Params.Select(p=>
            {
                var parameter = new ParameterInfo { Parameter = Utils.StringToHex(p) };
                var arg = new ArgInfo { Arg = parameter };
                return arg;
            });

            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the call contract transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction< CallContractTxResponse> CallContractTx(CallContractTxOptions options)
        {
            var tx = new InnerTransaction<ContractTxData, CallContractTxResponse>(this);

            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid address.");
                return tx;
            }

            if (!Utils.IsValidAddress(options.Destination))
            {
                tx.TxJson.Exception = new Exception("Invalid destination.");
                return tx;
            }

            if (string.IsNullOrEmpty(options.Foo))
            {
                tx.TxJson.Exception = new Exception("Foo must be set.");
                return tx;
            }

            tx.TransactionType = TransactionType.ConfigContract;
            tx.TxJson.Account = options.Account;
            tx.TxJson.Method = 1;
            tx.TxJson.ContractMethod = Utils.StringToHex(options.Foo);
            tx.TxJson.Destination = options.Destination;
            tx.TxJson.Args = options.Params == null ? new ArgInfo[0] : options.Params.Select(p =>
            {
                var parameter = new ParameterInfo { Parameter = Utils.StringToHex(p) };
                var arg = new ArgInfo { Arg = parameter };
                return arg;
            });

            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the sign transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction< SignTxResponse> BuildSignTx(SignTxOptions options)
        {
            var tx = new InnerTransaction<TxData, SignTxResponse>(this);
            tx.TransactionType = TransactionType.Signer;
            tx.TxJson.Blob = options.Blob;
            return tx;
        }

        private InnerTransaction<RelationTxData, V> BuildTrustSet<V>(RelationTxOptions options, InnerTransaction<RelationTxData, V> tx)
            where V : GeneralTxResponse
        {
            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            if (!Utils.IsValidAmount(options.Limit))
            {
                tx.TxJson.Exception = new Exception("Invalid amount.");
                return tx;
            }

            tx.TransactionType = TransactionType.TrustSet;
            tx.TxJson.Account = options.Account;
            if (options.Limit != null)
            {
                tx.TxJson.LimitAmount = options.Limit;
            }
            if (options.QualityIn != null)
            {
                tx.TxJson.QualityIn = options.QualityIn.Value;
            }
            if (options.QualityOut != null)
            {
                tx.TxJson.QualityOut = options.QualityOut.Value;
            }

            return tx;
        }

        private InnerTransaction<RelationTxData, V> BuildRelationSet<V>(RelationTxOptions options, InnerTransaction<RelationTxData, V> tx)
            where V : GeneralTxResponse
        {
            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            if (!Utils.IsValidAddress(options.Target))
            {
                tx.TxJson.Exception = new Exception("Invalid target address.");
                return tx;
            }

            if (!Utils.IsValidAmount(options.Limit))
            {
                tx.TxJson.Exception = new Exception("Invalid amount.");
                return tx;
            }

            tx.TransactionType = options.Type == RelationType.Unfreeze ? TransactionType.RelationDel : TransactionType.RelationSet;
            tx.TxJson.Account = options.Account;
            tx.TxJson.Target = options.Target;
            tx.TxJson.RelationType = (uint)(options.Type == RelationType.Authorize ? 1 : 3);
            if (options.Limit != null)
            {
                tx.TxJson.LimitAmount = options.Limit;
            }

            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the relation transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction<RelationTxResponse> BuildRelationTx(RelationTxOptions options)
        {
            var tx = new InnerTransaction<RelationTxData, RelationTxResponse>(this);

            switch (options.Type)
            {
                case RelationType.Trust:
                    return BuildTrustSet(options, tx);
                case RelationType.Authorize:
                case RelationType.Freeze:
                case RelationType.Unfreeze:
                    return BuildRelationSet(options, tx);
            }

            tx.TxJson.Exception = new Exception("Build relation set should not go here.");
            return tx;
        }

        private InnerTransaction<AccountSetTxData, V> BuildAccountSet<V>(AccountSetTxOptions options, InnerTransaction<AccountSetTxData, V> tx)
            where V : GeneralTxResponse
        {
            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            tx.TransactionType = TransactionType.AccountSet;
            tx.TxJson.Account = options.Account;

            if (options.SetFlag != null)
            {
                tx.TxJson.SetFlag = (UInt32)options.SetFlag.Value;
            }

            if (options.ClearFlag != null)
            {
                tx.TxJson.ClearFlag = (UInt32)options.ClearFlag.Value;
            }

            return tx;
        }

        private InnerTransaction<AccountSetTxData, V> BuildDelegateKeySet<V>(AccountSetTxOptions options, InnerTransaction<AccountSetTxData, V> tx)
            where V : GeneralTxResponse
        {
            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            if (!Utils.IsValidAddress(options.DelegateKey))
            {
                tx.TxJson.Exception = new Exception("Invalid regular key address.");
                return tx;
            }

            tx.TransactionType = TransactionType.SetRegularKey;
            tx.TxJson.Account = options.Account;
            tx.TxJson.RegularKey = options.DelegateKey;

            return tx;
        }

        private InnerTransaction<AccountSetTxData, V> BuildSignerSet<V>(AccountSetTxOptions options, InnerTransaction<AccountSetTxData, V> tx)
            where V : GeneralTxResponse
        {
            tx.TxJson.Exception = new Exception("Not implemented.");
            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the set account attribute transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction<AccountSetTxResponse> BuildAccountSetTx(AccountSetTxOptions options)
        {
            var tx = new InnerTransaction<AccountSetTxData, AccountSetTxResponse>(this);

            switch (options.Type)
            {
                case AccountSetType.Property:
                    return BuildAccountSet(options, tx);
                case AccountSetType.Delegate:
                    return BuildDelegateKeySet(options, tx);
                case AccountSetType.Signer:
                    return BuildSignerSet(options, tx);
            }

            tx.TxJson.Exception = new Exception("Build account set should not go here.");
            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the offer create transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction<OfferCreateTxResponse> BuildOfferCreateTx(OfferCreateTxOptions options)
        {
            var tx = new InnerTransaction<OfferCreateTxData, OfferCreateTxResponse>(this);

            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            var takerGets = options.TakerGets;
            var takerPays = options.TakerPays;
            if (!Utils.IsValidAmount(takerGets))
            {
                tx.TxJson.Exception = new Exception("Invalid to pays amount.");
                return tx;
            }

            if (!Utils.IsValidAmount(takerPays))
            {
                tx.TxJson.Exception = new Exception("Invalid to gets amount.");
                return tx;
            }

            tx.TransactionType = TransactionType.OfferCreate;
            if (options.Type== OfferType.Sell)
            {
                tx.SetFlags((UInt32)OfferCreateFlags.Sell);
            }
            tx.TxJson.Account = options.Account;
            tx.TxJson.TakerPays = Utils.ToAmount(takerPays);
            tx.TxJson.TakerGets = Utils.ToAmount(takerGets);

            return tx;
        }

        /// <summary>
        /// Creates the <see cref=Transaction{T}"/> object and builds the offer cancel transaction.
        /// </summary>
        /// <param name="options">The options for this transaction.</param>
        /// <remarks>
        /// The order can be cancel by order sequence. 
        /// The sequence can be get when order is submitted or from offer query operation.
        /// </remarks>
        /// <returns>A <see cref=Transaction{T}"/> object.</returns>
        public Transaction< OfferCancelTxResponse> BuildOfferCancelTx(OfferCancelTxOptions options)
        {
            var tx = new InnerTransaction<OfferCancelTxData, OfferCancelTxResponse>(this);

            if (!Utils.IsValidAddress(options.Account))
            {
                tx.TxJson.Exception = new Exception("Invalid source address.");
                return tx;
            }

            tx.TransactionType = TransactionType.OfferCancel;
            tx.TxJson.Account = options.Account;
            tx.TxJson.OfferSequence = options.Sequence;
            return tx;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_server != null)
            {
                _server.Dispose();
                _server = null;
            }
        }
        #endregion
    }
}
