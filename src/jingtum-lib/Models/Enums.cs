using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Indicates the ledger state.
    /// </summary>
    public enum LedgerState
    {
        /// <summary>
        /// The current ledger.
        /// </summary>
        Current,
        /// <summary>
        /// The closed ledger.
        /// </summary>
        Closed,
        /// <summary>
        /// The validated ledger.
        /// </summary>
        Validated
    }

    /// <summary>
    /// Indicates the type of relation.
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// The turst relation.
        /// </summary>
        Trust = 0,
        /// <summary>
        /// The authorize relation.
        /// </summary>
        Authorize = 1,
        /// <summary>
        /// The freeze relation.
        /// </summary>
        Freeze = 2,
        /// <summary>
        /// The unfreeze relation.
        /// </summary>
        Unfreeze = 3,
    }

    /// <summary>
    /// Indicates the type of account attribute.
    /// </summary>
    public enum AccountSetType
    {
        /// <summary>
        /// Used to set normal account info.
        /// </summary>
        Property,
        /// <summary>
        /// Used to set delegate account for this account.
        /// </summary>
        Delegate,
        /// <summary>
        /// Used to set signer for this account.
        /// </summary>
        Signer
    }

    /// <summary>
    /// Indicates the type of offer.
    /// </summary>
    public enum OfferType
    {
        /// <summary>
        /// Buy offer.
        /// </summary>
        Buy,
        /// <summary>
        /// Sell offer.
        /// </summary>
        Sell
    }

    /// <summary>
    /// Indicates the flags for acount set transaction.
    /// </summary>
    public enum SetClearFlags : UInt32
    {
        /// <summary>
        /// Require a destination tag for payments.
        /// </summary>
        RequireDest = 1,
        /// <summary>
        /// Require an authorization to hold IOUs.
        /// </summary>
        RequireAuth = 2,
        /// <summary>
        /// Disallow sending SWT.
        /// </summary>
        DisallowSWT = 3,
        /// <summary>
        /// Force regular key.
        /// </summary>
        DisableMaster = 4,
        /// <summary>
        /// todo:
        /// </summary>
        NoFreeze = 6,
        /// <summary>
        /// todo:
        /// </summary>
        GlobalFreeze = 7
    }

    /// <summary>
    /// Indicates the flags for any transaction type.
    /// </summary>
    [Flags]
    public enum UniversalFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        FullyCanonicalSig = 0x80000000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.AccountSet"/>.
    /// </summary>
    [Flags]
    public enum AccountSetFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        RequireDestTag = 0x00010000,
        /// <summary>
        /// todo:
        /// </summary>
        OptionalDestTag = 0x00020000,
        /// <summary>
        /// todo:
        /// </summary>
        RequireAuth = 0x00040000,
        /// <summary>
        /// todo:
        /// </summary>
        OptionalAuth = 0x00080000,
        /// <summary>
        /// todo:
        /// </summary>
        DisallowSWT = 0x00100000,
        /// <summary>
        /// todo:
        /// </summary>
        AllowSWT = 0x00200000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.TrustSet"/>.
    /// </summary>
    [Flags]
    public enum TrustSetFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        SetAuth = 0x00010000,
        /// <summary>
        /// todo:
        /// </summary>
        NoSkywell = 0x00020000,
        /// <summary>
        /// todo:
        /// </summary>
        SetNoSkywell = 0x00020000,
        /// <summary>
        /// todo:
        /// </summary>
        ClearNoSkywell = 0x00040000,
        /// <summary>
        /// todo:
        /// </summary>
        SetFreeze = 0x00100000,
        /// <summary>
        /// todo:
        /// </summary>
        ClearFreeze = 0x00200000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.OfferCreate"/>.
    /// </summary>
    [Flags]
    public enum OfferCreateFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        Passive = 0x00010000,
        /// <summary>
        /// todo:
        /// </summary>
        ImmediateOrCancel = 0x00020000,
        /// <summary>
        /// todo:
        /// </summary>
        FillOrKill = 0x00040000,
        /// <summary>
        /// todo:
        /// </summary>
        Sell = 0x00080000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.Payment"/>.
    /// </summary>
    [Flags]
    public enum PaymentFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        NoSkywellDirect = 0x00010000,
        /// <summary>
        /// todo:
        /// </summary>
        PartialPayment = 0x00020000,
        /// <summary>
        /// todo:
        /// </summary>
        LimitQuality = 0x00040000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.RelationSet"/>.
    /// </summary>
    [Flags]
    public enum RelationSetFlags : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        Authorize = 0x00000001,
        /// <summary>
        /// todo:
        /// </summary>
        Freeze = 0x00000011
    }

    /// <summary>
    /// Indicates the transaction type.
    /// </summary>
    public enum TransactionType : UInt32
    {
        /// <summary>
        /// For <see cref="Remote.BuildPaymentTx(PaymentTxOptions)"/> transaction.
        /// </summary>
        Payment = 0,
        /// <summary>
        /// For <see cref="Remote.BuildAccountSetTx(AccountSetTxOptions)"/> transaction.
        /// </summary>
        AccountSet = 3,
        /// <summary>
        /// For <see cref="Remote.BuildAccountSetTx(AccountSetTxOptions)"/> transaction with <see cref="AccountSetType.Delegate"/> attruibute.
        /// </summary>
        SetRegularKey = 5,
        /// <summary>
        /// For <see cref="Remote.BuildOfferCreateTx(OfferCreateTxOptions)"/> transaciton.
        /// </summary>
        OfferCreate = 7,
        /// <summary>
        /// For <see cref="Remote.BuildOfferCancelTx(OfferCancelTxOptions)"/> transaction.
        /// </summary>
        OfferCancel = 8,
        /// <summary>
        /// todo:
        /// </summary>
        Contract = 9,
        /// <summary>
        /// todo:
        /// </summary>
        RemoveContract = 10,
        /// <summary>
        /// For <see cref="Remote.BuildRelationTx(RelationTxOptions)"/> transaction.
        /// </summary>
        TrustSet = 20,
        /// <summary>
        /// For <see cref="Remote.BuildRelationTx(RelationTxOptions)"/> transaction.
        /// </summary>
        RelationSet = 21,
        /// <summary>
        /// For <see cref="Remote.BuildRelationTx(RelationTxOptions)"/> transaction.
        /// </summary>
        RelationDel = 22,
        /// <summary>
        /// For <see cref="Remote.DeployContractTx(DeployContractTxOptions)"/> transaction and <see cref="Remote.CallContractTx(CallContractTxOptions)"/> transaction.
        /// </summary>
        ConfigContract = 30,
        /// <summary>
        /// For <see cref="Remote.BuildSignTx(SignTxOptions)"/> transaction.
        /// </summary>
        EnableFeature = 100,

        /// <summary>
        /// todo:
        /// </summary>
        SetFee = 101,

        // following transaction type value is unknown
        /// <summary>
        /// todo:
        /// </summary>
        Signer,
        /// <summary>
        /// todo:
        /// </summary>
        SignSet,
        /// <summary>
        /// todo:
        /// </summary>
        Operation,
    }

    /// <summary>
    /// todo:
    /// </summary>
    public enum LedgerEntryType : UInt32
    {
        /// <summary>
        /// todo:
        /// </summary>
        Unknown,

        /// <summary>
        /// todo:
        /// </summary>
        State,
        /// <summary>
        /// todo:
        /// </summary>
        ManageIssuer,

        /// <summary>
        /// todo:
        /// </summary>
        AccountRoot = 97,
        /// <summary>
        /// todo:
        /// </summary>
        Contract = 99,
        /// <summary>
        /// todo:
        /// </summary>
        DirectoryNode = 100,
        /// <summary>
        /// todo:
        /// </summary>
        EnabledFeatures = 102,
        /// <summary>
        /// todo:
        /// </summary>
        GeneratorMap = 103,
        /// <summary>
        /// todo:
        /// </summary>
        LedgerHashes = 104,
        /// <summary>
        /// todo:
        /// </summary>
        Nickname = 110,
        /// <summary>
        /// todo:
        /// </summary>
        Offer = 111,
        /// <summary>
        /// todo:
        /// </summary>
        SkywellState = 114 ,
        /// <summary>
        /// todo:
        /// </summary>
        FeeSettings = 115,
    };

    /// <summary>
    /// Indicates the the connection state.
    /// </summary>
    internal enum ServerState
    {
        Offline,
        Online
    }

    /// <summary>
    /// Indicates the the online state of the server.
    /// </summary>
    internal enum OnlineState
    {
        Syncing,
        Tracking,
        Proposing,
        Validating,
        Full,
        Connected
    }

    internal enum LedgerAccountRootFlags : UInt32
    {
        PasswordSpent = 0x00010000, // True, if password set fee is spent.
        RequireDestTag = 0x00020000, // True, to require a DestinationTag for payments.
        RequireAuth = 0x00040000, // True, to require a authorization to hold IOUs.
        DisallowSWT = 0x00080000, // True, to disallow sending SWT.
        DisableMaster = 0x00100000  // True, force regular key.
    }

    internal enum LedgerOfferFlags : UInt32
    {
        Passive = 0x00010000,
        Sell = 0x00020000  // True, offer was placed as a sell.
    }

    internal enum LedgerStateFlags : UInt32
    {
        LowReserve = 0x00010000, // True, if entry counts toward reserve.
        HighReserve = 0x00020000,
        LowAuth = 0x00040000,
        HighAuth = 0x00080000,
        LowNoSkywell = 0x00100000,
        HighNoSkywell = 0x00200000
    }
}
