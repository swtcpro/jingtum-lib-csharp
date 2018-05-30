using JingTum.Lib.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Posts request to server with account secret.
    /// </summary>
    /// <typeparam name="T">The type of the parsed response message.</typeparam>
    /// <remarks>
    /// Transaction is used to make transaction and collect transaction parameter. 
    /// Each transaction is secret required, and transaction can be signed local or remote. 
    /// All transactions are asynchronized and should provide a callback. 
    /// </remarks>
    public abstract class Transaction<T> where T : GeneralTxResponse
    {
        private Remote _remote;
        private Func<object, T> _filter;
        private string _secret;
        private TransactionType _type;
        private TxData _txJson;
        private bool _localSigned;

        internal Transaction(Remote remote, Func<object, T> filter = null)
        {
            _remote = remote;
            _filter = filter;
        }

        internal Transaction<T> SetFilter(Func<object, T> filter)
        {
            _filter = filter;
            return this;
        }

        internal TxData Data
        {
            get { return _txJson; }
            set { _txJson = value; }
        }

        /// <summary>
        /// Gets the account which builds the transaction.
        /// </summary>
        /// <remarks>
        /// Account can be master account, delegate account or operation account.
        /// </remarks>
        /// <returns>The account for this transaction.</returns>
        public string Account
        {
            get { return _txJson.Account; }
        }

        /// <summary>
        /// Gets the transaction type.
        /// </summary>
        /// <returns>A <see cref="TransactionType"/> enum value.</returns>
        public TransactionType TransactionType
        {
            get { return _type; }
            internal set
            {
                _type = value;
                _txJson.TransactionType = value.ToString();
            }
        }

        /// <summary>
        /// Sets the transaction secret.
        /// </summary>
        /// <remarks>
        /// It is required before transaction submit.
        /// </remarks>
        /// <param name="secret">The secret address.</param>
        public void SetSecret(string secret)
        {
            _secret = secret;
        }

        /// <summary>
        /// Adds one memo to the transaction.
        /// </summary>
        /// <remarks>
        /// Memo is string and is limited to 2k.
        /// </remarks>
        /// <param name="data">The memo string.</param>
        public void AddMemo(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                _txJson.Exception = new ArgumentException("Momo is empty.");
                return;
            }

            if (data.Length > 2048)
            {
                _txJson.Exception = new ArgumentOutOfRangeException("The length of Memo shoule be less than or equal 2048.");
                return;
            }

            var memo = new MemoInfo { Memo = new MemoDataInfo { MemoData = Utils.BytesToHex(Encoding.UTF8.GetBytes(data)) } };
            var memos = _txJson.Memos ?? (_txJson.Memos = new List<MemoInfo>());
            memos.Add(memo);
        }

        /// <summary>
        /// Sets the fee.
        /// </summary>
        /// <param name="fee">The fee.</param>
        public void SetFee(UInt32 fee)
        {
            if (fee < 10)
            {
                _txJson.Exception = new ArgumentOutOfRangeException("Fee should be great than or equal 10.");
                return;
            }

            _txJson.Fee = fee;
        }

        private object MaxAmount(Amount amount)
        {
            if (amount.Currency == Config.Currency)
            {
                decimal amountValue;
                if (decimal.TryParse(amount.Value, out amountValue))
                {
                    amountValue *= new decimal(1.0001);
                    return amountValue.ToString("0");
                }
            }
            else
            {
                if (Utils.IsValidAmount(amount))
                {
                    decimal amountValue = decimal.Parse(amount.Value);
                    amountValue *= new decimal(1.0001);
                    return new Amount(amount.Currency, amount.Issuer, amountValue.ToString());
                }
            }

            return new Exception("Invalid amount to max.");
        }

        /// <summary>
        /// Sets the payment path for one transaction.
        /// </summary>
        /// <remarks>
        /// The key parameter is request by <see cref="Remote.RequestPathFind(PathFindOptions)"/>. 
        /// When the key is set, "SendMax" parameter is also set.
        /// </remarks>
        /// <param name="key">The key of the path.</param>
        public void SetPath(string key)
        {
            if (key == null || key.Length != 40)
            {
                _txJson.Exception = new ArgumentException("Invalid path key.");
                return;
            }

            var item = _remote.Paths.Get(key);
            if (item == null)
            {
                _txJson.Exception = new ArgumentException("Non exists path key.");
                return;
            }

            //沒有支付路径，不需要传下面的参数
            if (item.PathsComputed == null || item.PathsComputed.Length == 0)
            {
                return;
            }

            _txJson.Paths = item.PathsComputed;
            _txJson.SendMax = MaxAmount(item.SourceAmount);
        }

        /// <summary>
        /// Sets the payment transaction max amount when needed. 
        /// </summary>
        /// <remarks>
        /// It is set by <see cref="SetPath(string)"/> default.
        /// </remarks>
        /// <param name="amount">The max amount.</param>
        public void SetSendMax(Amount amount)
        {
            if (!Utils.IsValidAmount(amount))
            {
                _txJson.Exception = new ArgumentException("Invalid send max amount");
                return;
            }

            _txJson.SendMax = Utils.ToAmount(amount);
        }

        /// <summary>
        /// Sets the transaction transfer rate.
        /// </summary>
        /// <remarks>
        ///  It should be checked with fee.
        /// </remarks>
        /// <param name="rate">The transfer rate.</param>
        public void SetTransferRate(float rate)
        {
            if (rate < 0 || rate > 1)
            {
                _txJson.Exception = new ArgumentOutOfRangeException("Incalid transfer rate.");
                return;
            }

            _txJson.TransferRate = decimal.ToUInt32(((decimal)rate + 1) * 1000000000);
        }

        /// <summary>
        /// Sets the transaction flags.
        /// </summary>
        /// <remarks>
        /// <para>It is used to set Offer type mainly.</para>
        /// The flags depends on the transaction type.
        /// <para>There are following flags for specific transaction type.</para>
        /// <list type="bullet">
        /// <item><see cref="AccountSetFlags"/></item>
        /// <item><see cref="TrustSetFlags"/></item>
        /// <item><see cref="OfferCreateFlags"/></item>
        /// <item><see cref="PaymentFlags"/></item>
        /// <item><see cref="RelationSetFlags"/></item>
        /// <item><see cref="UniversalFlags"/> Apply to any transaction type.</item>
        /// </list>
        /// </remarks>
        /// <param name="flags">The flags value.</param>
        public void SetFlags(UInt32 flags)
        {
            _txJson.Flags = flags;
        }

        /// <summary>
        /// Sets the transaction flags.
        /// </summary>
        /// <remarks>
        /// <para>It is used to set Offer type mainly.</para>
        /// The flags depends on the TransactionType.
        /// <para>There are following flags for specific transaction type.</para>
        /// <list type="bullet">
        /// <item><see cref="AccountSetFlags"/></item>
        /// <item><see cref="TrustSetFlags"/></item>
        /// <item><see cref="OfferCreateFlags"/></item>
        /// <item><see cref="PaymentFlags"/></item>
        /// <item><see cref="RelationSetFlags"/></item>
        /// <item><see cref="UniversalFlags"/> Apply to any transaction type.</item>
        /// </list>
        /// </remarks>
        /// <param name="flags">The flags name.</param>
        public void SetFlags(params string[] flags)
        {
            Type type = null;
            Type commonType = typeof(UniversalFlags);
            switch (_type)
            {
                case TransactionType.AccountSet:
                    type = typeof(AccountSetFlags);
                    break;
                case TransactionType.TrustSet:
                    type = typeof(TrustSetFlags);
                    break;
                case TransactionType.OfferCreate:
                    type = typeof(OfferCreateFlags);
                    break;
                case TransactionType.Payment:
                    type = typeof(PaymentFlags);
                    break;
                case TransactionType.RelationSet:
                    type = typeof(RelationSetFlags);
                    break;
                default:
                    break;
            }

            UInt32 flagsValue = 0;
            foreach (var flag in flags)
            {
                if (type != null)
                {
                    flagsValue |= GetFlagValue(type, flag);
                }
                flagsValue |= GetFlagValue(commonType, flag);
            }

            _txJson.Flags = flagsValue;
        }

        private UInt32 GetFlagValue(Type type, string flag)
        {
            try
            {
                return (UInt32)Enum.Parse(type, flag, true);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Sign the transaction.
        /// </summary>
        /// <param name="callback">The callback for the blob of sign result.</param>
        public void Sign(MessageCallback<string> callback)
        {
            var req = _remote.RequestAccountInfo(new AccountInfoOptions { Account = _txJson.Account });
            req.Submit(accountInfoResult =>
            {
                Sign(accountInfoResult, callback);
            });
        }

        private void Sign(MessageResult<AccountInfoResponse> accountInfoResult, MessageCallback<string> callback)
        {
            if (accountInfoResult.Exception != null)
            {
                callback?.Invoke(new MessageResult<string>("sign error: ", accountInfoResult.Exception));
                return;
            }

            const decimal factor = 1000000;
            var accountInfo = accountInfoResult.Result;
            _txJson.Sequence = accountInfo.AccountData.Sequence;
            var fee = Utils.TryGetNumber<decimal>(_txJson.Fee);
            if (fee == null)
            {
                fee = (decimal)Config.Fee;
            }
            _txJson.Fee = fee.Value / factor;

            //payment
            var amount = Utils.TryGetNumber<decimal>(_txJson.Amount);
            if (amount.HasValue)
            {
                _txJson.Amount = amount / factor;
            }

            if (_txJson.Memos != null)
            {
                foreach (var memo in _txJson.Memos)
                {
                    memo.Memo.MemoData = Encoding.UTF8.GetString(Utils.HexToBytes(memo.Memo.MemoData));
                }
            }

            var sendMax = Utils.TryGetNumber<decimal>(_txJson.SendMax);
            if (sendMax.HasValue) _txJson.SendMax = sendMax / factor;

            // order
            var offerCreateTxData = _txJson as OfferCreateTxData;
            if (offerCreateTxData != null)
            {
                var takerPays = Utils.TryGetNumber<decimal>(offerCreateTxData.TakerPays);
                if (takerPays.HasValue)
                {
                    offerCreateTxData.TakerPays = takerPays / factor;
                }

                var takerGets = Utils.TryGetNumber<decimal>(offerCreateTxData.TakerGets);
                if (takerGets.HasValue)
                {
                    offerCreateTxData.TakerGets = takerGets / factor;
                }
            }

            var wt = Wallet.FromSecret(_secret);
            var pubKey = Utils.BytesToHex(wt.GetPublicKey().ToByteArray());

            _txJson.SigningPubKey = pubKey;

            // The TxnSignature is different for each Sign with the same hash.
            // the TxnSignature is set for unit test
            if (_txJson.TxnSignature == null)
            {
                var so = Serializer.Create(_txJson);
                var hash = so.Hash(0x53545800);
                _txJson.TxnSignature = wt.Sign(hash);
            }

            var soTx = Serializer.Create(_txJson);
            _txJson.Blob = soTx.ToHex();
            _localSigned = true;

            callback(new MessageResult<string>(null, null, _txJson.Blob));
        }

        /// <summary>
        /// Submits entry for transaction.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Submit(MessageCallback<T> callback)
        {
            SubmitAsync(callback);
        }

        /// <summary>
        /// The async version of <see cref="Submit(MessageCallback{T})"/>.
        /// </summary>
        /// <param name="callback">The callback for the request result.</param>
        /// <param name="timeout">The millisends to wait for the result.</param>
        /// <returns>The task.</returns>
        public Task SubmitAsync(MessageCallback<T> callback, int timeout = -1)
        {
            if (_txJson.Exception != null)
            {
                callback?.Invoke(new MessageResult<T>(null, _txJson.Exception));
                return AsyncEx.Complete();
            }

            var resetEvent = new AutoResetEvent(false);
            var task = new Task(() =>
            {
                resetEvent.WaitOne(timeout);
                resetEvent.Dispose();
            });
            task.Start();

            if (_type == TransactionType.Signer || _localSigned)//直接将blob传给底层
            {
                dynamic data = new ExpandoObject();
                data.tx_blob = _txJson.Blob;
                _remote.Submit("submit", data, _filter, callback, resetEvent);
            }
            else if (_remote.LocalSign)//签名之后传给底层
            {
                Sign(result =>
                {
                    if (result.Exception != null)
                    {
                        callback?.Invoke(new MessageResult<T>("sign error: ", result.Exception));
                        return;
                    }

                    dynamic data = new ExpandoObject();
                    data.tx_blob = result.Result;
                    _remote.Submit("submit", data, _filter, callback, resetEvent);
                });
            }
            else//不签名交易传给底层
            {
                dynamic data = new ExpandoObject();
                data.secret = _secret;
                data.tx_json = _txJson;
                _remote.Submit("submit", data, _filter, callback, resetEvent);
            }

            return task;
        }
    }

    /// <summary>
    /// The inner wrapper class of <see cref="Transaction{T}"/>
    /// </summary>
    /// <typeparam name="V">The type of transaction data.</typeparam>
    /// <typeparam name="T">The type of the response data.</typeparam>
    internal class InnerTransaction<V, T> : Transaction<T> 
        where T:GeneralTxResponse
        where V: TxData, new() 
    {
        private readonly V _txJson;

        public InnerTransaction(Remote remote, Func<object, T> filter = null) : base(remote, filter)
        {
            _txJson = new V();
            _txJson.Flags = 0;
            _txJson.Fee = Config.Fee;

            Data = _txJson;
        }

        public V TxJson { get { return _txJson; } }
    }
}
