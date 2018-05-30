using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class Serializer
    {
        private const int _requiredMeta = 0;
        private const int _optionalMeta = 1;
        private const int _defaultMeta = 2;

        private static readonly List<List<object>> _transactionTypeBaseMeta = new List<List<object>>
        {
            new List<object>{ "TransactionType", _requiredMeta },
            new List<object>{ "Flags", _optionalMeta },
            new List<object>{ "SourceTag", _optionalMeta },
            new List<object>{ "LastLedgerSequence", _optionalMeta },
            new List<object>{ "Account", _requiredMeta },
            new List<object>{ "Sequence", _optionalMeta },
            new List<object>{ "Fee", _requiredMeta },
            new List<object>{ "OperationLimit", _optionalMeta },
            new List<object>{ "SigningPubKey", _optionalMeta },
            new List<object>{ "TxnSignature", _optionalMeta },
        };

        private static readonly IDictionary<TransactionType, List<List<object>>> _transactionTypeMetas = new Dictionary<TransactionType, List<List<object>>>
        {
            { TransactionType.AccountSet,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "EmailHash", _optionalMeta },
                    new List<object>{ "WalletLocator", _optionalMeta },
                    new List<object>{ "WalletSize", _optionalMeta },
                    new List<object>{ "MessageKey", _optionalMeta },
                    new List<object>{ "Domain", _optionalMeta },
                    new List<object>{ "TransferRate", _optionalMeta },
                }
            },
            { TransactionType.TrustSet,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "LimitAmount", _optionalMeta },
                    new List<object>{ "QualityIn", _optionalMeta },
                    new List<object>{ "QualityOut", _optionalMeta },
                }
            },
            {TransactionType.RelationSet,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{"Target", _requiredMeta },
                    new List<object>{"RelationType", _requiredMeta },
                    new List<object>{"LimitAmount", _optionalMeta }
                }
            },
            {TransactionType.RelationDel,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{"Target", _requiredMeta },
                    new List<object>{"RelationType", _requiredMeta },
                    new List<object>{"LimitAmount", _optionalMeta }
                }
            },
            { TransactionType.OfferCreate,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "TakerPays", _requiredMeta },
                    new List<object>{ "TakerGets", _requiredMeta },
                    new List<object>{ "Expiration", _optionalMeta },
                }
            },
            { TransactionType.OfferCancel,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "OfferSequence", _requiredMeta },
                }
            },
            { TransactionType.SetRegularKey,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "RegularKey", _requiredMeta },
                }
            },
            { TransactionType.Payment,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Destination", _requiredMeta },
                    new List<object>{ "Amount", _requiredMeta },
                    new List<object>{ "SendMax", _optionalMeta },
                    new List<object>{ "Paths", _defaultMeta },
                    new List<object>{ "InvoiceID", _optionalMeta },
                    new List<object>{ "DestinationTag", _optionalMeta },
                }
            },
            { TransactionType.ConfigContract,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Method", _requiredMeta },
                    new List<object>{ "Amount", _optionalMeta },
                    new List<object>{ "Payload", _optionalMeta },
                    new List<object>{ "Args", _optionalMeta },
                    new List<object>{ "Destination", _optionalMeta },
                    new List<object>{ "ContractMethod", _optionalMeta },
                    new List<object>{ "Contracttype", _optionalMeta },
                }
            },
            { TransactionType.Contract,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Expiration", _requiredMeta },
                    new List<object>{ "BondAmount", _requiredMeta },
                    new List<object>{ "StampEscrow", _requiredMeta },
                    new List<object>{ "JingtumEscrow", _requiredMeta },
                    new List<object>{ "CreateCode", _optionalMeta },
                    new List<object>{ "FundCode", _optionalMeta },
                    new List<object>{ "RemoveCode", _optionalMeta },
                    new List<object>{ "ExpireCode", _optionalMeta },
                }
            },
            { TransactionType.RemoveContract,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Target", _requiredMeta },
                }
            },
            { TransactionType.EnableFeature,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Feature", _requiredMeta },
                }
            },
            { TransactionType.SetFee,
                new List<List<object>>(_transactionTypeBaseMeta)
                {
                    new List<object>{ "Features", _requiredMeta },
                    new List<object>{ "BaseFee", _requiredMeta },
                    new List<object>{ "ReferenceFeeUnits", _requiredMeta },
                    new List<object>{ "ReserveBase", _requiredMeta },
                    new List<object>{ "ReserveIncrement", _requiredMeta },
                }
            }
        };

        private static readonly List<List<object>> _ledgerEntryTypeBaseMeta = new List<List<object>>
        {
            new List<object>{ "LedgerIndex", _optionalMeta },
            new List<object>{ "LedgerEntryType", _requiredMeta },
            new List<object>{ "Flags", _requiredMeta },
        };

        private static readonly IDictionary<LedgerEntryType, List<List<object>>> _ledgerEntryTypeMetas = new Dictionary<LedgerEntryType, List<List<object>>>
        {
            {
                LedgerEntryType.AccountRoot,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "Sequence", _requiredMeta },
                    new List<object>{ "PreviousTxnLgrSeq", _requiredMeta },
                    new List<object>{ "TransferRate", _optionalMeta },
                    new List<object>{ "OwnerCount", _requiredMeta },
                    new List<object>{ "EmailHash", _optionalMeta },
                    new List<object>{ "PreviousTxnID", _requiredMeta },
                    new List<object>{ "AccountTxnID", _optionalMeta },
                    new List<object>{ "WalletLocator", _optionalMeta },
                    new List<object>{ "Balance", _requiredMeta },
                    new List<object>{ "MessageKey", _optionalMeta },
                    new List<object>{ "Domain", _optionalMeta },
                    new List<object>{ "Account", _requiredMeta },
                    new List<object>{ "RegularKey", _optionalMeta },
                }
            },
            {
                LedgerEntryType.Contract,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "PreviousTxnLgrSeq", _requiredMeta },
                    new List<object>{ "Expiration", _requiredMeta },
                    new List<object>{ "BondAmount", _requiredMeta },
                    new List<object>{ "PreviousTxnID", _requiredMeta },
                    new List<object>{ "Balance", _requiredMeta },
                    new List<object>{ "FundCode", _optionalMeta },
                    new List<object>{ "RemoveCode", _optionalMeta },
                    new List<object>{ "ExpireCode", _optionalMeta },
                    new List<object>{ "CreateCode", _optionalMeta },
                    new List<object>{ "Account", _requiredMeta },
                    new List<object>{ "Owner", _requiredMeta },
                    new List<object>{ "Issuer", _requiredMeta },
                }
            },
            {
                LedgerEntryType.DirectoryNode,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "IndexNext", _optionalMeta },
                    new List<object>{ "IndexPrevious", _optionalMeta },
                    new List<object>{ "ExchangeRate", _optionalMeta },
                    new List<object>{ "RootIndex", _requiredMeta },
                    new List<object>{ "Owner", _optionalMeta },
                    new List<object>{ "TakerPaysCurrency", _optionalMeta },
                    new List<object>{ "TakerPaysIssuer", _optionalMeta },
                    new List<object>{ "TakerGetsCurrency", _optionalMeta },
                    new List<object>{ "TakerGetsIssuer", _optionalMeta },
                    new List<object>{ "Indexes", _requiredMeta },
                }
            },
            {
                LedgerEntryType.EnabledFeatures,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "Features", _requiredMeta },
                }
            },
            {
                LedgerEntryType.FeeSettings,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "ReferenceFeeUnits", _requiredMeta },
                    new List<object>{ "ReserveBase", _requiredMeta },
                    new List<object>{ "ReserveIncrement", _requiredMeta },
                    new List<object>{ "BaseFee", _requiredMeta },
                    new List<object>{ "LedgerIndex", _optionalMeta },
                }
            },
            {
                LedgerEntryType.GeneratorMap,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "Generator", _requiredMeta },
                }
            },
            {
                LedgerEntryType.LedgerHashes,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "LedgerEntryType", _requiredMeta },
                    new List<object>{ "Flags", _requiredMeta },
                    new List<object>{ "FirstLedgerSequence", _optionalMeta },
                    new List<object>{ "LastLedgerSequence", _optionalMeta },
                    new List<object>{ "LedgerIndex", _optionalMeta },
                    new List<object>{ "Hashes", _requiredMeta },
                }
            },
            {
                LedgerEntryType.Nickname,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "LedgerEntryType", _requiredMeta },
                    new List<object>{ "Flags", _requiredMeta },
                    new List<object>{ "LedgerIndex", _optionalMeta },
                    new List<object>{ "MinimumOffer", _optionalMeta },
                    new List<object>{ "Account", _requiredMeta },
                }
            },
            {
                LedgerEntryType.Offer,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "LedgerEntryType", _requiredMeta },
                    new List<object>{ "Flags", _requiredMeta },
                    new List<object>{ "Sequence", _requiredMeta },
                    new List<object>{ "PreviousTxnLgrSeq", _requiredMeta },
                    new List<object>{ "Expiration", _optionalMeta },
                    new List<object>{ "BookNode", _requiredMeta },
                    new List<object>{ "OwnerNode", _requiredMeta },
                    new List<object>{ "PreviousTxnID", _requiredMeta },
                    new List<object>{ "LedgerIndex", _optionalMeta },
                    new List<object>{ "BookDirectory", _requiredMeta },
                    new List<object>{ "TakerPays", _requiredMeta },
                    new List<object>{ "TakerGets", _requiredMeta },
                    new List<object>{ "Account", _requiredMeta },
                }
            },
            {
                LedgerEntryType.SkywellState,
                new List<List<object>>(_ledgerEntryTypeBaseMeta)
                {
                    new List<object>{ "LedgerEntryType", _requiredMeta },
                    new List<object>{ "Flags", _requiredMeta },
                    new List<object>{ "PreviousTxnLgrSeq", _requiredMeta },
                    new List<object>{ "HighQualityIn", _optionalMeta },
                    new List<object>{ "HighQualityOut", _optionalMeta },
                    new List<object>{ "LowQualityIn", _optionalMeta },
                    new List<object>{ "LowQualityOut", _optionalMeta },
                    new List<object>{ "LowNode", _optionalMeta },
                    new List<object>{ "HighNode", _optionalMeta },
                    new List<object>{ "PreviousTxnID", _requiredMeta },
                    new List<object>{ "LedgerIndex", _optionalMeta },
                    new List<object>{ "Balance", _requiredMeta },
                    new List<object>{ "LowLimit", _requiredMeta },
                    new List<object>{ "HighLimit", _requiredMeta },
                }
            },
        };

        private static readonly List<List<object>> _metaData = new List<List<object>>
        {
            new List<object>{ "TransactionIndex", _requiredMeta },
            new List<object>{ "TransactionResult", _requiredMeta },
            new List<object>{ "AffectedNodes", _requiredMeta },
        };

        private IList<byte> _buffer = new List<byte>();
        public byte[] Buffer => _buffer.ToArray();

        internal Serializer()
        {

        }

        public static Serializer Create(TxData obj)
        {
            obj = (TxData)obj.Clone();
            if (obj.TransactionType is int || obj.TransactionType is TransactionType)
            {
                var transactionType = (TransactionType)obj.TransactionType;
                if (!Enum.IsDefined(typeof(TransactionType), transactionType))
                {
                    throw new InvalidOperationException("Invalid transaction type!");
                }

                obj.TransactionType = transactionType.ToString();
            }

            List<List<object>> typedef = null;
            if (obj.TransactionType is string transactionTypeStr)
            {
                TransactionType transactionType;
                if (!Enum.TryParse(transactionTypeStr, out transactionType))
                {
                    throw new InvalidOperationException("Invalid transaction type!");
                }

                obj.TransactionType = (int)transactionType;
                typedef = _transactionTypeMetas[transactionType];
            }
            else
            {
                throw new InvalidOperationException("Object to be serialized must contain either TransactionType, LedgerEntryType or AffectedNodes.");
            }

            var so = new Serializer();
            so.Serialize(typedef, obj);
            return so;
        }

        private void Serialize(List<List<object>> typedef, TxData obj)
        {
            SerializedTypeHelper.STObject.Serialize(this, obj, true);
        }

        public void Append(byte[] v)
        {
            foreach(var b in v)
            {
                _buffer.Add(b);
            }
        }

        public string ToHex()
        {
            return Utils.BytesToHex(Buffer);
        }

        public byte[] Hash(UInt32 prefix)
        {
            var so = new Serializer();
            SerializedTypeHelper.STInt32.Serialize(so, prefix);

            var bytes = so.Buffer.Concat(this.Buffer).ToArray();
            var sha512 = new Core.Sha512();
            sha512.Add(bytes);
            return sha512.Finish256();
        }
    }
}
