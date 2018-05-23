using JingTum.Lib.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace JingTum.Lib
{
    /// <summary>
    /// Post request to server with account secret.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <remarks>
    /// Transaction is used to make transaction and collect transaction parameter. 
    /// Each transaction is secret required, and transaction can be signed local or remote. 
    /// Now remote sign is supported, local sign will be suport soon. 
    /// All transaction is asynchronized and should provide a callback. 
    /// Each callback has two parameter, one is error and the other is result.
    /// </remarks>
    public abstract class Transaction<T> where T : GeneralTxResponse
    {
        private Remote _remote;
        private Func<object, T> _filter;
        private string _secret;
        private TransactionType _type;
        private TxData _txJson;

        protected Transaction(Remote remote, Func<object, T> filter = null)
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
        /// Account can be master account, delegate account or operation account.
        /// </summary>
        /// <returns>The account for this transaction.</returns>
        public string GetAccount()
        {
            return _txJson.Account;
        }

        /// <summary>
        /// Get transaction type.
        /// </summary>
        /// <returns>The relation type.</returns>
        public TransactionType GetTransactionType()
        {
            return _type;
        }

        internal void SetTransactionType(TransactionType type)
        {
            _type = type;
            _txJson.TransactionType = _type.ToString();
        }

        /// <summary>
        /// Set Transaction secret.
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
        /// Add one memo to transaction.
        /// </summary>
        /// <remarks>
        /// Memo is string and is limited to 2k.
        /// Memo is one way to add payload to transaction. 
        /// But payload should be add in another way.
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
        public void SetFee(long fee)
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
            return MaxAmount(new AmountSettings(amount.Currency, amount.Issuer, amount.Value));
        }

        private object MaxAmount(AmountSettings amount)
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
                    return new AmountSettings(amount.Currency, amount.Issuer, amountValue.ToString());
                }
            }

            return new Exception("Invalid amount to max.");
        }

        /// <summary>
        /// Set path for one transaction.
        /// </summary>
        /// <remarks>
        /// The key parameter is request by requestPathFind. 
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

            // todo,item.Path is complex
            _txJson.Paths = item.PathsComputed;
            _txJson.SendMax = MaxAmount(item.SourceAmount);
        }

        /// <summary>
        /// Set payment transaction max amount when needed. 
        /// </summary>
        /// <remarks>
        /// It is set by "Path" parameter default.
        /// </remarks>
        /// <param name="amount">The max amount.</param>
        public void SetSendMax(AmountSettings amount)
        {
            if (!Utils.IsValidAmount(amount))
            {
                _txJson.Exception = new ArgumentException("Invalid send max amount");
                return;
            }

            _txJson.SendMax = Utils.ToAmount(amount);
        }

        /// <summary>
        /// Set transaction transfer rate.
        /// </summary>
        /// <remarks>
        ///  It should be check with fee.
        /// </remarks>
        /// <param name="rate">The transfer rate.</param>
        public void SetTransferRate(double rate)
        {
            if (rate < 0 || rate > 1)
            {
                _txJson.Exception = new ArgumentOutOfRangeException("Incalid transfer rate.");
                return;
            }

            _txJson.TransferRate = decimal.ToUInt32(((decimal)rate + 1) * 1000000000);
        }

        /// <summary>
        /// Set transaction flags.
        /// </summary>
        /// <remarks>
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
        /// <param name="flags">The flags value.</param>
        public void SetFlags(UInt32 flags)
        {
            _txJson.Flags = flags;
        }

        /// <summary>
        /// Set transaction flags.
        /// </summary>
        /// <remarks>
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
        /// <param name="flags">The flags value.</param>
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

        public void Sign(MessageCallback<string> callback)
        {
            var req = _remote.RequestAccountInfo(new AccountInfoOptions { Account = _txJson.Account });
            req.Submit(accountInfoResult =>
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

                callback(new MessageResult<string>(null, null, _txJson.Blob));
            });
        }

        /// <summary>
        /// Submit entry for transaction.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void Submit(MessageCallback<T> callback)
        {
            if(_txJson.Exception != null)
            {
                callback?.Invoke(new MessageResult<T>(null, _txJson.Exception));
                return;
            }

            if (_type == TransactionType.Signer)//直接将blob传给底层
            {
                dynamic data = new ExpandoObject();
                data.tx_blob = _txJson.Blob;
                _remote.Submit("submit", data, _filter, callback);
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
                    _remote.Submit("submit", data, _filter, callback);
                });
            }
            else//不签名交易传给底层
            {
                dynamic data = new ExpandoObject();
                data.secret = _secret;
                data.tx_json = _txJson;
                _remote.Submit("submit", data, _filter, callback);
            }
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
