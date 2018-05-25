using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal static class ResponseParser
    {
        internal class FieldsSet
        {
            private Fields[] _fieldsArray;

            public FieldsSet(params Fields[] fieldsArray)
            {
                _fieldsArray = fieldsArray;
            }

            public Amount TakerGets
            {
                get
                {
                    Amount amount;
                    TryGetProperty("TakerGets", out amount);
                    return amount;
                }
            }

            public Amount TakerPays
            {
                get
                {
                    Amount amount;
                    TryGetProperty("TakerPays", out amount);
                    return amount;
                }
            }

            public string Account
            {
                get
                {
                    string account;
                    TryGetProperty("Account", out account);
                    return account;
                }
            }

            public Amount HighLimit
            {
                get
                {
                    Amount amount;
                    TryGetProperty("HighLimit", out amount);
                    return amount;
                }
            }

            public Amount LowLimit
            {
                get
                {
                    Amount amount;
                    TryGetProperty("LowLimit", out amount);
                    return amount;
                }
            }

            public long Flags
            {
                get
                {
                    long? flags;
                    TryGetProperty("Flags", out flags);
                    return flags == null ? 0 : flags.Value;
                }
            }

            public int Sequence
            {
                get
                {
                    int? sequence;
                    TryGetProperty("Sequence", out sequence);
                    return sequence == null ? 0 : sequence.Value;
                }
            }

            public string PreviousTxnID
            {
                get
                {
                    string hash;
                    TryGetProperty("PreviousTxnID", out hash);
                    return hash;
                }
            }

            public string RegularKey
            {
                get
                {
                    string key;
                    TryGetProperty("RegularKey", out key);
                    return key;
                }
            }

            public bool TryGetProperty<T>(string name, out T value)
            {
                var property = typeof(Fields).GetProperty(name);
                if (property != null)
                {
                    foreach (var fields in _fieldsArray)
                    {
                        if (fields == null) continue;


                        var propertyValue = property.GetValue(fields);
                        if (propertyValue != null)
                        {
                            try
                            {
                                value = (T)propertyValue;
                                return true;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }

                value = default(T);
                return false;
            }
        }

        public static string[] AffectBooks(Tx tx, Meta meta)
        {
            if (tx == null || meta == null) return null;

            var isSell = ((long)LedgerOfferFlags.Sell & tx.Flags) != 0;
            var books = new List<string>();
            foreach(var an in meta.AffectedNodes)
            {
                var node = an.CreatedNode ?? an.ModifiedNode ?? an.DeletedNode;
                if (node == null || node.LedgerEntryType != "Offer") continue;

                var fielsSet = new FieldsSet(node.FinalFields, node.NewFields, node.PreviousFields);
                var gets = fielsSet.TakerGets;
                var pays = fielsSet.TakerPays;
                var getsKey = GetAmountString(gets);
                var paysKey = GetAmountString(pays);
                var key = isSell ? GetAmoutPairKey(pays, gets) : GetAmoutPairKey(gets, pays);
                books.Add(key);
            }
            return books.ToArray();
        }

        public static string GetAmountString(Amount amount)
        {
            if (amount.Currency == Config.Currency) return Config.Currency;
            return string.Format("{0}:{1}", amount.Currency ?? "", amount.Issuer ?? "");
        }

        public static string GetAmoutPairKey(Amount gets, Amount pays)
        {
            return string.Format("{0}/{1}", GetAmountString(gets), GetAmountString(pays));
        }

        public static string[] AffectedAccounts(Tx tx, Meta meta)
        {
            var accounts = new List<string>();
            accounts.Add(tx.Account);

            if (tx.Destination != null)
            {
                accounts.Add(tx.Destination);
            }

            if (tx.LimitAmount != null)
            {
                accounts.Add(tx.LimitAmount.Issuer);
            }

            if(meta!=null && meta.TransactionResult== "tesSUCCESS")
            {
                foreach(var an in meta.AffectedNodes)
                {
                    var node = an.CreatedNode ?? an.DeletedNode ?? an.ModifiedNode;
                    if (node == null) continue;

                    var fieldsSet = new FieldsSet(node.FinalFields, node.NewFields, node.PreviousFields);

                    switch (node.LedgerEntryType)
                    {
                        case "AccountRoot":
                        case "Offer":
                            var account = fieldsSet.Account;
                            if (account != null)
                            {
                                accounts.Add(account);
                            }

                            break;
                        case "SkywellState":
                            var highLimit = fieldsSet.HighLimit;
                            if (highLimit != null && highLimit.Issuer != null)
                            {
                                accounts.Add(highLimit.Issuer);
                            }

                            var lowLimit = fieldsSet.LowLimit;
                            if (lowLimit != null && lowLimit.Issuer != null)
                            {
                                accounts.Add(lowLimit.Issuer);
                            }

                            break;
                    }
                }
            }

            return accounts.ToArray();
        }

        public static TxResult ProcessTx(Tx tx, Meta meta, string account)
        {
            var result = CreateTxResult(tx, account);
            result.Date = (tx.Date ?? tx.Timestamp).Value;
            result.Hash = tx.Hash;
            var fee = 10000;
            int.TryParse(tx.Fee, out fee);
            result.Fee = (fee / 1000000d).ToString("0.######");
            result.Result = meta != null ? meta.TransactionResult : "failed";
            result.Memos = ParseMemos(tx);

            if (meta != null && meta.TransactionResult == "tesSUCCESS")
            {
                result.Effects = ParseEffects(tx, meta.AffectedNodes, account, result);
            }

            return result;
        }

        private static NodeEffect[] ParseEffects(Tx tx, AffectedNode[] affectedNodes, string account, TxResult result)
        {
            if (affectedNodes == null || affectedNodes.Length == 0) return null;

            var effects = new List<NodeEffect>();
            foreach (var an in affectedNodes)
            {
                var diffType = an.DiffType;
                var node = an.CreatedNode ?? an.DeletedNode ?? an.ModifiedNode;
                var fieldsSet = new FieldsSet(node.FinalFields, node.PreviousFields, node.NewFields);
                NodeEffect nodeEffect = null;

                //TODO now only get offer related effects, need to process other entry type

                if (node.LedgerEntryType == "Offer")
                {
                    // for new and cancelled offers
                    var sell = fieldsSet.Flags == (long)LedgerOfferFlags.Sell;
                    OfferEffect offerEffect = null;

                    // current account offer
                    if (fieldsSet.Account == account)
                    {
                        // 1. offer_partially_funded
                        var seq = fieldsSet.Sequence;
                        if (diffType == DiffType.ModifiedNode || (diffType == DiffType.DeletedNode && node.PreviousFields != null && node.PreviousFields.TakerGets != null && !IsAmountZero(node.FinalFields.TakerGets)))
                        {
                            var partiallyFoundedEffect = new OfferPartiallyFundedEffect();
                            offerEffect = partiallyFoundedEffect;
                            partiallyFoundedEffect.Seq = seq;

                            partiallyFoundedEffect.CounterParty = new CounterParty { Account = tx.Account, Seq = tx.Sequence, Hash = tx.Hash };
                            if (diffType == DiffType.DeletedNode)
                            {
                                partiallyFoundedEffect.Cancelled = true;
                            }
                            else
                            {
                                // TODO no need partially funded must remains offers
                                partiallyFoundedEffect.Remaining = !IsAmountZero(fieldsSet.TakerGets);
                            }

                            var gets = fieldsSet.TakerGets;
                            var pays = fieldsSet.TakerPays;
                            partiallyFoundedEffect.Gets = gets;
                            partiallyFoundedEffect.Pays = pays;
                            partiallyFoundedEffect.Got = AmountSubtract(node.PreviousFields.TakerPays, pays);
                            partiallyFoundedEffect.Paid = AmountSubtract(node.PreviousFields.TakerGets, gets);
                            partiallyFoundedEffect.Type = sell ? OfferEffectType.Sold : OfferEffectType.Bought;
                        }
                        else
                        {
                            // offer_funded, offer_created or offer_cancelled offer effect
                            var effect = diffType == DiffType.CreatedNode ? EffectType.OfferCreated : (node.PreviousFields != null && node.PreviousFields.TakerPays != null ? EffectType.OfferFunded : EffectType.OfferCancelled);
                            switch (effect)
                            {
                                // 2. offer_funded
                                case EffectType.OfferFunded:
                                    var fundedfEffect = new OfferFundedEffect();
                                    fundedfEffect.Seq = seq;
                                    offerEffect = fundedfEffect;

                                    fundedfEffect.CounterParty = new CounterParty { Account = tx.Account, Seq = tx.Sequence, Hash = tx.Hash };
                                    fundedfEffect.Got = AmountSubtract(node.PreviousFields.TakerPays, fieldsSet.TakerPays);
                                    fundedfEffect.Paid = AmountSubtract(node.PreviousFields.TakerGets, fieldsSet.TakerGets);
                                    fundedfEffect.Type = sell ? OfferEffectType.Sold : OfferEffectType.Bought;
                                    break;
                                // 3. offer_created
                                case EffectType.OfferCreated:
                                    var createdEffect = new OfferCreatedEffect();
                                    createdEffect.Seq = seq;
                                    offerEffect = createdEffect;

                                    createdEffect.Gets = fieldsSet.TakerGets;
                                    createdEffect.Pays = fieldsSet.TakerPays;
                                    createdEffect.Type = sell ? OfferEffectType.Sell : OfferEffectType.Buy;
                                    break;
                                // 4. offer_cancelled
                                case EffectType.OfferCancelled:
                                    var cancelledEffect = new OfferCancelledEffect();
                                    cancelledEffect.Seq = seq;
                                    offerEffect = cancelledEffect;

                                    cancelledEffect.Gets = fieldsSet.TakerGets;
                                    cancelledEffect.Pays = fieldsSet.TakerPays;
                                    cancelledEffect.Type = sell ? OfferEffectType.Sell : OfferEffectType.Buy;

                                    // collect data for cancel transaction type
                                    if (result.Type == TxResultType.OfferCancel)
                                    {
                                        var cancelResult = result as OfferCancelTxResult;
                                        cancelResult.Gets = cancelledEffect.Gets;
                                        cancelResult.Pays = cancelledEffect.Pays;
                                    }

                                    break;
                            }
                        }
                    }
                    // 5. offer_bought
                    else if (tx.Account == account && node.PreviousFields != null)
                    {
                        var boughtEffect = new OfferBoughtEffect();
                        offerEffect = boughtEffect;

                        boughtEffect.CounterParty = new CounterParty { Account = fieldsSet.Account, Seq = fieldsSet.Sequence, Hash = node.PreviousTxnID ?? fieldsSet.PreviousTxnID };
                        boughtEffect.Paid = AmountSubtract(node.PreviousFields.TakerPays, fieldsSet.TakerPays);
                        boughtEffect.Got = AmountSubtract(node.PreviousFields.TakerGets, fieldsSet.TakerGets);
                        boughtEffect.Type = sell ? OfferEffectType.Bought : OfferEffectType.Sold;
                    }

                    // add price
                    if (offerEffect != null && offerEffect.GotOrPays != null && offerEffect.PaidOrGets != null)
                    {
                        var created = offerEffect.Effect == EffectType.OfferCreated && offerEffect.Type == OfferEffectType.Buy;
                        var funded = offerEffect.Effect == EffectType.OfferFunded && offerEffect.Type == OfferEffectType.Bought;
                        var cancelled = offerEffect.Effect == EffectType.OfferCancelled && offerEffect.Type == OfferEffectType.Buy;
                        var bought = offerEffect.Effect == EffectType.OfferBought && offerEffect.Type == OfferEffectType.Bought;
                        var partially_funded = offerEffect.Effect == EffectType.OfferPartiallyFunded && offerEffect.Type == OfferEffectType.Bought;
                        var offerFunded = created || funded || cancelled || bought || partially_funded;
                        offerEffect.Price = GetPrice(offerEffect.GotOrPays, offerEffect.PaidOrGets, offerFunded);
                    }

                    nodeEffect = offerEffect;
                }
                else if (node.LedgerEntryType == "AccountRoot")
                {
                    if (result.Type == TxResultType.OfferEffect)
                    {
                        if (fieldsSet.RegularKey == account)
                        {
                            var regularKeyEffect = new SetRegularKeyEffect();
                            nodeEffect = regularKeyEffect;

                            regularKeyEffect.Type = "null";
                            regularKeyEffect.Account = fieldsSet.Account;
                            regularKeyEffect.RegularKey = account;
                        }
                    }
                }

                if (nodeEffect != null)
                {
                    effects.Add(nodeEffect);

                    if (diffType == DiffType.DeletedNode && nodeEffect.Effect != EffectType.OfferBought)
                    {
                        nodeEffect.Deleted = true;
                    }
                }
            }

            return effects.ToArray();
        }

        private static bool IsAmountZero(Amount amount)
        {
            if (amount == null) return false;

            var value = decimal.Parse(amount.Value);
            return value < new decimal(1e-12);
        }

        private static Amount AmountNegate(Amount amount)
        {
            if (amount == null) return null;

            var value = decimal.Parse(amount.Value);
            value = -value;
            return new Amount { Currency = amount.Currency, Issuer = amount.Issuer, Value = value.ToString() };
        }

        private static Amount AmountAdd(Amount amount1, Amount amount2)
        {
            if (amount1 == null) return amount2;
            if (amount2 == null) return amount1;

            var value1 = decimal.Parse(amount1.Value);
            var value2 = decimal.Parse(amount2.Value);
            var value = value1 + value2;

            return new Amount { Currency = amount1.Currency, Issuer = amount1.Issuer, Value = value.ToString() };
        }

        private static Amount AmountSubtract(Amount amount1, Amount amount2)
        {
            return AmountAdd(amount1, AmountNegate(amount2));
        }

        private static string AmountRatio(Amount amount1, Amount amount2)
        {
            var value1 = decimal.Parse(amount1.Value);
            var value2 = decimal.Parse(amount2.Value);
            var ratio = value1 / value2;
            return ratio.ToString();
        }

        public static string GetPrice(Amount pays, Amount gets, bool founded)
        {
            return founded ? AmountRatio(gets, pays) : AmountRatio(pays, gets);
        }

        private static string[] ParseMemos(Tx tx)
        {
            if (tx.Memos == null || tx.Memos.Length == 0) return null;

            return tx.Memos.Select(m => m.Memo == null ? null : m.Memo.MemoData).Where(s => !string.IsNullOrEmpty(s)).ToArray();
        }

        private static TxResult CreateTxResult(Tx tx, string account)
        {
            var txType = GetTxType(tx, account);
            switch (txType)
            {
                case TxResultType.Sent:
                    {
                        var result = new SentTxResult();
                        result.CounterParty = tx.Destination;
                        result.Amount = tx.Amount;
                        return result;
                    }
                case TxResultType.Received:
                    {
                        var result = new ReceivedTxResult();
                        result.CounterParty = tx.Account;
                        result.Amount = tx.Amount;
                        return result;
                    }
                case TxResultType.Trusted:
                    {
                        var result = new TrustedTxResult();
                        result.CounterParty = tx.Account;
                        result.Amount = ReserveAmount(tx.Amount, tx.Account);
                        return result;
                    }
                case TxResultType.Trusting:
                    {
                        var result = new TrustingTxResult();
                        result.CounterParty = tx.LimitAmount.Issuer;
                        result.Amount = tx.LimitAmount;
                        return result;
                    }
                case TxResultType.Convert:
                    {
                        var result = new ConvertTxResult();
                        result.Spent = tx.SendMax;
                        result.Amount = tx.Amount;
                        return result;
                    }

                case TxResultType.OfferNew:
                    {
                        var result = new OfferNewTxResult();
                        result.OfferType = tx.Flags == (long)OfferCreateFlags.Sell ? "sell" : "buy";
                        result.Gets = tx.TakerGets;
                        result.Pays = tx.TakerPays;
                        result.Seq = tx.Sequence;
                        return result;
                    }
                case TxResultType.OfferCancel:
                    {
                        var result = new OfferCancelTxResult();
                        result.OfferSeq = tx.OfferSequence;
                        return result;
                    }
                case TxResultType.RelationSet:
                    {
                        var result = new RelationSetTxResult();
                        result.CounterParty = tx.Target == account ? tx.Account : tx.Target;
                        result.RelationType = tx.RelationType == 3 ? RelationType.Freeze : RelationType.Authorize;
                        result.IsActive = tx.Target != account; //account === tx.Target ? false : true;
                        result.Amount = tx.LimitAmount;
                        return result;
                    }
                case TxResultType.RelationDel:
                    {
                        var result = new RelationDelTxResult();
                        result.CounterParty = tx.Target == account ? tx.Account : tx.Target;
                        result.RelationType = tx.RelationType == 3 ? (RelationType?)RelationType.Unfreeze : null;
                        result.IsActive = tx.Target != account; //account === tx.Target ? false : true;
                        result.Amount = tx.LimitAmount;
                        return result;
                    }
                case TxResultType.ConfigContract:
                    {
                        ConfigContractTxResult contractResult = null;

                        if (tx.Method == 0)
                        {
                            var result = new DeployContractTxResult();
                            contractResult = result;
                            result.Payload = tx.Payload;
                        }
                        else
                        {
                            var result = new CallContractTxResult();
                            contractResult = result;
                            result.Destination = tx.Destination;
                            result.Foo = tx.ContractMethod;
                        }

                        if (contractResult != null)
                        {
                            contractResult.Params = FormatContractArgs(tx.Args);
                        }
                        return contractResult;
                    }
                case TxResultType.AccountSet:
                    {
                        var result = new AccountSetTxResult();
                        result.ClearFlag = tx.ClearFlag;
                        result.SetFlag = tx.SetFlag;
                        return result;
                    }
                case TxResultType.SetRegularKey:
                    {
                        var result = new SetRegularKeyTxResult();
                        result.RegularKey = tx.RegularKey;
                        return result;
                    }
                case TxResultType.SignSet:
                    // TODO parse other type
                    return new SignSetTxResult();
                case TxResultType.Operation:
                    // TODO parse other type
                    return new OperationTxResult();
                case TxResultType.OfferEffect:
                    // TODO parse other type
                    return new OfferEffectTxResult();
                case TxResultType.Unknown:
                default:
                    // TODO parse other type
                    return new UnknownTxResult();
            }
        }

        private static Amount ReserveAmount(Amount amount, string account)
        {
            var value = decimal.Parse(amount.Value);
            value = -value;
            return new Amount { Currency = amount.Currency, Issuer = account, Value = value.ToString() };
        }

        private static string[] FormatContractArgs(ArgData[] args)
        {
            if (args == null || args.Length == 0) return new string[0];
            return args.Select(arg => arg.Arg.Parameter).ToArray();
        }

        private static TxResultType GetTxType(Tx tx, string account)
        {
            if (tx.Account == account || tx.Target == account || tx.Destination == account || (tx.LimitAmount != null && tx.LimitAmount.Issuer == account))
            {
                switch (tx.TransactionType)
                {
                    case TransactionType.Payment:
                        return tx.Account == account ? (tx.Destination == account ? TxResultType.Convert : TxResultType.Sent) : TxResultType.Received;
                    case TransactionType.OfferCreate:
                        return TxResultType.OfferNew;
                    case TransactionType.OfferCancel:
                        return TxResultType.OfferCancel;
                    case TransactionType.TrustSet:
                        return tx.Account == account ? TxResultType.Trusting : TxResultType.Trusted;
                    case TransactionType.RelationDel:
                        return TxResultType.RelationDel;
                    case TransactionType.AccountSet:
                        return TxResultType.AccountSet;
                    case TransactionType.SetRegularKey:
                        return TxResultType.SetRegularKey;
                    case TransactionType.RelationSet:
                        return TxResultType.RelationSet;
                    case TransactionType.SignSet:
                        return TxResultType.SignSet;
                    case TransactionType.Operation:
                        return TxResultType.Operation;
                    case TransactionType.ConfigContract:
                        return TxResultType.ConfigContract;
                    default:
                        // TODO CHECK
                        return TxResultType.Unknown;
                }
            }
            else
            {
                return TxResultType.OfferEffect;
            }
        }
    }
}
