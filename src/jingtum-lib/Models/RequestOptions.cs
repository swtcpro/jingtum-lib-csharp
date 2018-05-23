using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the options for ledger request.
    /// </summary>
    public class LedgerOptions
    {
        /// <summary>
        /// Optional. The ledger index to return.
        /// </summary>
        /// <remarks>
        /// If <see cref="LedgerIndex"/> and <see cref="LedgerHash"/> are not provided, then last closed ledger is returned.
        /// </remarks>
        [Category("Optional")]
        public long? LedgerIndex { get; set; }
        /// <summary>
        /// Optional. The ledger hash to return.
        /// </summary>
        /// <remarks>
        /// If <see cref="LedgerIndex"/> and <see cref="LedgerHash"/> are not provided, then last closed ledger is returned.
        /// </remarks>
        [Category("Optional")]
        public string LedgerHash { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        [Category("Optional")]
        public bool? Full { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        [Category("Optional")]
        public bool? Expand { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        [Category("Optional")]
        public bool? Transactions { get; set; }
        /// <summary>
        /// Reserved.
        /// </summary>
        [Category("Optional")]
        public bool? Accounts { get; set; }
    }

    /// <summary>
    /// Represents the options for transaction request.
    /// </summary>
    public class TxOptions
    {
        /// <summary>
        /// Required. The transaction hash.
        /// </summary>
        [Category("Required")]
        public string Hash { get; set; }
    }

    /// <summary>
    /// Represents the ledger option.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LedgerSettings
    {
        private long? _ledgerIndex;
        private string _ledgerHash;
        private LedgerState? _ledgerState;

        /// <summary>
        /// The ledger hash.
        /// </summary>
        [Category("Optional")]
        public string LedgerHash
        {
            get { return _ledgerHash; }
            set
            {
                _ledgerHash = value;
                _ledgerIndex = null;
                _ledgerIndex = null;
            }
        }

        /// <summary>
        /// The ledger index.
        /// </summary>
        [Category("Optional")]
        public long? LedgerIndex
        {
            get { return _ledgerIndex; }
            set
            {
                _ledgerIndex = value;
                _ledgerState = null;
                _ledgerHash = null;
            }
        }

        /// <summary>
        /// The ledger state.
        /// </summary>
        [Category("Optional")]
        public LedgerState? LedgerState
        {
            get { return _ledgerState; }
            set
            {
                _ledgerState = value;
                _ledgerIndex = null;
                _ledgerHash = null;
            }
        }
    }

    internal interface IAccountOptions
    {
        string Account { get; set; }
        LedgerSettings Ledger { get; set; }
        int? Limit { get; set; }
        string Marker { get; set; }
        string Peer { get; set; }
    }

    public class AccountInfoOptions : IAccountOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Optional. The ledger option.
        /// </summary>
        /// <remarks>
        /// If it's not provided, the <see cref="LedgerState.Validated"/> is returned.
        /// </remarks>
        [Category("Optional")]
        public LedgerSettings Ledger { get; set; }

        int? IAccountOptions.Limit { get; set; }
        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    public class AccountTumsOptions : IAccountOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Optional. The ledger option.
        /// </summary>
        /// <remarks>
        /// If it's not provided, the <see cref="LedgerState.Validated"/> is returned.
        /// </remarks>
        [Category("Optional")]
        public LedgerSettings Ledger { get; set; }

        int? IAccountOptions.Limit { get; set; }
        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    public class AccountRelationsOptions : IAccountOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Required.The relation type.
        /// </summary>
        [Category("Required")]
        public RelationType Type { get; set; }
        /// <summary>
        /// Optional. The ledger option.
        /// </summary>
        /// <remarks>
        /// If it's not provided, the <see cref="LedgerState.Validated"/> is returned.
        /// </remarks>
        [Category("Optional")]
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        /// Optional. Limit output tx record.
        /// </summary>
        /// <remarks>
        /// Min is 200.
        /// </remarks>
        [Category("Optional")]
        public int? Limit { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        [Category("Optional")]
        public string Marker { get; set; }

        public string Peer { get; set; }
    }

    public class AccountOffersOptions : IAccountOptions
    {
        /// <summary>
        /// Required. The account address.
        /// </summary>
        [Category("Required")]
        public string Account { get; set; }
        /// <summary>
        /// Optional. The ledger option.
        /// </summary>
        /// <remarks>
        /// If it's not provided, the <see cref="LedgerState.Validated"/> is returned.
        /// </remarks>
        [Category("Optional")]
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        /// Optional. Limit output tx record.
        /// </summary>
        /// <remarks>
        /// Min is 200.
        /// </remarks>
        [Category("Optional")]
        public int? Limit { get; set; }

        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    /// <summary>
    /// Represents the options of account tx request.
    /// </summary>
    public class AccountTxOptions
    {
        /// <summary>
        /// Required. The accound address.
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Optional. The ledger option.
        /// </summary>
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        /// <remarks>
        /// Default is 0 it not provided.
        /// </remarks>
        public int? LedgerMin { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        /// <remarks>
        /// Default is -1 if not provided.
        /// </remarks>
        public int? LedgerMax { get; set; }
        /// <summary>
        /// Optional. Limit output tx record.
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Opitonal.
        /// </summary>
        public int? Offset { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        public MarkerSettings Marker { get; set; }
        /// <summary>
        /// Optional. If returns recently tx records.
        /// </summary>
        public bool? Forward { get; set; }
    }

    /// <summary>
    /// Represents the marker option.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [JsonConverter(typeof(NoTypeConverterJsonConverter<MarkerSettings>))]
    public class MarkerSettings
    {
        /// <summary>
        /// The ledger index.
        /// </summary>
        [JsonProperty("ledger")]
        public long? Ledger { get; set; }
        /// <summary>
        /// The sequence.
        /// </summary>
        [JsonProperty("seq")]
        public long? Seq { get; set; }

        internal bool IsValid()
        {
            return Ledger!= null && Seq != null;
        }
    }

    /// <summary>
    /// Represents the options for order book request.
    /// </summary>
    public class OrderBookOptions
    {
        /// <summary>
        /// Requied. The amout to get.
        /// </summary>
        public AmountSettings Gets { get; set; }
        /// <summary>
        /// Required. The amout to exchange out.
        /// </summary>
        public AmountSettings Pays { get; set; }
        /// <summary>
        /// Optional.
        /// </summary>
        public string Taker { get; set; }
        /// <summary>
        /// Opitonal.
        /// </summary>
        public int? Limit { get; set; }
    }

    internal interface IAmount
    {
        string Currency { get; }
        string Issuer { get; }
        string Value { get; }
    }

    /// <summary>
    /// Represents the amount options for <see cref="Request{T}"/> and <see cref="Transaction{T}"/>.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [JsonConverter(typeof(NoTypeConverterJsonConverter<AmountSettings>))]
    public class AmountSettings : IAmount,ICloneable
    {
        /// <summary>
        /// Creates a new instance of <see cref="AmountSettings"/> object.
        /// </summary>
        public AmountSettings()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="AmountSettings"/> object for SWT currency.
        /// </summary>
        /// /// <param name="value">The amount value.</param>
        /// <returns>The <see cref="AmountSettings"/> object.</returns>
        public static AmountSettings SWT(string value = null)
        {
            return new AmountSettings("SWT", "", value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AmountSettings"/> object.
        /// </summary>
        /// <param name="currency">The amount currency.</param>
        /// <param name="issuer">The amount issuer.</param>
        /// <param name="value">The amount value.</param>
        public AmountSettings(string currency, string issuer, string value = null)
        {
            Currency = currency;
            Issuer = issuer;
            Value = value;
        }

        /// <summary>
        /// Optional. The value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
        /// <summary>
        /// Required. The currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Required. The issuer.
        /// </summary>
        /// <remarks>
        /// The issuer is ignored if the currency is "SWT".
        /// </remarks>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        public object Clone()
        {
            var that = new AmountSettings();
            that.Value = Value;
            that.Currency = Currency;
            that.Issuer = Issuer;
            return that;
        }

        public override string ToString()
        {
            if (Currency == Config.Currency) return Currency;
            return string.Format("{0}:{1}", Currency, Issuer);
        }
    }

    /// <summary>
    /// Represents the options for path find request.
    /// </summary>
    public class PathFindOptions
    {
        /// <summary>
        /// Required. Account to find path.
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Required. Destination account.
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// Required. The amount that destination will received.
        /// </summary>
        public AmountSettings Amount { get; set; }
    }
}
