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
        public string Hash { get; set; }
    }

    /// <summary>
    /// Represents the ledger settings.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
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
        /// The marker for this reqeust.
        /// </summary>
        /// <remarks>
        /// Optional. It's from the previous reqeust with limit.
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
        /// <summary>
        /// The marker for this request.
        /// </summary>
        /// <remarks>
        /// Optional. It's from the previous reqeust with limit.
        /// </remarks>
        public string Marker { get; set; }

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
        /// The marker settings for this request.
        /// </summary>
        /// <remarks>
        /// Optional. It's from the previous reqeust with limit.
        /// </remarks>
        public Marker Marker { get; set; }
        /// <summary>
        ///  Whether returns tx records from older to newer.
        /// </summary>
        /// <remarks>
        /// Optional. Default is false.
        /// </remarks>
        public bool? Forward { get; set; }
    }

    /// <summary>
    /// Represents the marker.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
    public class Marker
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
        public Amount Gets { get; set; }
        /// <summary>
        /// The amount to exchange out.
        /// </summary>
        /// <remarks>
        /// Required. 
        /// </remarks>
        public Amount Pays { get; set; }
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
        public Amount Amount { get; set; }
    }
}
