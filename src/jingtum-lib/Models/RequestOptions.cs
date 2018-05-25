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
        /// The ledger index to return.
        /// </summary>
        /// <remarks>
        /// Opitonal. If <see cref="LedgerIndex"/> and <see cref="LedgerHash"/> are not provided, then last closed ledger is returned.
        /// </remarks>
        public long? LedgerIndex { get; set; }
        /// <summary>
        /// The ledger hash to return.
        /// </summary>
        /// <remarks>
        /// Optional. If <see cref="LedgerIndex"/> and <see cref="LedgerHash"/> are not provided, then last closed ledger is returned.
        /// </remarks>
        public string LedgerHash { get; set; }
        /// <summary>
        /// Whether gets expanded info of both transactions and accounts.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Full { get; set; }
        /// <summary>
        /// Whether gets expanded info the transactions or accounts.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Expand { get; set; }
        /// <summary>
        /// Whether gets the transactions hash list in the ledger.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Transactions { get; set; }
        /// <summary>
        /// Whether gets the accounts list in the ledger.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Accounts { get; set; }
    }

    /// <summary>
    /// Represents the options for transaction request.
    /// </summary>
    public class TxOptions
    {
        /// <summary>
        /// The transaction hash.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        [Category("Required")]
        public string Hash { get; set; }
    }

    /// <summary>
    /// Represents the ledger settings.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LedgerSettings
    {
        private UInt32? _ledgerIndex;
        private string _ledgerHash;
        private LedgerState? _ledgerState;

        /// <summary>
        /// The ledger hash.
        /// </summary>
        /// <remarks>
        /// Optional. It will be reset if <see cref="LedgerIndex"/> or <see cref="LedgerState"/> is set.
        /// </remarks>
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
        /// <remarks>
        /// Optional. It will be reset if <see cref="LedgerHash"/> or <see cref="LedgerState"/> is set.
        /// </remarks>
        public UInt32? LedgerIndex
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
        /// <remarks>
        /// Optional. It will be reset if <see cref="LedgerIndex"/> or <see cref="LedgerHash"/> is set.
        /// </remarks>
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
        UInt32? Limit { get; set; }
        string Marker { get; set; }
        string Peer { get; set; }
    }

    /// <summary>
    /// Represents the options the account info request.
    /// </summary>
    public class AccountInfoOptions : IAccountOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The ledger option.
        /// </summary>
        /// <remarks>
        /// Optional. Default is the <see cref="LedgerState.Validated"/>.
        /// </remarks>
        public LedgerSettings Ledger { get; set; }

        UInt32? IAccountOptions.Limit { get; set; }
        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    /// <summary>
    /// Represents the options for the account tums request.
    /// </summary>
    public class AccountTumsOptions : IAccountOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The ledger option.
        /// </summary>
        /// <remarks>
        /// Optional. Default is the <see cref="LedgerState.Validated"/>.
        /// </remarks>
        [Category("Optional")]
        public LedgerSettings Ledger { get; set; }

        UInt32? IAccountOptions.Limit { get; set; }
        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    /// <summary>
    /// Represents the options for account relations request.
    /// </summary>
    public class AccountRelationsOptions : IAccountOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The relation type.
        /// </summary>
        /// <remarks>
        /// Required.
        /// </remarks>
        public RelationType Type { get; set; }
        /// <summary>
        /// The ledger option.
        /// </summary>
        /// <remarks>
        /// Optional. Default is the <see cref="LedgerState.Validated"/>.
        /// </remarks>
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        ///  Limit output tx record.
        /// </summary>
        /// <remarks>
        /// Optional. Default is 200.
        /// </remarks>
        public UInt32? Limit { get; set; }
        /// <summary>
        /// The marker string.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string Marker { get; set; }
        /// <summary>
        /// The peer string.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public string Peer { get; set; }
    }

    /// <summary>
    /// Gets the options for account offers request.
    /// </summary>
    public class AccountOffersOptions : IAccountOptions
    {
        /// <summary>
        /// The account address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The ledger option.
        /// </summary>
        /// <remarks>
        ///  Optional. Default is the <see cref="LedgerState.Validated"/>.
        /// </remarks>
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        /// Limit output tx record.
        /// </summary>
        /// <remarks>
        /// Optional. Min is 200.
        /// </remarks>
        public UInt32? Limit { get; set; }

        string IAccountOptions.Marker { get; set; }
        string IAccountOptions.Peer { get; set; }
    }

    /// <summary>
    /// Represents the options for account tx request.
    /// </summary>
    public class AccountTxOptions
    {
        /// <summary>
        /// The accound address.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// The ledger option.
        /// </summary>
        /// <remarks>
        /// Optional. Default is the <see cref="LedgerState.Validated"/>.
        /// </remarks>
        public LedgerSettings Ledger { get; set; }
        /// <summary>
        /// The min of the ledger range.
        /// </summary>
        /// <remarks>
        ///  Optional. Default is 0.
        /// </remarks>
        public long? LedgerMin { get; set; }
        /// <summary>
        /// The max of the ledger range.
        /// </summary>
        /// <remarks>
        /// Optional. Default is -1.
        /// </remarks>
        public long? LedgerMax { get; set; }
        /// <summary>
        /// Limit output tx record.
        /// </summary>
        /// <remarks>
        /// Optional. 
        /// </remarks>
        public UInt32? Limit { get; set; }
        /// <summary>
        /// The offset.
        /// </summary>
        /// <remarks>
        /// Opitonal.
        /// </remarks>
        public UInt32? Offset { get; set; }
        /// <summary>
        /// The marker settings.
        /// </summary>
        /// <remarks>
        /// Optional.
        /// </remarks>
        public MarkerSettings Marker { get; set; }
        /// <summary>
        ///  Whether returns tx records from older to newer.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Forward { get; set; }
    }

    /// <summary>
    /// Represents the marker settings.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [JsonConverter(typeof(NoTypeConverterJsonConverter<MarkerSettings>))]
    public class MarkerSettings
    {
        /// <summary>
        /// The ledger index.
        /// </summary>
        [JsonProperty("ledger")]
        public UInt32? Ledger { get; set; }
        /// <summary>
        /// The sequence.
        /// </summary>
        [JsonProperty("seq")]
        public UInt32? Seq { get; set; }

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
        /// The amount to get.
        /// </summary>
        /// <remarks>
        /// Requied. 
        /// </remarks>
        public AmountSettings Gets { get; set; }
        /// <summary>
        /// The amount to exchange out.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public AmountSettings Pays { get; set; }
        /// <summary>
        /// The taker address.
        /// </summary>
        /// <remarks>
        ///  Optional.
        /// </remarks>
        public string Taker { get; set; }
        /// <summary>
        /// The limit of the records.
        /// </summary>
        /// <remarks>
        /// Opitonal. Default is 300.
        /// </remarks>
        public UInt32? Limit { get; set; }
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
        /// Account to find path.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Account { get; set; }
        /// <summary>
        /// Destination account.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public string Destination { get; set; }
        /// <summary>
        /// The amount that destination will received.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public AmountSettings Amount { get; set; }
    }
}
