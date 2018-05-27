using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    #region response data
    internal class MessageData
    {
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("id")]
        public int RequestId { get; internal set; } = -1;
        [JsonProperty("error")]
        public string Error { get; internal set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; internal set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; internal set; }
        [JsonProperty("error_exception")]
        public string ErrorException { get; internal set; }
        [JsonProperty("request")]
        public dynamic Request { get; internal set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        public MessageType Type { get; set; }
    }

    internal enum MessageType
    {
        Unknown=0,
        LedgerClosed,
        ServerStatus,
        Response,
        Transaction,
        Path_Find
    }

    internal class ResultWithServerStatus
    {
        [JsonProperty("server_status")]
        public string ServerStatus { get; internal set; }
    }

    internal class ResponseData<T> : MessageData
    {
        [JsonProperty("result")]
        public T Result { get; internal set; }
    }
    #endregion

    #region Connect
    public class ConnectResponse : ServerStatus
    {
        [JsonProperty("hostid")]
        public string HostId { get; internal set; }
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("validated_ledgers")]
        public string ValidatedLedgers { get; internal set; }
    }
    #endregion

    #region RequestServerInfo
    public class ServerInfoResponse
    {
        [JsonProperty("info")]
        public ServerInfo Info { get; internal set; }
    }

    /// <summary>
    /// Represents the server info.
    /// </summary>
    public class ServerInfo
    {
        [JsonProperty("hostid")]
        public string HostId { get; internal set; }
        /// <summary>
        /// The current jingtum version.
        /// </summary>
        [JsonProperty("build_version")]
        public string Version { get; internal set; }
        /// <summary>
        /// The completed ledgers in system.
        /// </summary>
        [JsonProperty("complete_ledgers")]
        public string Ledgers { get; internal set; }
        /// <summary>
        /// The jingtum node id.
        /// </summary>
        [JsonProperty("pubkey_node")]
        public string Node { get; internal set; }
        /// <summary>
        /// The current jingtum node state.
        /// </summary>
        [JsonProperty("server_state")]
        public string State { get; internal set; }
    }
    #endregion

    #region RequestLedgerClosed
    /// <summary>
    /// Represents the closed ledger info.
    /// </summary>
    public class LedgerClosedResponse
    {
        /// <summary>
        /// The index of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; internal set; }
        /// <summary>
        /// The hash of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
    }

    public class LedgerClosedInfo
    {
        /// <summary>
        /// The index of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; internal set; }
        /// <summary>
        /// The hash of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LedgerTime { get; internal set; }
        [JsonProperty("fee_base")]
        public int FeeBase { get; internal set; }
        [JsonProperty("fee_ref")]
        public int FeeRef { get; internal set; }
        [JsonProperty("reserve_base")]
        public int ReserveBase { get; internal set; }
        [JsonProperty("reserve_inc")]
        public int ReserveInc { get; internal set; }
        [JsonProperty("txn_count")]
        public int TxnCount { get; internal set; }
    }
    #endregion

    #region RequestLedger
    public class LedgerResponse
    {
        [JsonProperty("ledger")]
        internal LedgerInfo Current { get; set; }

        [JsonProperty("closed")]
        internal LedgerResponse Closed { get; set; }

        [JsonIgnore]
        public LedgerInfo Ledger
        {
            get
            {
                return Current ?? Closed.Ledger;
            }
        }
    }

    /// <summary>
    /// Represents the ledger info.
    /// </summary>
    public class LedgerInfo
    {
        /// <summary>
        /// Marks the ledger is accepted.
        /// </summary>
        [JsonProperty("accepted")]
        public bool Accepted { get; internal set; }
        [JsonProperty("account_hash")]
        public string AccountHash { get; internal set; }
        /// <summary>
        /// The index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; internal set; }
        /// <summary>
        /// The hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        /// <summary>
        /// The parent ledger hash.
        /// </summary>
        [JsonProperty("parent_hash")]
        public string ParentHash { get; internal set; }
        /// <summary>
        /// The ledger closed time. (UNIX time in UTC+8)
        /// </summary>
        [JsonProperty("close_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; internal set; }
        /// <summary>
        /// The ledger closed time. (human readable string)
        /// </summary>
        [JsonProperty("close_time_human")]
        public string CloseTimeHuman { get; internal set; }
        [JsonProperty("close_time_resolution")]
        public int CloseTimeResolution { get; internal set; }
        [JsonProperty("closed")]
        public Boolean Closed { get; internal set; }
        [JsonProperty("hash")]
        public string Hash { get; internal set; }
        [JsonProperty("seqNum")]
        public string SeqNum { get; internal set; }
        /// <summary>
        /// The total swt in system.
        /// </summary>
        [JsonProperty("total_coins")]
        public string TotalCoins { get; internal set; }
        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; internal set; }

        [JsonProperty("accountState")]
        public AccountState[] AccountStates { get; internal set; }
        [JsonProperty("transactions")]
        public LedgerTransaction[] Transactions { get; internal set; }

    }

    [JsonConverter(typeof(LedgerTransactionConverter))]
    public class LedgerTransaction : Tx
    {
        [JsonProperty("metaData")]
        public Meta Meta { get; internal set; }

        [JsonIgnore]
        public TxResult TxResult { get; internal set; }

        [JsonIgnore]
        public string RawData { get; internal set; }

        [JsonIgnore]
        public bool IsExpanded { get; internal set; }
    }

    public enum AccountStateLedgerEntryType
    {
        Unknown,
        LedgerHashes,
        AccountRoot,
        State,
        FeeSettings,
        DirectoryNode,
        SkywellState,
        ManageIssuer,
        Simple = 100
    }

    [JsonConverter(typeof(LedgerAccountStateConverter))]
    public abstract class AccountState
    {
        [JsonProperty("LedgerEntryType")]
        [JsonConverter(typeof(EnumConverter<AccountStateLedgerEntryType>))]
        public AccountStateLedgerEntryType LedgerEntryType { get; internal set; }
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }
        [JsonProperty("index")]
        public string Index { get; internal set; }

        [JsonIgnore]
        public string RawData { get; internal set; }

        [JsonIgnore]
        public virtual bool IsExpanded { get => true; }
    }

    public class SimpleAccountState : AccountState
    {
        public override bool IsExpanded => false;
    }

    public class LedgerHashesAccountState: AccountState
    {
        [JsonProperty("Hashes")]
        public string[] Hashes { get; internal set; }
        [JsonProperty("LastLedgerSequence")]
        public long LastLedgerSequence { get; internal set; }
    }

    public class AccountRootAccountState : AccountState
    {
        [JsonProperty("Account")]
        public string Account { get; internal set; }
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }
        [JsonProperty("OwnerCount")]
        public int OwnerCount { get; internal set; }
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Payload { get; internal set; }
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }
        [JsonProperty("PreviousTxnLgrSeq")]
        public long PreviousTxnLgrSeq { get; internal set; }
        [JsonProperty("Sequence")]
        public UInt32 Sequence { get; internal set; }
        [JsonProperty("Domain")]
        public string Domain { get; internal set; }
        [JsonProperty("MessageKey")]
        public string MessageKey { get; internal set; }
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }
        [JsonProperty("TransferRate")]
        public int TransferRate { get; internal set; }
    }

    public class StateAccountState : AccountState
    {
        [JsonProperty("info")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Info { get; internal set; }
    }

    public class FeeSettingsAccountState : AccountState
    {
        [JsonProperty("BaseFee")]
        public string BaseFee { get; internal set; }
        [JsonProperty("ReferenceFeeUnits")]
        public int ReferenceFeeUnits { get; internal set; }
        [JsonProperty("ReserveBase")]
        public int ReserveBase { get; internal set; }
        [JsonProperty("ReserveIncrement")]
        public int ReserveIncrement { get; internal set; }
    }

    public class DirectoryNodeAccountState : AccountState
    {
        [JsonProperty("Indexes")]
        public string[] Indexes { get; internal set; }
        [JsonProperty("Owner")]
        public string Owner { get; internal set; }
        [JsonProperty("RootIndex")]
        public string RootIndex { get; internal set; }
    }

    public class SkywellStateAccountState : AccountState
    {
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }
        [JsonProperty("HighLimit")]
        public Amount HighLimit { get; internal set; }
        [JsonProperty("HighNode")]
        public string HighNode { get; internal set; }
        [JsonProperty("LowLimit")]
        public Amount LowLimit { get; internal set; }
        [JsonProperty("LowNode")]
        public string LowNode { get; internal set; }
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }
        [JsonProperty("PreviousTxnLgrSeq")]
        public long PreviousTxnLgrSeq { get; internal set; }
    }

    public class ManageIssuerAccountState : AccountState
    {
        [JsonProperty("IssuerAccountID")]
        public string IssuerAccountID { get; internal set; }
    }

    public class UnknownAccountState : AccountState
    {
        public UnknownAccountState(string entryType)
        {
            RawLedgerEntryType = entryType;
        }

        [JsonIgnore]
        public string RawLedgerEntryType { get; private set; }
    }
    #endregion

    #region RequestTx
    public class TxResponse : Tx
    {
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }

        [JsonIgnore]
        public TxResult TxResult { get; internal set; }
    }
    #endregion

    #region Tx
    public class Tx
    {
        [JsonProperty("Account")]
        public string Account { get; internal set; }
        [JsonProperty("amount")]
        public Amount Amount { get; internal set; }
        [JsonProperty("Destination")]
        public string Destination { get; internal set; }
        [JsonProperty("Fee")]
        public string Fee { get; internal set; }
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }
        [JsonProperty("Memos")]
        public MemoData[] Memos { get; internal set; }
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }
        [JsonProperty("SigningPubKey")]
        public string SigningPubKey { get; internal set; }
        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; internal set; }
        [JsonProperty("TransactionType")]
        public TransactionType TransactionType { get; internal set; }
        [JsonProperty("TxnSignature")]
        public string TxnSignature { get; internal set; }
        [JsonProperty("date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Date { get; internal set; }
        [JsonProperty("hash")]
        public string Hash { get; internal set; }
        [JsonProperty("inLedger")]
        public int InLedger { get; internal set; }
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }

        [JsonProperty("Target")]
        internal string Target { get; set; }
        [JsonProperty("LimitAmount")]
        internal Amount LimitAmount { get; set; }
        [JsonProperty("SendMax")]
        internal Amount SendMax { get; set; }
        [JsonProperty("TakerGets")]
        internal Amount TakerGets { get; set; }
        [JsonProperty("TakerPays")]
        internal Amount TakerPays { get; set; }
        [JsonProperty("RelationType")]
        internal int RelationType { get; set; }
        [JsonProperty("Args")]
        internal ArgData[] Args { get; set; }
        [JsonProperty("Method")]
        internal int Method { get; set; }
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        internal string Payload { get; set; }
        [JsonProperty("ContractMethod")]
        [JsonConverter(typeof(HexToStringConverter))]
        internal string ContractMethod { get; set; }
        [JsonProperty("ClearFlag")]
        internal long ClearFlag { get; set; }
        [JsonProperty("SetFlag")]
        internal long SetFlag { get; set; }
        [JsonProperty("RegularKey")]
        internal string RegularKey { get; set; }
        [JsonProperty("OfferSequence")]
        internal int OfferSequence { get; set; }
    }

    public class ArgData
    {
        [JsonProperty("Arg")]
        public Arg Arg { get; internal set; }
    }

    public class Arg
    {
        [JsonProperty("Parameter")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Parameter { get; internal set; }
    }

    public class MemoData
    {
        [JsonProperty("Memo")]
        public Memo Memo { get; internal set; }
    }

    public class Memo
    {
        [JsonProperty("MemoData")]
        [JsonConverter(typeof(HexToUtf8Converter))]
        public string MemoData { get; internal set; }
    }

    public class Meta
    {
        [JsonProperty("AffectedNodes")]
        public AffectedNode[] AffectedNodes { get; internal set; }
        [JsonProperty("TransactionIndex")]
        public int TransactionIndex { get; internal set; }
        [JsonProperty("TransactionResult")]
        public string TransactionResult { get; internal set; }
    }

    public enum DiffType
    {
        Unknown,
        ModifiedNode,
        DeletedNode,
        CreatedNode
    }

    public class AffectedNode
    {
        [JsonProperty("ModifiedNode")]
        public Node ModifiedNode { get; internal set; }
        [JsonProperty("DeletedNode")]
        public Node DeletedNode { get; internal set; }
        [JsonProperty("CreatedNode")]
        public Node CreatedNode { get; internal set; }

        [JsonIgnore]
        public DiffType DiffType
        {
            get
            {
                if (ModifiedNode != null) return DiffType.ModifiedNode;
                if (DeletedNode != null) return DiffType.DeletedNode;
                if (CreatedNode != null) return DiffType.CreatedNode;
                return DiffType.Unknown;
            }
        }
    }

    public class Node
    {
        [JsonProperty("LedgerEntryType")]
        public string LedgerEntryType { get; internal set; }
        [JsonProperty("LedgerIndex")]
        public string LedgerIndex { get; internal set; }
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }
        [JsonProperty("PreviousTxnLgrSeq")]
        public int PreviousTxnLgrSeq { get; internal set; }
        [JsonProperty("FinalFields")]
        public Fields FinalFields { get; internal set; }
        [JsonProperty("PreviousFields")]
        public Fields PreviousFields { get; internal set; }
        [JsonProperty("NewFields")]
        public Fields NewFields { get; internal set; }
    }

    public class Fields
    {
        [JsonProperty("Account")]
        public string Account { get; internal set; }
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }
        [JsonProperty("Flags")]
        public long? Flags { get; internal set; }
        [JsonProperty("OwnerCount")]
        public int? OwnerCount { get; internal set; }
        [JsonProperty("Sequence")]
        public int? Sequence { get; internal set; }
        [JsonProperty("TakerGets")]
        public Amount TakerGets { get; internal set; }
        [JsonProperty("TakerPays")]
        public Amount TakerPays { get; internal set; }
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }
    }
    #endregion

    #region RequestAccountInfo
    public class AccountInfoResponse
    {
        [JsonProperty("account_data")]
        public AccountRootAccountState AccountData { get; internal set; }
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region RequestAccountTums
    public class AccountTumsResponse
    {
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }
        [JsonProperty("receive_currencies")]
        public string[] ReceivCurrencies { get; internal set; }
        [JsonProperty("send_currencies")]
        public string[] SendCurrencies { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region  RequestAccountRelations
    public class AccountRelationsResponse
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }
        [JsonProperty("lines")]
        public Line[] Lines { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }

    public class Line
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }
        [JsonProperty("balance")]
        public string Balance { get; internal set; }
        [JsonProperty("currency")]
        public string Currency { get; internal set; }
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        [JsonProperty("limit")]
        public string Limit { get; internal set; }
        [JsonProperty("limit_peer")]
        public string LimitPeer { get; internal set; }
        /// <summary>
        /// "1"-Authorize, "3"-Freeze
        /// </summary>
        [JsonProperty("relation_type")]
        public string RelationType { get; set; }
        [JsonProperty("no_skywell")]
        public bool NoSkywell { get; internal set; }
        [JsonProperty("quality_in")]
        public int QualityIn { get; internal set; }
        [JsonProperty("quality_out")]
        public int QualityOut { get; internal set; }
    }
    #endregion

    #region RequestAccountOffers
    public class AccountOffersResponse
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }
        [JsonProperty("offers")]
        public Offer[] Offers { get; internal set; }
        [JsonProperty("marker")]
        public Marker Marker { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }

    public class Offer
    {
        [JsonProperty("flags")]
        public long Flags { get; internal set; }
        [JsonProperty("seq")]
        public int Seq { get; internal set; }
        [JsonProperty("taker_gets")]
        public Amount TakerGets { get; internal set; }
        [JsonProperty("taker_pays")]
        public Amount TakerPays { get; internal set; }

        [JsonIgnore]
        public bool IsSell
        {
            get
            {
                return ((long)LedgerOfferFlags.Sell & Flags) != 0;
            }
        }
    }
    #endregion

    #region  RequestAccountTx
    public class AccountTxResponse
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }
        [JsonProperty("ledger_index_max")]
        public int LedgerIndexMax { get; internal set; }
        [JsonProperty("ledger_index_min")]
        public int LedgerIndexMin { get; internal set; }
        [JsonProperty("marker")]
        public Marker Marker { get; internal set; }
        [JsonProperty("transactions")]
        public AccountTx[] RawTransactions { get; set; }

        [JsonIgnore]
        public TxResult[] Transactions { get; internal set; }
    }

    public class AccountTx
    {
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }
        [JsonProperty("tx")]
        public Tx Tx { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region TxResult
    public enum TxResultType
    {
        Unknown,
        Sent,
        Received,
        Trusted,
        Trusting,
        Convert,
        OfferNew,
        OfferCancel,
        RelationSet,
        RelationDel,
        AccountSet,
        SetRegularKey,
        SignSet,
        Operation,
        OfferEffect,
        ConfigContract
    }

    public abstract class TxResult
    {
        protected TxResult(TxResultType type)
        {
            Type = type;
        }

        public TxResultType Type { get; private set; }
        public DateTime Date { get; internal set; }
        public string Hash { get; internal set; }
        public string Fee { get; internal set; }
        public string Result { get; internal set; }
        public string[] Memos { get; internal set; }
        public NodeEffect[] Effects { get; internal set; }
    }

    public class SentTxResult : TxResult
    {
        public SentTxResult() : base(TxResultType.Sent)
        {

        }
        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
    }

    public class ReceivedTxResult : TxResult
    {
        public ReceivedTxResult() : base(TxResultType.Received)
        {

        }
        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
    }

    public class TrustedTxResult : TxResult
    {
        public TrustedTxResult() : base(TxResultType.Trusted)
        {

        }
        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
    }

    public class TrustingTxResult : TxResult
    {
        public TrustingTxResult() : base(TxResultType.Trusting)
        {

        }
        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
    }

    public class ConvertTxResult : TxResult
    {
        public ConvertTxResult():base(TxResultType.Convert)
        {

        }
        public Amount Spent { get; internal set; }
        public Amount Amount { get; internal set; }
    }

    public class OfferNewTxResult : TxResult
    {
        public OfferNewTxResult() : base(TxResultType.OfferNew)
        {
        }

        public string OfferType { get; internal set; }
        public Amount Gets { get; internal set; }
        public Amount Pays { get; internal set; }
        public int Seq { get; internal set; }
    }

    public class OfferCancelTxResult : TxResult
    {
       public OfferCancelTxResult() : base(TxResultType.OfferCancel)
        {

        }

        public int OfferSeq { get; internal set; }
        public Amount Gets { get; internal set; }
        public Amount Pays { get; internal set; }
    }

    public class RelationSetTxResult : TxResult
    {
        public RelationSetTxResult() : base(TxResultType.RelationSet)
        {

        }

        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
        public RelationType RelationType { get; internal set; }
        public bool IsActive { get; internal set; }
    }

    public class RelationDelTxResult : TxResult
    {
        public RelationDelTxResult() : base(TxResultType.RelationDel)
        {

        }

        public string CounterParty { get; internal set; }
        public Amount Amount { get; internal set; }
        public RelationType? RelationType { get; internal set; }
        public bool IsActive { get; internal set; }
    }

    public class AccountSetTxResult : TxResult
    {
        public AccountSetTxResult() : base(TxResultType.AccountSet)
        {

        }

        public long ClearFlag { get; internal set; }
        public long SetFlag { get; internal set; }
    }

    public class SetRegularKeyTxResult : TxResult
    {
        public SetRegularKeyTxResult() : base(TxResultType.SetRegularKey)
        {

        }

        public string RegularKey { get; internal set; }
    }

    public class SignSetTxResult : TxResult
    {
        public SignSetTxResult() : base(TxResultType.SignSet)
        {

        }
    }

    public class OperationTxResult : TxResult
    {
        public OperationTxResult() : base(TxResultType.Operation)
        {

        }
    }

    public class OfferEffectTxResult : TxResult
    {
        public OfferEffectTxResult() : base(TxResultType.OfferEffect)
        {

        }
    }

    public enum ContractMethod
    {
        Deploy,
        Call
    }

    public abstract class ConfigContractTxResult : TxResult
    {
        protected ConfigContractTxResult(ContractMethod method) : base(TxResultType.ConfigContract)
        {
            Method = method;
        }

        public string[] Params { get; internal set; }
        public ContractMethod Method { get; private set; }
    }

    public class DeployContractTxResult : ConfigContractTxResult
    {
        public DeployContractTxResult() : base(ContractMethod.Deploy)
        {

        }

        public string Payload { get; internal set; }
    }

    public class CallContractTxResult : ConfigContractTxResult
    {
        public CallContractTxResult() : base(ContractMethod.Call)
        {

        }

        public string Destination { get; internal set; }
        public string Foo { get; internal set; }
    }

    public class UnknownTxResult : TxResult
    {
        public UnknownTxResult():base(TxResultType.Unknown)
        {

        }
    }
    #endregion

    #region NodeEffect
    public enum EffectType
    {
        Unknown,
        OfferPartiallyFunded,
        OfferFunded,
        OfferCreated,
        OfferCancelled,
        OfferBought,
        SetRegularKey
    }

    public abstract class NodeEffect
    {
        protected NodeEffect(EffectType effect)
        {
            Effect = effect;
        }
        public EffectType Effect { get; internal set; }
        public bool Deleted { get; internal set; }

    }

    public class CounterParty
    {
        public string Account { get; internal set; }
        public int Seq { get; internal set; }
        public string Hash { get; internal set; }
    }

    public enum OfferEffectType
    {
        Unknown,
        Sell,
        Buy,
        Sold,
        Bought
    }

    public abstract class OfferEffect : NodeEffect
    {
        protected OfferEffect(EffectType effect) : base(effect)
        {
        }

        public OfferEffectType Type { get; internal set; }
        public string Price { get; internal set; }

        internal abstract Amount GotOrPays { get; }
        internal abstract Amount PaidOrGets { get; }
    }

    public class OfferPartiallyFundedEffect : OfferEffect
    {
        public OfferPartiallyFundedEffect() : base(EffectType.OfferPartiallyFunded)
        {
            
        }

        public CounterParty CounterParty { get; internal set; }
        public bool Remaining { get; internal set; }
        public bool Cancelled { get; internal set; }
        public Amount Gets { get; internal set; }
        public Amount Pays { get; internal set; }
        public Amount Got { get; internal set; }
        public Amount Paid { get; internal set; }
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Got ?? Pays;
        internal override Amount PaidOrGets => Paid ?? Gets;
    }

    public class OfferFundedEffect : OfferEffect
    {
        public OfferFundedEffect() : base(EffectType.OfferFunded)
        {

        }

        public CounterParty CounterParty { get; internal set; }
        public Amount Got { get; internal set; }
        public Amount Paid { get; internal set; }
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Got;
        internal override Amount PaidOrGets => Paid;
    }

    public class OfferCreatedEffect : OfferEffect
    {
        public OfferCreatedEffect():base(EffectType.OfferCreated)
        {

        }

        public Amount Gets { get; internal set; }
        public Amount Pays { get; internal set; }
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Pays;
        internal override Amount PaidOrGets => Gets;
    }

    public class OfferCancelledEffect : OfferEffect
    {
        public OfferCancelledEffect() : base(EffectType.OfferCancelled)
        {

        }

        public string Hash { get; internal set; }
        public Amount Gets { get; internal set; }
        public Amount Pays { get; internal set; }
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Pays;
        internal override Amount PaidOrGets => Gets;
    }

    public class OfferBoughtEffect : OfferEffect
    {
        public OfferBoughtEffect() : base(EffectType.OfferBought)
        {

        }

        public CounterParty CounterParty { get; internal set; }
        public Amount Got { get; internal set; }
        public Amount Paid { get; internal set; }

        internal override Amount GotOrPays => Got;
        internal override Amount PaidOrGets => Paid;
    }

    public class SetRegularKeyEffect : NodeEffect
    {
        public SetRegularKeyEffect() : base(EffectType.SetRegularKey)
        {

        }

        public string Type { get; internal set; }
        public string Account { get; internal set; }
        public string RegularKey { get; internal set; }
    }

    public class UnknownEffect : NodeEffect
    {
        public UnknownEffect() : base(EffectType.Unknown)
        {

        }
    }
    #endregion

    #region RequestOrderBook
    public class OrderBookResponse
    {
        [JsonProperty("ledger_current_index")]
        public string LedgerCurrentIndex { get; internal set; }
        [JsonProperty("offers")]
        public OrderBookOffer[] Offers { get; internal set; }
    }

    public class OrderBookOffer
    {
        [JsonProperty("Account")]
        public string Account { get; internal set; }
        [JsonProperty("BookDirectory")]
        public string BookDirectory { get; internal set; }
        [JsonProperty("BookNode")]
        public string BookNode { get; internal set; }
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }
        [JsonProperty("LedgerEntryType")]
        public string LedgerEntryType { get; internal set; }
        [JsonProperty("OwnerNode")]
        public string OwnerNode { get; internal set; }
        [JsonProperty ("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }
        [JsonProperty("PreviousTxnLgrSeq")]
        public int PreviousTxnLgrSeq { get; internal set; }
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }
        [JsonProperty("TakerGets")]
        public Amount TakerGets { get; internal set; }
        [JsonProperty("TakerPays")]
        public Amount TakerPays { get; internal set; }
        [JsonProperty("index")]
        public string Index { get; internal set; }
        [JsonProperty("owner_funds")]
        public string OwnerFunds { get; internal set; }
        [JsonProperty("quality")]
        public string Quality { get; internal set; }

        [JsonIgnore]
        public bool IsSell
        {
            get
            {
                return ((long)LedgerOfferFlags.Sell & Flags) != 0;
            }
        }

        [JsonIgnore]
        public string Price { get; internal set; }
    }
    #endregion

    #region Transaction response base
    public class GeneralTxResponse
    {
        [JsonProperty("engine_result")]
        public string EngineResult { get; internal set; }
        [JsonProperty("engine_result_code")]
        public int EngineResultCode { get; internal set; }
        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; internal set; }
        [JsonProperty("tx_blob")]
        public string TxBlob { get; internal set; }
    }

    public class GeneralTxJson
    {
        [JsonProperty("Account")]
        public string Account { get; internal set; }
        [JsonProperty("Fee")]
        public string Fee { get; internal set; }
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }
        [JsonProperty("Memos")]
        public MemoData[] Memos { get; internal set; }
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }
        [JsonProperty("SigningPubKey")]
        public string SigningPubKey { get; internal set; }
        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; internal set; }
        [JsonProperty("TransactionType")]
        [JsonConverter(typeof(EnumConverter<TransactionType>))]
        public TransactionType TransactionType { get; internal set; }
        [JsonProperty("TxnSignature")]
        public string TxnSignature { get; internal set; }
        [JsonProperty("hash")]
        public string Hash { get; internal set; }
    }
    #endregion

    #region BuildPaymentTx
    public class PaymentTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public PaymentTxJson TxJson { get; internal set; }
    }

    public class PaymentTxJson : GeneralTxJson
    {
        [JsonProperty("amount")]
        public Amount Amount { get; internal set; }
        [JsonProperty("Destination")]
        public string Destination { get; internal set; }
    }
    #endregion

    #region BuildRelationTx
    public class RelationTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public RelationTxJson TxJson { get; internal set; }
    }

    public class RelationTxJson : GeneralTxJson
    {
        [JsonProperty("LimitAmount")]
        public Amount LimitAmount { get; internal set; }
        [JsonProperty("RelationType")]
        [JsonConverter(typeof(NullableEnumConverter<RelationType>))]
        public RelationType? RelationType { get; internal set; }
        [JsonProperty("Target")]
        public string Target { get; internal set; }
    }
    #endregion

    #region BuildAccountSetTx
    public class AccountSetTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public AccountSetTxJson TxJson { get; internal set; }
    }

    public class AccountSetTxJson : GeneralTxJson
    {
        [JsonProperty("SetFlag")]
        [JsonConverter(typeof(NullableEnumConverter<SetClearFlags>))]
        public SetClearFlags? SetFlag { get; internal set; }
        [JsonProperty("ClearFlag")]
        [JsonConverter(typeof(NullableEnumConverter<SetClearFlags>))]
        public SetClearFlags? ClearFlag { get; internal set; }
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }
    }
    #endregion

    #region BuildOfferCreateTx
    public class OfferCreateTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public OfferCreateTxJson TxJson { get; internal set; }
    }

    public class OfferCreateTxJson : GeneralTxJson
    {
        [JsonProperty("TakerGets")]
        public Amount TakerGets { get; internal set; }
        [JsonProperty("TakerPays")]
        public Amount TakerPays { get; internal set; }
    }
    #endregion

    #region DeployContractTx CallContractTx
    public class DeployContractTxResponse : GeneralTxResponse
    {
        [JsonProperty("ContractState")]
        public string ContractState { get; internal set; }
        [JsonProperty("tx_json")]
        public DeployContractTxJson TxJson { get; internal set; }
    }

    public class DeployContractTxJson : GeneralTxJson
    {
        [JsonProperty("Method")]
        public int Method { get; internal set; }
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Payload { get; internal set; }
        [JsonProperty("Args")]
        public ArgData[] Args { get; internal set; }
        [JsonProperty("Amount")]
        public Amount Amount { get; internal set; }
    }
    #endregion

    #region CallContractTx
    public class CallContractTxResponse : GeneralTxResponse
    {
        [JsonProperty("ContractState")]
        public string ContractState { get; internal set; }
        [JsonProperty("tx_json")]
        public CallContractTxJson TxJson { get; internal set; }
    }

    public class CallContractTxJson : GeneralTxJson
    {
        [JsonProperty("Method")]
        public int Method { get; internal set; }
        [JsonProperty("ContractMethod")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string ContractMethod { get; internal set; }
        [JsonProperty("Args")]
        public ArgData[] Args { get; internal set; }
    }
    #endregion

    #region BuildSignTx
    public class SignTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public SignTxJsonResult TxJson { get; internal set; }
    }

    public class SignTxJsonResult : GeneralTxJson
    {

    }
    #endregion

    #region BuildOfferCancelTx
    public class OfferCancelTxResponse : GeneralTxResponse
    {
        [JsonProperty("tx_json")]
        public OfferCancelTxJson TxJson { get; internal set; }
    }

    public class OfferCancelTxJson : GeneralTxJson
    {
        [JsonProperty("OfferSequence")]
        public int OfferSequence { get; internal set; }
    }

    #endregion

    #region RequestPathFind
    public class PathFindResponse
    {
        [JsonProperty("source_account")]
        public string Source { get; internal set; }
        [JsonProperty("destination_account")]
        public string Destination { get; internal set; }
        [JsonProperty("destination_amount")]
        public Amount Amount { get; internal set; }
        [JsonProperty("alternatives")]
        internal PathFindAlternative[] RawAlternatives { get; set; }

        [JsonIgnore]
        public PathFind[] Alternatives { get; internal set; }
    }

    /// <summary>
    /// Represents a path find.
    /// </summary>
    public class PathFind
    {
        /// <summary>
        /// The key of the path find.
        /// </summary>
        public string Key { get; internal set; }
        /// <summary>
        /// The choice amount of the path find.
        /// </summary>
        public Amount Choice { get; internal set; }
    }

    internal class PathFindAlternative
    {
        [JsonProperty("source_amount")]
        public Amount SourceAmount { get; internal set; }
        [JsonProperty("paths_computed")]
        internal PathComputed[][] PathsComputed { get; set; }
    }

    internal class PathComputed
    {
        [JsonProperty("account")]
        public string Account { get; internal set; }
        [JsonProperty("currency")]
        public string Currency { get; internal set; }
        [JsonProperty("issuer")]
        public string Issuer { get; internal set; }
        [JsonProperty("type")]
        public byte? Type { get; internal set; }
        [JsonProperty("type_hex")]
        public string TypeHex { get; internal set; }
    }
    #endregion

    #region Transactions event
    public class TransactionResponse
    {
        [JsonProperty("engine_result")]
        public string EngineResult { get; internal set; }
        [JsonProperty("engine_result_code")]
        public int EngineResultCode { get; internal set; }
        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; internal set; }
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; internal set; }
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
        [JsonProperty("transaction")]
        public Tx Transaction { get; internal set; }
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }

        [JsonIgnore]
        public TxResult TxResult { get; internal set; }
    }
    #endregion
}
