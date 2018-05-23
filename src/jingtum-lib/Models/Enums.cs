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
        /// Used to set signers for this account.
        /// </summary>
        Signer
    }

    /// <summary>
    /// Indicates the type of offer.
    /// </summary>
    public enum OfferType
    {
        /// <summary>
        /// Sell order.
        /// </summary>
        Sell,
        /// <summary>
        /// Buy order.
        /// </summary>
        Buy
    }

    /// <summary>
    /// Indicates the flags for acount set transaction.
    /// </summary>
    public enum SetClearFlags
    {
        RequireDest = 1,
        RequireAuth = 2,
        DisallowSWT = 3,
        DisableMaster = 4,
        NoFreeze = 6,
        GlobalFreeze = 7
    }

    /// <summary>
    /// Indicates the flags for any transaction type.
    /// </summary>
    [Flags]
    public enum UniversalFlags : long
    {
        FullyCanonicalSig = 0x80000000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.AccountSet"/>.
    /// </summary>
    [Flags]
    public enum AccountSetFlags : long
    {
        RequireDestTag = 0x00010000,
        OptionalDestTag = 0x00020000,
        RequireAuth = 0x00040000,
        OptionalAuth = 0x00080000,
        DisallowSWT = 0x00100000,
        AllowSWT = 0x00200000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.TrustSet"/>.
    /// </summary>
    [Flags]
    public enum TrustSetFlags : long
    {
        SetAuth = 0x00010000,
        NoSkywell = 0x00020000,
        SetNoSkywell = 0x00020000,
        ClearNoSkywell = 0x00040000,
        SetFreeze = 0x00100000,
        ClearFreeze = 0x00200000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.OfferCreate"/>.
    /// </summary>
    [Flags]
    public enum OfferCreateFlags : long
    {
        Passive = 0x00010000,
        ImmediateOrCancel = 0x00020000,
        FillOrKill = 0x00040000,
        Sell = 0x00080000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.Payment"/>.
    /// </summary>
    [Flags]
    public enum PaymentFlags : long
    {
        NoSkywellDirect = 0x00010000,
        PartialPayment = 0x00020000,
        LimitQuality = 0x00040000
    }

    /// <summary>
    /// Indicates the flags for <see cref="TransactionType.RelationSet"/>.
    /// </summary>
    [Flags]
    public enum RelationSetFlags : long
    {
        Authorize = 0x00000001,
        Freeze = 0x00000011
    }

    /// <summary>
    /// Indicates the transaction type.
    /// </summary>
    public enum TransactionType
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
        Contract = 9,
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
        /// For <see cref="Remote.BuildDelegateKeySet(AccountSetTxOptions, Transaction)"/> transaction and <see cref="Remote.CallContractTx(CallContractTxOptions)"/> transaction.
        /// </summary>
        ConfigContract = 30,
        /// <summary>
        /// For <see cref="Remote.BuildSignTx(SignTxOptions)"/> transaction.
        /// </summary>
        EnableFeature = 100,
        SetFee = 101,

        // following transaction type value is unknown
        Signer,
        SignSet,
        Operation,
    }

    internal enum LedgerEntryType
    {
        AccountRoot = 97,
        Contract = 99,
        DirectoryNode = 100,
        EnabledFeatures = 102,
        GeneratorMap = 103,
        LedgerHashes = 104,
        Nickname = 110,
        Offer = 111,
        SkywellState = 114 ,
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

    internal enum LedgerAccountRootFlags : long
    {
        PasswordSpent = 0x00010000, // True, if password set fee is spent.
        RequireDestTag = 0x00020000, // True, to require a DestinationTag for payments.
        RequireAuth = 0x00040000, // True, to require a authorization to hold IOUs.
        DisallowSWT = 0x00080000, // True, to disallow sending SWT.
        DisableMaster = 0x00100000  // True, force regular key.
    }

    internal enum LedgerOfferFlags : long
    {
        Passive = 0x00010000,
        Sell = 0x00020000  // True, offer was placed as a sell.
    }

    internal enum LedgerStateFlags : long
    {
        LowReserve = 0x00010000, // True, if entry counts toward reserve.
        HighReserve = 0x00020000,
        LowAuth = 0x00040000,
        HighAuth = 0x00080000,
        LowNoSkywell = 0x00100000,
        HighNoSkywell = 0x00200000
    }
}
