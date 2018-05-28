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
    /// <summary>
    /// Represents the response for <see cref="Remote.Connect(MessageCallback{ConnectResponse})"/> method callback.
    /// </summary>
    public class ConnectResponse : ServerStatus
    {
        /// <summary>
        /// Gets the host id.
        /// </summary>
        [JsonProperty("hostid")]
        public string HostId { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the completed ledgers.
        /// </summary>
        [JsonProperty("validated_ledgers")]
        public string ValidatedLedgers { get; internal set; }
    }
    #endregion

    #region RequestServerInfo
    /// <summary>
    /// Represents the response for <see cref="Remote.RequestServerInfo"/> method.
    /// </summary>
    public class ServerInfoResponse
    {
        /// <summary>
        /// Gets the info of the server.
        /// </summary>
        [JsonProperty("info")]
        public ServerInfo Info { get; internal set; }
    }

    /// <summary>
    /// Represents the server info.
    /// </summary>
    public class ServerInfo
    {
        /// <summary>
        /// Gets the host id.
        /// </summary>
        [JsonProperty("hostid")]
        public string HostId { get; internal set; }

        /// <summary>
        /// Gets the current jingtum version.
        /// </summary>
        [JsonProperty("build_version")]
        public string Version { get; internal set; }

        /// <summary>
        /// Gets the completed ledgers in system.
        /// </summary>
        [JsonProperty("complete_ledgers")]
        public string Ledgers { get; internal set; }

        /// <summary>
        /// Gets the publick key of the node.
        /// </summary>
        [JsonProperty("pubkey_node")]
        public string Node { get; internal set; }

        /// <summary>
        /// Gets the current jingtum node state.
        /// </summary>
        [JsonProperty("server_state")]
        public string State { get; internal set; }
    }
    #endregion

    #region RequestLedgerClosed
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestLedgerClosed"/> method.
    /// </summary>
    public class LedgerClosedResponse
    {
        /// <summary>
        /// Gets the index of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the hash of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }
    }

    /// <summary>
    /// Represents the info of the closed ledger.
    /// </summary>
    public class LedgerClosedInfo
    {
        /// <summary>
        /// Gets the index of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the hash of last closed ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the closed time of the ledger. (in UTC+8)
        /// </summary>
        [JsonProperty("ledger_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LedgerTime { get; internal set; }

        /// <summary>
        /// Gets the base fee for calculation formula factor.
        /// </summary>
        [JsonProperty("fee_base")]
        public int FeeBase { get; internal set; }

        /// <summary>
        /// Gets the reference fee for calculation formula factor.
        /// </summary>
        [JsonProperty("fee_ref")]
        public int FeeRef { get; internal set; }

        /// <summary>
        /// Gets the retention swt value for each Account.
        /// </summary>
        [JsonProperty("reserve_base")]
        public int ReserveBase { get; internal set; }

        /// <summary>
        ///  Gets the freezed swt value for each offer or trust relation.
        /// </summary>
        [JsonProperty("reserve_inc")]
        public int ReserveInc { get; internal set; }

        /// <summary>
        /// Gets the count of the transactions in the ledger.
        /// </summary>
        [JsonProperty("txn_count")]
        public int TxnCount { get; internal set; }
    }
    #endregion

    #region RequestLedger
    /// <summary>
    /// Represents the response of the <see cref="Remote.RequestLedger(LedgerOptions)"/> method.
    /// </summary>
    public class LedgerResponse
    {
        [JsonProperty("ledger")]
        internal LedgerInfo Current { get; set; }

        [JsonProperty("closed")]
        internal LedgerResponse Closed { get; set; }

        /// <summary>
        /// Gets the info of the ledger.
        /// </summary>
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
        /// Gets a boolean value indicating whether the ledger is accepted.
        /// </summary>
        [JsonProperty("accepted")]
        public bool Accepted { get; internal set; }

        /// <summary>
        /// Gets the root of the state hash.
        /// </summary>
        [JsonProperty("account_hash")]
        public string AccountHash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the hash of the previous ledger.
        /// </summary>
        [JsonProperty("parent_hash")]
        public string ParentHash { get; internal set; }

        /// <summary>
        /// Gets the closed time of the ledger. (in UTC+8)
        /// </summary>
        [JsonProperty("close_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; internal set; }

        /// <summary>
        /// Gets the closed time of the ledger. (human readable string)
        /// </summary>
        [JsonProperty("close_time_human")]
        public string CloseTimeHuman { get; internal set; }

        /// <summary>
        /// Gets the time span to close the ledger.
        /// </summary>
        [JsonProperty("close_time_resolution")]
        public int CloseTimeResolution { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the ledger is closed.
        /// </summary>
        [JsonProperty("closed")]
        public Boolean Closed { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; internal set; }

        /// <summary>
        /// Gets the height of ledger.
        /// </summary>
        [JsonProperty("seqNum")]
        public string SeqNum { get; internal set; }

        /// <summary>
        /// Gets the total swt in system.
        /// </summary>
        [JsonProperty("total_coins")]
        public string TotalCoins { get; internal set; }

        /// <summary>
        /// Gets the root hash for the transaction.
        /// </summary>
        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; internal set; }

        /// <summary>
        /// For internal used. Gets the accounts list in this ledger.
        /// </summary>
        [JsonProperty("accountState")]
        public AccountState[] AccountStates { get; internal set; }

        /// <summary>
        /// Gets the transactions list in this ledger.
        /// </summary>
        [JsonProperty("transactions")]
        public TransactionState[] Transactions { get; internal set; }

    }

    /// <summary>
    /// Represents the transaction state in the ledger.
    /// </summary>
    [JsonConverter(typeof(LedgerTransactionConverter))]
    public class TransactionState : Tx
    {
        /// <summary>
        /// Gets the meta data which contains affect nodes.
        /// </summary>
        [JsonProperty("metaData")]
        public Meta Meta { get; internal set; }

        /// <summary>
        /// Gets the result of the transaction.
        /// </summary>
        [JsonIgnore]
        public TxResult TxResult { get; internal set; }

        /// <summary>
        /// Gets the raw json string for this transaction state.
        /// </summary>
        [JsonIgnore]
        public string RawData { get; internal set; }

        /// <summary>
        /// Gets a whether indicating whether this state has expanded info.
        /// </summary>
        /// <remarks>
        /// If this property is false, only the <see cref="Tx.Hash"/> property is valid.
        /// </remarks>
        [JsonIgnore]
        public bool IsExpanded { get; internal set; }
    }

    /// <summary>
    /// Represents the account state.
    /// </summary>
    [JsonConverter(typeof(LedgerAccountStateConverter))]
    public abstract class AccountState
    {
        /// <summary>
        /// Gets the type of the ledger entry.
        /// </summary>
        [JsonProperty("LedgerEntryType")]
        [JsonConverter(typeof(EnumConverter<LedgerEntryType>))]
        public LedgerEntryType LedgerEntryType { get; internal set; }

        /// <summary>
        /// Flags.
        /// </summary>
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }

        /// <summary>
        /// Gets the index hash.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; internal set; }

        /// <summary>
        /// Gets the raw json string for this account state.
        /// </summary>
        [JsonIgnore]
        public string RawData { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the account state has expanded info.
        /// </summary>
        /// <remarks>
        /// Only the <see cref="AccountState.Index"/> is valid if this property is false.
        /// </remarks>
        [JsonIgnore]
        public virtual bool IsExpanded { get => true; }
    }

    /// <summary>
    /// Represents the account state which is not expanded.
    /// </summary>
    public class SimpleAccountState : AccountState
    {
        /// <summary>
        /// Gets a boolean value indicating whether the account state has expanded info.
        /// </summary>
        /// <remarks>
        /// Only the <see cref="AccountState.Index"/> is valid if this property is false.
        /// </remarks>
        public override bool IsExpanded => false;
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.LedgerHashes"/> entry.
    /// </summary>
    public class LedgerHashesAccountState: AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("Hashes")]
        public string[] Hashes { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("LastLedgerSequence")]
        public UInt32 LastLedgerSequence { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.AccountRoot"/> entry.
    /// </summary>
    public class AccountRootAccountState : AccountState
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("Account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the amount of swt.
        /// </summary>
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }

        /// <summary>
        /// Gets the count of offers and trust lines.
        /// </summary>
        [JsonProperty("OwnerCount")]
        public int OwnerCount { get; internal set; }

        /// <summary>
        /// Gets the payload if it's a contract address.
        /// </summary>
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Payload { get; internal set; }

        /// <summary>
        ///Gets the hash of previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }

        /// <summary>
        /// Gets the ledger index for the previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnLgrSeq")]
        public UInt32 PreviousTxnLgrSeq { get; internal set; }

        /// <summary>
        /// Gets the current sequence of the account.
        /// </summary>
        [JsonProperty("Sequence")]
        public UInt32 Sequence { get; internal set; }

        /// <summary>
        /// Gets the domain info.
        /// </summary>
        [JsonProperty("Domain")]
        public string Domain { get; internal set; }

        /// <summary>
        /// Gets the public key used to send encrypted message to this account.
        /// </summary>
        [JsonProperty("MessageKey")]
        public string MessageKey { get; internal set; }

        /// <summary>
        /// Gets the address of the delegate key.
        /// </summary>
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }

        /// <summary>
        /// Gets the rate of transfer fee.
        /// </summary>
        [JsonProperty("TransferRate")]
        public int TransferRate { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.State"/> entry.
    /// </summary>
    public class StateAccountState : AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("info")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Info { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for the <see cref="LedgerEntryType.FeeSettings"/> entry.
    /// </summary>
    public class FeeSettingsAccountState : AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("BaseFee")]
        public string BaseFee { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("ReferenceFeeUnits")]
        public int ReferenceFeeUnits { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("ReserveBase")]
        public int ReserveBase { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("ReserveIncrement")]
        public int ReserveIncrement { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.DirectoryNode"/> entry.
    /// </summary>
    public class DirectoryNodeAccountState : AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("Indexes")]
        public string[] Indexes { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("Owner")]
        public string Owner { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("RootIndex")]
        public string RootIndex { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.SkywellState"/> entry.
    /// </summary>
    public class SkywellStateAccountState : AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("HighLimit")]
        public Amount HighLimit { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("HighNode")]
        public string HighNode { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("LowLimit")]
        public Amount LowLimit { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("LowNode")]
        public string LowNode { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("PreviousTxnLgrSeq")]
        public long PreviousTxnLgrSeq { get; internal set; }
    }

    /// <summary>
    /// Represents the account state for <see cref="LedgerEntryType.ManageIssuer"/> entry.
    /// </summary>
    public class ManageIssuerAccountState : AccountState
    {
        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("IssuerAccountID")]
        public string IssuerAccountID { get; internal set; }
    }

    /// <summary>
    /// Represents the account state which is an unknown enty.
    /// </summary>
    public class UnknownAccountState : AccountState
    {
        internal UnknownAccountState(string entryType)
        {
            RawLedgerEntryType = entryType;
        }

        /// <summary>
        /// Gets the raw string of the ledger entry type.
        /// </summary>
        [JsonIgnore]
        public string RawLedgerEntryType { get; private set; }
    }
    #endregion

    #region RequestTx
    /// <summary>
    /// Represents the response for <see cref="Remote.RequestTx(TxOptions)"/> method.
    /// </summary>
    public class TxResponse : Tx
    {
        /// <summary>
        /// Gets the meta data which contains affect nodes.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the transaction is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }

        /// <summary>
        /// Gets the parsed result of the transaction.
        /// </summary>
        [JsonIgnore]
        public TxResult TxResult { get; internal set; }
    }
    #endregion

    #region Tx
    /// <summary>
    /// Represents the data for a transaction.
    /// </summary>
    public class Tx
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("Account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the transaction amount.
        /// </summary>
        [JsonProperty("amount")]
        public Amount Amount { get; internal set; }

        /// <summary>
        /// Gets the counter party address.
        /// </summary>
        [JsonProperty("Destination")]
        public string Destination { get; internal set; }

        /// <summary>
        /// Gets the transaction fee.
        /// </summary>
        [JsonProperty("Fee")]
        public string Fee { get; internal set; }

        /// <summary>
        /// Gets the transaction flags.
        /// </summary>
        [JsonProperty("Flags")]
        public long Flags { get; internal set; }

        /// <summary>
        /// Gets the list of memos.
        /// </summary>
        [JsonProperty("Memos")]
        public MemoData[] Memos { get; internal set; }

        /// <summary>
        /// Gets the transaction sequence of account.
        /// </summary>
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }

        /// <summary>
        /// Gets public key for signing.
        /// </summary>
        [JsonProperty("SigningPubKey")]
        public string SigningPubKey { get; internal set; }

        /// <summary>
        /// Gets the timestamp of the transaction.
        /// </summary>
        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; internal set; }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        [JsonProperty("TransactionType")]
        public TransactionType TransactionType { get; internal set; }

        /// <summary>
        /// Gets the signature of the transaction.
        /// </summary>
        [JsonProperty("TxnSignature")]
        public string TxnSignature { get; internal set; }

        /// <summary>
        /// Gets the date of validated in the ledger.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Date { get; internal set; }

        /// <summary>
        /// Gets the hash of the transaction.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger which contains this transaction.
        /// </summary>
        [JsonProperty("inLedger")]
        public UInt32 InLedger { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the address of relation target.
        /// </summary>
        [JsonProperty("Target")]
        internal string Target { get; set; }

        /// <summary>
        /// Gets the amount of limit for relation set.
        /// </summary>
        [JsonProperty("LimitAmount")]
        internal Amount LimitAmount { get; set; }

        /// <summary>
        /// Gets the max amount for send.
        /// </summary>
        [JsonProperty("SendMax")]
        internal Amount SendMax { get; set; }

        /// <summary>
        /// Gets the amount of taker gets.
        /// </summary>
        [JsonProperty("TakerGets")]
        internal Amount TakerGets { get; set; }

        /// <summary>
        /// Gets the amount of taker pays.
        /// </summary>
        [JsonProperty("TakerPays")]
        internal Amount TakerPays { get; set; }

        /// <summary>
        /// Gets the type of the relation.
        /// </summary>
        [JsonProperty("RelationType")]
        internal int RelationType { get; set; }

        /// <summary>
        /// Gets the parameters list for contract.
        /// </summary>
        [JsonProperty("Args")]
        internal ArgData[] Args { get; set; }

        /// <summary>
        /// Gets the method of contract config. 0 for deploy, 1 for call.
        /// </summary>
        [JsonProperty("Method")]
        internal int Method { get; set; }

        /// <summary>
        /// Gets the payload of the contract.
        /// </summary>
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        internal string Payload { get; set; }

        /// <summary>
        /// Gets the function name of the contract to call.
        /// </summary>
        [JsonProperty("ContractMethod")]
        [JsonConverter(typeof(HexToStringConverter))]
        internal string ContractMethod { get; set; }

        /// <summary>
        /// Gets the clear flag for account set.
        /// </summary>
        [JsonProperty("ClearFlag")]
        internal long ClearFlag { get; set; }

        /// <summary>
        /// Gets the set flag for account set.
        /// </summary>
        [JsonProperty("SetFlag")]
        internal long SetFlag { get; set; }

        /// <summary>
        /// Gets the address for delegate account set.
        /// </summary>
        [JsonProperty("RegularKey")]
        internal string RegularKey { get; set; }

        /// <summary>
        /// Gets the sequence of the offer to cancel.
        /// </summary>
        [JsonProperty("OfferSequence")]
        internal int OfferSequence { get; set; }
    }

    /// <summary>
    /// Represents the data wrapper for the contract parameter.
    /// </summary>
    public class ArgData
    {
        /// <summary>
        /// Gets the parameter data.
        /// </summary>
        [JsonProperty("Arg")]
        public Arg Arg { get; internal set; }
    }

    /// <summary>
    /// Represents the data for contract parameter.
    /// </summary>
    public class Arg
    {
        /// <summary>
        /// Gets the parameter for contract.
        /// </summary>
        [JsonProperty("Parameter")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Parameter { get; internal set; }
    }

    /// <summary>
    /// Represents the data wrapper for the memo.
    /// </summary>
    public class MemoData
    {
        /// <summary>
        /// Gets the memo data.
        /// </summary>
        [JsonProperty("Memo")]
        public Memo Memo { get; internal set; }
    }

    /// <summary>
    /// Represents the data for memo.
    /// </summary>
    public class Memo
    {
        /// <summary>
        /// Gets the memo data for the transaction.
        /// </summary>
        [JsonProperty("MemoData")]
        [JsonConverter(typeof(HexToUtf8Converter))]
        public string MemoData { get; internal set; }
    }

    /// <summary>
    /// Represents the meta data which contains affect nodes.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets the affect nodes.
        /// </summary>
        [JsonProperty("AffectedNodes")]
        public AffectedNode[] AffectedNodes { get; internal set; }

        /// <summary>
        /// Gets the index of the transaction.
        /// </summary>
        [JsonProperty("TransactionIndex")]
        public int TransactionIndex { get; internal set; }

        /// <summary>
        /// Gets the result of the transaction.
        /// </summary>
        /// <remarks>
        /// "tesSUCCESS" means transaction is applied successfully.
        /// </remarks>
        [JsonProperty("TransactionResult")]
        public string TransactionResult { get; internal set; }
    }

    /// <summary>
    /// Indicates the type of the difference node which is contained in the affect node.
    /// </summary>
    public enum DiffType
    {
        /// <summary>
        /// It's unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Contains the <see cref="AffectedNode.ModifiedNode"/>.
        /// </summary>
        ModifiedNode,
        /// <summary>
        /// Contains the <see cref="AffectedNode.DeletedNode"/>.
        /// </summary>
        DeletedNode,
        /// <summary>
        /// Contains the <see cref="CreatedNode"/>.
        /// </summary>
        CreatedNode
    }

    /// <summary>
    /// Represents the affect node of one transaction.
    /// </summary>
    public class AffectedNode
    {
        /// <summary>
        /// Gets the modified node.
        /// </summary>
        [JsonProperty("ModifiedNode")]
        public Node ModifiedNode { get; internal set; }

        /// <summary>
        /// Gets the deleted node.
        /// </summary>
        [JsonProperty("DeletedNode")]
        public Node DeletedNode { get; internal set; }

        /// <summary>
        /// Gets the created node.
        /// </summary>
        [JsonProperty("CreatedNode")]
        public Node CreatedNode { get; internal set; }

        /// <summary>
        /// Gets the type of the difference node which is contained in this affect node.
        /// </summary>
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

    /// <summary>
    /// Represents the difference node which is containd in the affect node.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Gets the type of the ledger entry. Refer to <see cref="LedgerEntryType"/>.
        /// </summary>
        [JsonProperty("LedgerEntryType")]
        public string LedgerEntryType { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("LedgerIndex")]
        public string LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the hash of the previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }

        /// <summary>
        /// Gets the ledger sequence for the previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnLgrSeq")]
        public UInt32 PreviousTxnLgrSeq { get; internal set; }

        /// <summary>
        /// Gets the fields of the final state.
        /// </summary>
        [JsonProperty("FinalFields")]
        public Fields FinalFields { get; internal set; }

        /// <summary>
        /// Gets the fields of previous state.
        /// </summary>
        [JsonProperty("PreviousFields")]
        public Fields PreviousFields { get; internal set; }

        /// <summary>
        /// Gets the new created fields.
        /// </summary>
        [JsonProperty("NewFields")]
        public Fields NewFields { get; internal set; }
    }

    /// <summary>
    /// Represents the affect node fields.
    /// </summary>
    public class Fields
    {
        /// <summary>
        /// Gets the the account address.
        /// </summary>
        [JsonProperty("Account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the amount of balance.
        /// </summary>
        [JsonProperty("Balance")]
        public Amount Balance { get; internal set; }

        /// <summary>
        /// Gets the flags for offer sell or buy.
        /// </summary>
        [JsonProperty("Flags")]
        public UInt32? Flags { get; internal set; }

        /// <summary>
        /// Gets the count of offers and trust lines.
        /// </summary>
        [JsonProperty("OwnerCount")]
        public int? OwnerCount { get; internal set; }

        /// <summary>
        /// Gets sequence of the account.
        /// </summary>
        [JsonProperty("Sequence")]
        public UInt32? Sequence { get; internal set; }

        /// <summary>
        /// Gets the amount of taker gets.
        /// </summary>
        [JsonProperty("TakerGets")]
        public Amount TakerGets { get; internal set; }

        /// <summary>
        /// Gets the amount of taker pays.
        /// </summary>
        [JsonProperty("TakerPays")]
        public Amount TakerPays { get; internal set; }

        /// <summary>
        /// Gets the hash of previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }

        /// <summary>
        /// Gets the address the delegate key.
        /// </summary>
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }
    }
    #endregion

    #region RequestAccountInfo
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestAccountInfo(AccountInfoOptions)"/> method.
    /// </summary>
    public class AccountInfoResponse
    {
        /// <summary>
        /// Gets account state for the account root.
        /// </summary>
        [JsonProperty("account_data")]
        public AccountRootAccountState AccountData { get; internal set; }

        /// <summary>
        /// Gets the hash in the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the ledger is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region RequestAccountTums

    /// <summary>
    /// Represents the response of <see cref="Remote.RequestAccountTums(AccountTumsOptions)"/> method.
    /// </summary>
    public class AccountTumsResponse
    {
        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the list of receive currencies.
        /// </summary>
        [JsonProperty("receive_currencies")]
        public string[] ReceiveCurrencies { get; internal set; }

        /// <summary>
        /// Gets the list of send currencies.
        /// </summary>
        [JsonProperty("send_currencies")]
        public string[] SendCurrencies { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the ledger is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region  RequestAccountRelations
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestAccountRelations(AccountRelationsOptions)"/> method.
    /// </summary>
    public class AccountRelationsResponse
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the trust lines of this account.
        /// </summary>
        [JsonProperty("lines")]
        public Line[] Lines { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the ledger is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }

    /// <summary>
    /// Represents the relation line.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the amount of balance.
        /// </summary>
        [JsonProperty("balance")]
        public string Balance { get; internal set; }

        /// <summary>
        /// Gets the kind of currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; internal set; }

        /// <summary>
        /// Gets the address of the issuer.
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets the limit amount of trusting.
        /// </summary>
        [JsonProperty("limit")]
        public string Limit { get; internal set; }

        /// <summary>
        /// Gets the limit amount which is set by counter party.
        /// </summary>
        [JsonProperty("limit_peer")]
        public string LimitPeer { get; internal set; }

        /// <summary>
        /// Gets the type of the relation.
        /// </summary>
        /// <remarks>
        /// 1-Authorize, 3-Freeze
        /// </remarks>
        [JsonProperty("relation_type")]
        public int RelationType { get; set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("no_skywell")]
        public bool NoSkywell { get; internal set; }

        /// <summary>
        /// Reserved. Gets the exchange rate.
        /// </summary>
        [JsonProperty("quality_in")]
        public int QualityIn { get; internal set; }

        /// <summary>
        /// Reserved. Gets the exchange rate.
        /// </summary>
        [JsonProperty("quality_out")]
        public int QualityOut { get; internal set; }
    }
    #endregion

    #region RequestAccountOffers
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestAccountOffers(AccountOffersOptions)"/> method.
    /// </summary>
    public class AccountOffersResponse
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the hash of the ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the list of offers.
        /// </summary>
        [JsonProperty("offers")]
        public Offer[] Offers { get; internal set; }

        /// <summary>
        /// Gets the marker.
        /// </summary>
        [JsonProperty("marker")]
        public Marker Marker { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the ledger is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }

    /// <summary>
    /// Represents the offer.
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Gets the type of the offer.
        /// </summary>
        /// <remarks>
        /// 131072 for buy, otherwise for sell.
        /// </remarks>
        [JsonProperty("flags")]
        public long Flags { get; internal set; }

        /// <summary>
        /// Gets the sequence of the offfer transaction.
        /// </summary>
        [JsonProperty("seq")]
        public int Seq { get; internal set; }

        /// <summary>
        /// Gets the amount of taker gets.
        /// </summary>
        [JsonProperty("taker_gets")]
        public Amount TakerGets { get; internal set; }

        /// <summary>
        /// Gets the amount of taker gets.
        /// </summary>
        [JsonProperty("taker_pays")]
        public Amount TakerPays { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether it's a sell offer.
        /// </summary>
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
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestAccountTx(AccountTxOptions)"/> method.
    /// </summary>
    public class AccountTxResponse
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the index of max ledger in current node.
        /// </summary>
        [JsonProperty("ledger_index_max")]
        public UInt32 LedgerIndexMax { get; internal set; }

        /// <summary>
        /// Gets the index of min ledger in current node.
        /// </summary>
        [JsonProperty("ledger_index_min")]
        public UInt32 LedgerIndexMin { get; internal set; }

        /// <summary>
        /// Gets the marker of current record.
        /// </summary>
        [JsonProperty("marker")]
        public Marker Marker { get; internal set; }

        /// <summary>
        /// Gets the list of transactions.
        /// </summary>
        [JsonProperty("transactions")]
        public AccountTx[] RawTransactions { get; set; }

        /// <summary>
        /// Gets the list of parsed transaction results.
        /// </summary>
        [JsonIgnore]
        public TxResult[] Transactions { get; internal set; }
    }

    /// <summary>
    /// Reprecents the info for account transaction.
    /// </summary>
    public class AccountTx
    {
        /// <summary>
        /// Gets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }

        /// <summary>
        /// Gets the transaction data.
        /// </summary>
        [JsonProperty("tx")]
        public Tx Tx { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether it's validated in the ledger.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }
    }
    #endregion

    #region TxResult
    /// <summary>
    /// Indicates the type of the transaction.
    /// </summary>
    public enum TxResultType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Send to counter party.
        /// </summary>
        Sent,
        /// <summary>
        /// Received from counter party.
        /// </summary>
        Received,
        /// <summary>
        /// Trusted with counter party.
        /// </summary>
        Trusted,
        /// <summary>
        /// Trusting with counter party.
        /// </summary>
        Trusting,
        /// <summary>
        /// Convert.
        /// </summary>
        Convert,
        /// <summary>
        /// Create new offer.
        /// </summary>
        OfferNew,
        /// <summary>
        /// Offer is cancelled.
        /// </summary>
        OfferCancel,
        /// <summary>
        /// Set relation.
        /// </summary>
        RelationSet,
        /// <summary>
        /// Delete relation.
        /// </summary>
        RelationDel,
        /// <summary>
        /// Set account property.
        /// </summary>
        AccountSet,
        /// <summary>
        /// Set delegate key.
        /// </summary>
        SetRegularKey,
        /// <summary>
        /// SignSet.
        /// </summary>
        SignSet,
        /// <summary>
        /// Operation.
        /// </summary>
        Operation,
        /// <summary>
        /// Effect by other's offer.
        /// </summary>
        OfferEffect,
        /// <summary>
        /// Deploy or call contract.
        /// </summary>
        ConfigContract
    }

    /// <summary>
    /// Represents the result of one transaction.
    /// </summary>
    public abstract class TxResult
    {
        internal TxResult(TxResultType type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the type of the transaction result.
        /// </summary>
        public TxResultType Type { get; private set; }

        /// <summary>
        /// Gets the timestamp of the transaction.
        /// </summary>
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Gets the hash of the transaction.
        /// </summary>
        public string Hash { get; internal set; }

        /// <summary>
        /// Gets the fee of the transaction.
        /// </summary>
        public string Fee { get; internal set; }

        /// <summary>
        /// Gets the result of the transaciton.
        /// </summary>
        /// <remarks>
        /// "tesSUCCESS" means the transaction is applied successfully.
        /// </remarks>
        public string Result { get; internal set; }

        /// <summary>
        /// Gets the list of memos.
        /// </summary>
        public string[] Memos { get; internal set; }

        /// <summary>
        /// Gets the list of effects.
        /// </summary>
        public NodeEffect[] Effects { get; internal set; }
    }

    /// <summary>
    /// Represents the transaction result for send to counter party.
    /// </summary>
    public class SentTxResult : TxResult
    {
        internal SentTxResult() : base(TxResultType.Sent)
        {
        }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount of the payment.
        /// </summary>
        public Amount Amount { get; internal set; }
    }

    /// <summary>
    /// Represents the transaction result for received from counter party.
    /// </summary>
    public class ReceivedTxResult : TxResult
    {
        internal ReceivedTxResult() : base(TxResultType.Received)
        {
        }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount of payment.
        /// </summary>
        public Amount Amount { get; internal set; }
    }

    /// <summary>
    /// Represents the result for trusted transaction.
    /// </summary>
    public class TrustedTxResult : TxResult
    {
        internal TrustedTxResult() : base(TxResultType.Trusted)
        {
        }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount for trusting.
        /// </summary>
        public Amount Amount { get; internal set; }
    }

    /// <summary>
    /// Represents the result for trusting transaction.
    /// </summary>
    public class TrustingTxResult : TxResult
    {
        internal TrustingTxResult() : base(TxResultType.Trusting)
        {
        }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount for trusting.
        /// </summary>
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
