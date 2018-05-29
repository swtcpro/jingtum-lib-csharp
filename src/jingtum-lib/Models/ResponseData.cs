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

    /// <summary>
    /// Represents the result for Convert transaction.
    /// </summary>
    public class ConvertTxResult : TxResult
    {
        internal ConvertTxResult():base(TxResultType.Convert)
        {

        }

        /// <summary>
        /// Gets the amount of spent.
        /// </summary>
        public Amount Spent { get; internal set; }

        /// <summary>
        /// Gets the amount to convert.
        /// </summary>
        public Amount Amount { get; internal set; }
    }

    /// <summary>
    /// Represents the result of OfferNew transaction.
    /// </summary>
    public class OfferNewTxResult : TxResult
    {
        internal OfferNewTxResult() : base(TxResultType.OfferNew)
        {
        }

        /// <summary>
        /// Gets the type of offer.
        /// </summary>
        public OfferType OfferType { get; internal set; }

        /// <summary>
        /// Gets the amount of gets.
        /// </summary>
        public Amount Gets { get; internal set; }

        /// <summary>
        /// Gets the amount of pays.
        /// </summary>
        public Amount Pays { get; internal set; }

        /// <summary>
        /// Gets the sequence of the offer.
        /// </summary>
        public int Seq { get; internal set; }
    }

    /// <summary>
    /// Represents the result of OfferCancel transaction.
    /// </summary>
    public class OfferCancelTxResult : TxResult
    {
        internal OfferCancelTxResult() : base(TxResultType.OfferCancel)
        {

        }

        /// <summary>
        /// Gets the sequence of the offer.
        /// </summary>
        public int OfferSeq { get; internal set; }

        /// <summary>
        /// Gets the amount of gets.
        /// </summary>
        public Amount Gets { get; internal set; }

        /// <summary>
        /// Gets the amount of pays.
        /// </summary>
        public Amount Pays { get; internal set; }
    }

    /// <summary>
    /// Represents the result of RelationSet transaction.
    /// </summary>
    public class RelationSetTxResult : TxResult
    {
        internal RelationSetTxResult() : base(TxResultType.RelationSet)
        {
        }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount for the relation.
        /// </summary>
        public Amount Amount { get; internal set; }

        /// <summary>
        /// Gets the type of relation.
        /// </summary>
        public RelationType RelationType { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the relation is active.
        /// </summary>
        public bool IsActive { get; internal set; }
    }

    /// <summary>
    /// Represents the result of RelationDel transaction.
    /// </summary>
    public class RelationDelTxResult : TxResult
    {
        internal RelationDelTxResult() : base(TxResultType.RelationDel)
        {
        }

        /// <summary>
        /// Gets the address of the counter party.
        /// </summary>
        public string CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount for the relation.
        /// </summary>
        public Amount Amount { get; internal set; }

        /// <summary>
        /// Gets the type of the relation.
        /// </summary>
        public RelationType? RelationType { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the relation is active.
        /// </summary>
        public bool IsActive { get; internal set; }
    }

    /// <summary>
    /// Represents the result of set account property transaction.
    /// </summary>
    public class AccountSetTxResult : TxResult
    {
        internal AccountSetTxResult() : base(TxResultType.AccountSet)
        {
        }

        /// <summary>
        /// Gets the flag (<see cref="SetClearFlags"/>) to clear.
        /// </summary>
        public long ClearFlag { get; internal set; }

        /// <summary>
        /// Gets the flag (<see cref="SetClearFlags"/>) to set.
        /// </summary>
        public long SetFlag { get; internal set; }
    }

    /// <summary>
    /// Represents the result of set account delegate transaction.
    /// </summary>
    public class SetRegularKeyTxResult : TxResult
    {
        internal SetRegularKeyTxResult() : base(TxResultType.SetRegularKey)
        {
        }

        /// <summary>
        /// Gets the address of the delegate key.
        /// </summary>
        public string RegularKey { get; internal set; }
    }

    /// <summary>
    /// Represents the result of set account signer transaction.
    /// </summary>
    public class SignSetTxResult : TxResult
    {
        internal SignSetTxResult() : base(TxResultType.SignSet)
        {
        }
    }

    /// <summary>
    /// Represents the result of Operation transaction.
    /// </summary>
    public class OperationTxResult : TxResult
    {
        internal OperationTxResult() : base(TxResultType.Operation)
        {
        }
    }

    /// <summary>
    /// Represents the result of effect by other transaction.
    /// </summary>
    public class OfferEffectTxResult : TxResult
    {
        internal OfferEffectTxResult() : base(TxResultType.OfferEffect)
        {
        }
    }

    /// <summary>
    /// Indicates the method of the ConfigContract transaction.
    /// </summary>
    public enum ContractMethod
    {
        /// <summary>
        /// Depoly a contract.
        /// </summary>
        Deploy,
        /// <summary>
        /// Call a contract.
        /// </summary>
        Call
    }

    /// <summary>
    /// Represents the result of deploy of call contract transaction.
    /// </summary>
    public abstract class ConfigContractTxResult : TxResult
    {
        internal ConfigContractTxResult(ContractMethod method) : base(TxResultType.ConfigContract)
        {
            Method = method;
        }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        public string[] Params { get; internal set; }

        /// <summary>
        /// Gets the method of the ConfigContract transaction.
        /// </summary>
        public ContractMethod Method { get; private set; }
    }

    /// <summary>
    /// Represents the result of deploy contract transaction.
    /// </summary>
    public class DeployContractTxResult : ConfigContractTxResult
    {
        internal DeployContractTxResult() : base(ContractMethod.Deploy)
        {
        }

        /// <summary>
        /// Gets the payload of the contract to deploy.
        /// </summary>
        public string Payload { get; internal set; }
    }

    /// <summary>
    /// Represents the result of call contract transaction.
    /// </summary>
    public class CallContractTxResult : ConfigContractTxResult
    {
        internal CallContractTxResult() : base(ContractMethod.Call)
        {
        }

        /// <summary>
        /// Gets address of the contract to call.
        /// </summary>
        public string Destination { get; internal set; }

        /// <summary>
        /// Gets the function name to call.
        /// </summary>
        public string Foo { get; internal set; }
    }

    /// <summary>
    /// Represents the result of a transaction which type is unknown.
    /// </summary>
    public class UnknownTxResult : TxResult
    {
        internal UnknownTxResult():base(TxResultType.Unknown)
        {
        }
    }
    #endregion

    #region NodeEffect
    /// <summary>
    /// Indicates the type of transaction effect.
    /// </summary>
    public enum EffectType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Offer is partially funded, and has remained to offer.
        /// </summary>
        OfferPartiallyFunded,
        /// <summary>
        /// Offer is funded and finished.
        /// </summary>
        OfferFunded,
        /// <summary>
        /// Offer is created but not funded.
        /// </summary>
        OfferCreated,
        /// <summary>
        /// Offer is cancelled.
        /// </summary>
        OfferCancelled,
        /// <summary>
        /// Offer is bought on creating.
        /// </summary>
        OfferBought,
        /// <summary>
        /// Is delegated with other account.
        /// </summary>
        SetRegularKey
    }

    /// <summary>
    /// Represents the effect of a Node.
    /// </summary>
    public abstract class NodeEffect
    {
        internal NodeEffect(EffectType effect)
        {
            Effect = effect;
        }

        /// <summary>
        /// Gets the type of the effect.
        /// </summary>
        public EffectType Effect { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the node is deleted.
        /// </summary>
        public bool Deleted { get; internal set; }

    }

    /// <summary>
    /// Represents the info a the counter party.
    /// </summary>
    public class CounterParty
    {
        /// <summary>
        /// Gets the address of the counter party.
        /// </summary>
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the sequence of the counter party's transaction.
        /// </summary>
        public int Seq { get; internal set; }

        /// <summary>
        /// Gets the hash of the counter party's transaction.
        /// </summary>
        public string Hash { get; internal set; }
    }

    /// <summary>
    /// Indicates the type of the offer effect.
    /// </summary>
    public enum OfferEffectType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Sell offer.
        /// </summary>
        Sell,
        /// <summary>
        /// Buy offer.
        /// </summary>
        Buy,
        /// <summary>
        /// Offer is sold.
        /// </summary>
        Sold,
        /// <summary>
        /// Offer is bought.
        /// </summary>
        Bought
    }

    /// <summary>
    /// Represents the effect of offers.
    /// </summary>
    public abstract class OfferEffect : NodeEffect
    {
        internal OfferEffect(EffectType effect) : base(effect)
        {
        }

        /// <summary>
        /// Gets the type of the offer effect.
        /// </summary>
        public OfferEffectType Type { get; internal set; }

        /// <summary>
        /// Gets the price of the offer.
        /// </summary>
        public string Price { get; internal set; }

        internal abstract Amount GotOrPays { get; }
        internal abstract Amount PaidOrGets { get; }
    }

    /// <summary>
    /// Represents the effet of offer partially funded.
    /// </summary>
    public class OfferPartiallyFundedEffect : OfferEffect
    {
        internal OfferPartiallyFundedEffect() : base(EffectType.OfferPartiallyFunded)
        {
        }

        /// <summary>
        /// Gets the counter party info.
        /// </summary>
        public CounterParty CounterParty { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the offer is not finished.
        /// </summary>
        public bool Remaining { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the offer is cancelled.
        /// </summary>
        public bool Cancelled { get; internal set; }

        /// <summary>
        /// Gets the amount of gets for the offer.
        /// </summary>
        public Amount Gets { get; internal set; }

        /// <summary>
        /// Gets the amount of pays for the offer.
        /// </summary>
        public Amount Pays { get; internal set; }

        /// <summary>
        /// Gets the amount of got for this effect.
        /// </summary>
        public Amount Got { get; internal set; }

        /// <summary>
        /// gets the amount of paid for this effect.
        /// </summary>
        public Amount Paid { get; internal set; }

        /// <summary>
        /// Gets the sequnce of the offer.
        /// </summary>
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Got ?? Pays;
        internal override Amount PaidOrGets => Paid ?? Gets;
    }

    /// <summary>
    /// Represents the effect of offer funded.
    /// </summary>
    public class OfferFundedEffect : OfferEffect
    {
        internal OfferFundedEffect() : base(EffectType.OfferFunded)
        {
        }

        /// <summary>
        /// Gets the counter party info.
        /// </summary>
        public CounterParty CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount of got for this effect.
        /// </summary>
        public Amount Got { get; internal set; }

        /// <summary>
        /// Gets the amount of paid for this effect.
        /// </summary>
        public Amount Paid { get; internal set; }

        /// <summary>
        /// Gets the sequnce of the offer.
        /// </summary>
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Got;
        internal override Amount PaidOrGets => Paid;
    }

    /// <summary>
    /// Represents the effect of offer created.
    /// </summary>
    public class OfferCreatedEffect : OfferEffect
    {
        internal  OfferCreatedEffect():base(EffectType.OfferCreated)
        {
        }

        /// <summary>
        /// Gets the amount of gets for the offer.
        /// </summary>
        public Amount Gets { get; internal set; }

        /// <summary>
        /// Gets the amount of pays for the offer.
        /// </summary>
        public Amount Pays { get; internal set; }

        /// <summary>
        /// Gets the sequnce of the offfer.
        /// </summary>
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Pays;
        internal override Amount PaidOrGets => Gets;
    }

    /// <summary>
    /// Represents the effect of offer cancelled.
    /// </summary>
    public class OfferCancelledEffect : OfferEffect
    {
        internal OfferCancelledEffect() : base(EffectType.OfferCancelled)
        {
        }

        /// <summary>
        /// Gets the amount of gets for the offer.
        /// </summary>
        public Amount Gets { get; internal set; }

        /// <summary>
        /// Gets the amount of pays for the offer.
        /// </summary>
        public Amount Pays { get; internal set; }

        /// <summary>
        /// Gets the sequence of the offer.
        /// </summary>
        public int Seq { get; internal set; }

        internal override Amount GotOrPays => Pays;
        internal override Amount PaidOrGets => Gets;
    }

    /// <summary>
    /// Represents the effect of offer bought.
    /// </summary>
    public class OfferBoughtEffect : OfferEffect
    {
        internal OfferBoughtEffect() : base(EffectType.OfferBought)
        {
        }

        /// <summary>
        /// Gets the counter party info.
        /// </summary>
        public CounterParty CounterParty { get; internal set; }

        /// <summary>
        /// Gets the amount of got for the offer.
        /// </summary>
        public Amount Got { get; internal set; }

        /// <summary>
        /// Gets the amount of paid for the offer.
        /// </summary>
        public Amount Paid { get; internal set; }

        internal override Amount GotOrPays => Got;
        internal override Amount PaidOrGets => Paid;
    }

    /// <summary>
    /// Represents the effect of set delegate key.
    /// </summary>
    public class SetRegularKeyEffect : NodeEffect
    {
        internal SetRegularKeyEffect() : base(EffectType.SetRegularKey)
        {
        }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the address of delegate.
        /// </summary>
        public string RegularKey { get; internal set; }
    }

    /// <summary>
    /// Represents the effect which is unknown.
    /// </summary>
    public class UnknownEffect : NodeEffect
    {
        internal UnknownEffect() : base(EffectType.Unknown)
        {

        }
    }
    #endregion

    #region RequestOrderBook
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestOrderBook(OrderBookOptions)"/> method.
    /// </summary>
    public class OrderBookResponse
    {
        /// <summary>
        /// Gets the index of the current ledger.
        /// </summary>
        [JsonProperty("ledger_current_index")]
        public UInt32 LedgerCurrentIndex { get; internal set; }

        /// <summary>
        /// Represents the list of offers.
        /// </summary>
        [JsonProperty("offers")]
        public OrderBookOffer[] Offers { get; internal set; }
    }

    /// <summary>
    /// Represents the offer of order book.
    /// </summary>
    public class OrderBookOffer
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("Account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the hash of book directory.
        /// </summary>
        [JsonProperty("BookDirectory")]
        public string BookDirectory { get; internal set; }

        /// <summary>
        /// Ges the book node.
        /// </summary>
        [JsonProperty("BookNode")]
        public string BookNode { get; internal set; }

        /// <summary>
        /// Gets the flags of bug or sell.
        /// </summary>
        /// <remarks>
        /// 0x20000 means sell.
        /// </remarks>
        [JsonProperty("Flags")]
        public UInt32 Flags { get; internal set; }

        /// <summary>
        /// Gets the type of ledger entry.
        /// </summary>
        [JsonProperty("LedgerEntryType")]
        public string LedgerEntryType { get; internal set; }

        /// <summary>
        /// Gets the count of offers and freeze lines.
        /// </summary>
        [JsonProperty("OwnerNode")]
        public string OwnerNode { get; internal set; }

        /// <summary>
        /// Gets the hash of previous transaction.
        /// </summary>
        [JsonProperty ("PreviousTxnID")]
        public string PreviousTxnID { get; internal set; }

        /// <summary>
        /// Gets the ledger index which contains the previous transaction.
        /// </summary>
        [JsonProperty("PreviousTxnLgrSeq")]
        public int PreviousTxnLgrSeq { get; internal set; }

        /// <summary>
        /// Gets the sequence of the offer.
        /// </summary>
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }

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
        /// Gets the hash of the index for the data.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; internal set; }

        /// <summary>
        /// Gets the swt balance of the account.
        /// </summary>
        [JsonProperty("owner_funds")]
        public string OwnerFunds { get; internal set; }

        /// <summary>
        /// Gets the price of the reciprocal value of price.
        /// </summary>
        [JsonProperty("quality")]
        public string Quality { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the offer is sell.
        /// </summary>
        [JsonIgnore]
        public bool IsSell
        {
            get
            {
                return ((long)LedgerOfferFlags.Sell & Flags) != 0;
            }
        }

        /// <summary>
        /// Gets the price for each sell of taker gets or buy of taker pays.
        /// </summary>
        [JsonIgnore]
        public string Price { get; internal set; }
    }
    #endregion

    #region Transaction response base
    /// <summary>
    /// Represents the response for transaction submits.
    /// </summary>
    public class GeneralTxResponse
    {
        /// <summary>
        /// Ges the result of the submit.
        /// </summary>
        /// <remarks>
        /// "tesSUCCESS" means transaction is applied successfully.
        /// </remarks>
        [JsonProperty("engine_result")]
        public string EngineResult { get; internal set; }

        /// <summary>
        /// Gets the result code of the submit.
        /// </summary>
        [JsonProperty("engine_result_code")]
        public int EngineResultCode { get; internal set; }

        /// <summary>
        /// Gets the result message of the submit.
        /// </summary>
        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; internal set; }

        /// <summary>
        /// Gets the blob after signing for the transaciton.
        /// </summary>
        [JsonProperty("tx_blob")]
        public string TxBlob { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for the transaction.
    /// </summary>
    public class GeneralTxJson
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("Account")]
        public string Account { get; internal set; }

        /// <summary>
        /// Gets the transaction fee.
        /// </summary>
        [JsonProperty("Fee")]
        public string Fee { get; internal set; }

        /// <summary>
        /// Gets the flags for the transaction.
        /// </summary>
        [JsonProperty("Flags")]
        public UInt32 Flags { get; internal set; }

        /// <summary>
        /// Gets the list of memos.
        /// </summary>
        [JsonProperty("Memos")]
        public MemoData[] Memos { get; internal set; }

        /// <summary>
        /// Gets the sequence of the transaction.
        /// </summary>
        [JsonProperty("Sequence")]
        public int Sequence { get; internal set; }

        /// <summary>
        /// Gets the public key for signing.
        /// </summary>
        [JsonProperty("SigningPubKey")]
        public string SigningPubKey { get; internal set; }

        /// <summary>
        /// Gets the timestamp of the transaction.
        /// </summary>
        [JsonProperty("Timestamp")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; internal set; }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        [JsonProperty("TransactionType")]
        [JsonConverter(typeof(EnumConverter<TransactionType>))]
        public TransactionType TransactionType { get; internal set; }

        /// <summary>
        /// Gets the signature of the transaction.
        /// </summary>
        [JsonProperty("TxnSignature")]
        public string TxnSignature { get; internal set; }

        /// <summary>
        /// Gets the hash of the transaction.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; internal set; }
    }
    #endregion

    #region BuildPaymentTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildPaymentTx(PaymentTxOptions)"/> method.
    /// </summary>
    public class PaymentTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public PaymentTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for payment transaction.
    /// </summary>
    public class PaymentTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the amount of payment.
        /// </summary>
        [JsonProperty("amount")]
        public Amount Amount { get; internal set; }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        [JsonProperty("Destination")]
        public string Destination { get; internal set; }
    }
    #endregion

    #region BuildRelationTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildRelationTx(RelationTxOptions)"/> method.
    /// </summary>
    public class RelationTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public RelationTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for relation type of transactions.
    /// </summary>
    public class RelationTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the amount of limit for the relation.
        /// </summary>
        [JsonProperty("LimitAmount")]
        public Amount LimitAmount { get; internal set; }

        /// <summary>
        /// Gets the type of relation.
        /// </summary>
        [JsonProperty("RelationType")]
        [JsonConverter(typeof(NullableEnumConverter<RelationType>))]
        public RelationType? RelationType { get; internal set; }

        /// <summary>
        /// Ges the address of relation target.
        /// </summary>
        [JsonProperty("Target")]
        public string Target { get; internal set; }
    }
    #endregion

    #region BuildAccountSetTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildAccountSetTx(AccountSetTxOptions)"/> method.
    /// </summary>
    public class AccountSetTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public AccountSetTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for type of account set transactions.
    /// </summary>
    public class AccountSetTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the flag for set if exists.
        /// </summary>
        [JsonProperty("SetFlag")]
        [JsonConverter(typeof(NullableEnumConverter<SetClearFlags>))]
        public SetClearFlags? SetFlag { get; internal set; }

        /// <summary>
        /// Gets the flag for clear if exists.
        /// </summary>
        [JsonProperty("ClearFlag")]
        [JsonConverter(typeof(NullableEnumConverter<SetClearFlags>))]
        public SetClearFlags? ClearFlag { get; internal set; }

        /// <summary>
        /// Gets the address the delegate key.
        /// </summary>
        [JsonProperty("RegularKey")]
        public string RegularKey { get; internal set; }
    }
    #endregion

    #region BuildOfferCreateTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildOfferCreateTx(OfferCreateTxOptions)"/> method.
    /// </summary>
    public class OfferCreateTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public OfferCreateTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for offer create transaction.
    /// </summary>
    public class OfferCreateTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the amount taker gets.
        /// </summary>
        [JsonProperty("TakerGets")]
        public Amount TakerGets { get; internal set; }

        /// <summary>
        /// Gets the amount of taker pays.
        /// </summary>
        [JsonProperty("TakerPays")]
        public Amount TakerPays { get; internal set; }
    }
    #endregion

    #region DeployContractTx CallContractTx
    /// <summary>
    /// Represents the response of <see cref="Remote.DeployContractTx(DeployContractTxOptions)"/> method.
    /// </summary>
    public class DeployContractTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the state of the contract.
        /// </summary>
        /// <remarks>
        /// It's the address of the contract.
        /// </remarks>
        [JsonProperty("ContractState")]
        public string ContractState { get; internal set; }

        /// <summary>
        /// Gets json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public DeployContractTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for deploy contract transaction.
    /// </summary>
    public class DeployContractTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the method of the ConfigContract transaction.
        /// </summary>
        /// <remarks>
        /// 0 for deploy contract, 1 for call contract.
        /// </remarks>
        [JsonProperty("Method")]
        public int Method { get; internal set; }

        /// <summary>
        /// Gets the payload of the contract.
        /// </summary>
        [JsonProperty("Payload")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string Payload { get; internal set; }

        /// <summary>
        /// Gets the list of the parameters.
        /// </summary>
        [JsonProperty("Args")]
        public ArgData[] Args { get; internal set; }

        /// <summary>
        /// Gets the amount to active the contract address.
        /// </summary>
        /// <remarks>
        /// At least 35 SWT.
        /// </remarks>
        [JsonProperty("Amount")]
        public Amount Amount { get; internal set; }
    }
    #endregion

    #region CallContractTx
    /// <summary>
    /// Represents the response of <see cref="Remote.CallContractTx(CallContractTxOptions)"/> method.
    /// </summary>
    public class CallContractTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the state of the contract.
        /// </summary>
        /// <remarks>
        /// It's the result of contract calling.
        /// </remarks>
        [JsonProperty("ContractState")]
        public string ContractState { get; internal set; }

        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public CallContractTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for call contract transaction.
    /// </summary>
    public class CallContractTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the method of the ConfigContract transaction.
        /// </summary>
        /// <remarks>
        /// 0 for deploy contract, 1 for call contract.
        /// </remarks>
        [JsonProperty("Method")]
        public int Method { get; internal set; }

        /// <summary>
        /// Gets the function name to call.
        /// </summary>
        [JsonProperty("ContractMethod")]
        [JsonConverter(typeof(HexToStringConverter))]
        public string ContractMethod { get; internal set; }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        [JsonProperty("Args")]
        public ArgData[] Args { get; internal set; }
    }
    #endregion

    #region BuildSignTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildSignTx(SignTxOptions)"/> method.
    /// </summary>
    public class SignTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public SignTxJsonResult TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for signer transaciton.
    /// </summary>
    public class SignTxJsonResult : GeneralTxJson
    {

    }
    #endregion

    #region BuildOfferCancelTx
    /// <summary>
    /// Represents the response of <see cref="Remote.BuildOfferCancelTx(OfferCancelTxOptions)"/> method.
    /// </summary>
    public class OfferCancelTxResponse : GeneralTxResponse
    {
        /// <summary>
        /// Gets the json data for this transaction.
        /// </summary>
        [JsonProperty("tx_json")]
        public OfferCancelTxJson TxJson { get; internal set; }
    }

    /// <summary>
    /// Represents the json data for offer cancel transaction.
    /// </summary>
    public class OfferCancelTxJson : GeneralTxJson
    {
        /// <summary>
        /// Gets the sequence of the offer.
        /// </summary>
        [JsonProperty("OfferSequence")]
        public int OfferSequence { get; internal set; }
    }

    #endregion

    #region RequestPathFind
    /// <summary>
    /// Represents the response of <see cref="Remote.RequestPathFind(PathFindOptions)"/> method.
    /// </summary>
    public class PathFindResponse
    {
        /// <summary>
        /// Gets the wallet address.
        /// </summary>
        [JsonProperty("source_account")]
        public string Source { get; internal set; }

        /// <summary>
        /// Gets the address of counter party.
        /// </summary>
        [JsonProperty("destination_account")]
        public string Destination { get; internal set; }

        /// <summary>
        /// Gets the amount of payment.
        /// </summary>
        [JsonProperty("destination_amount")]
        public Amount Amount { get; internal set; }

        [JsonProperty("alternatives")]
        internal PathFindAlternative[] RawAlternatives { get; set; }

        /// <summary>
        /// Gets the list of alternatives.
        /// </summary>
        [JsonIgnore]
        public PathFind[] Alternatives { get; internal set; }
    }

    /// <summary>
    /// Represents a path find for payment.
    /// </summary>
    public class PathFind
    {
        /// <summary>
        /// Gets the key of the path find.
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// Gets the amount of choice for this path find.
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
    /// <summary>
    /// Represents the response for the <see cref="Remote.Transactions"/> event.
    /// </summary>
    public class TransactionResponse
    {
        /// <summary>
        /// Gets the result of the transaction.
        /// </summary>
        /// <remarks>
        /// "tesSUCCESS" means transaction is applied successfully.
        /// </remarks>
        [JsonProperty("engine_result")]
        public string EngineResult { get; internal set; }

        /// <summary>
        /// Gets the result code of the submit.
        /// </summary>
        [JsonProperty("engine_result_code")]
        public int EngineResultCode { get; internal set; }

        /// <summary>
        /// Gets the result message of the submit.
        /// </summary>
        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; internal set; }

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
        /// Gets the status of the transaction.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; internal set; }

        /// <summary>
        /// Gets the type of transaciton.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; internal set; }

        /// <summary>
        /// Gets a boolean value indicating whether the transaction is validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool Validated { get; internal set; }

        /// <summary>
        /// Gets the transaction data.
        /// </summary>
        [JsonProperty("transaction")]
        public Tx Transaction { get; internal set; }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }

        /// <summary>
        /// Gets the parsed result of the transaction.
        /// </summary>
        [JsonIgnore]
        public TxResult TxResult { get; internal set; }
    }
    #endregion
}
