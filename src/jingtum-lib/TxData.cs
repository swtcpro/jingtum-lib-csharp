using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JingTum.Lib
{
    internal class TxData : ICloneable
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public UInt32 Flags { get; set; }
        // Amount(long; decimal, string, AmountSettings)
        public object Fee { get; set; }
        public string Account { get; set; }
        public PathComputed[][] Paths { get; set; }
        //Amount(decimal, string, AmountSettings)
        public object SendMax { get; set; }
        public UInt32? TransferRate { get; set; }
        public List<MemoInfo> Memos { get; set; }
        public UInt32? Sequence { get; set; }
        //Amount(decimal, string, AmountSettings)
        public object Amount { get; set; }
        //string for remote sign, int for local sign
        public object TransactionType { get; set; }
        public string SigningPubKey { get; internal set; }
        public string TxnSignature { get; internal set; }

        // not public, for sign result
        internal string Blob { get; set; }

        // not public, for exception tracking
        internal Exception Exception { get; set; }

        public virtual object Clone()
        {
            var that = (TxData)Activator.CreateInstance(GetType());
            that.Flags = Flags;
            that.Fee = Fee;
            that.Account = Account;
            that.Paths = Paths;
            if (SendMax is ICloneable sendMaxClone)
            {
                that.SendMax = sendMaxClone.Clone();
            }
            else
            {
                that.SendMax = SendMax;
            }

            that.TransferRate = TransferRate;
            if(Memos != null)
            {
                that.Memos = new List<MemoInfo>();
                foreach(var memo in Memos)
                {
                    that.Memos.Add((MemoInfo)memo.Clone());
                }
            }

            that.Exception = Exception;
            if (Amount is ICloneable amountClone)
            {
                that.Amount = amountClone.Clone();
            }
            else
            {
                that.Amount = Amount;
            }

            that.Sequence = Sequence;
            that.TransactionType = TransactionType;
            that.Blob = Blob;
            that.SigningPubKey = SigningPubKey;
            that.TxnSignature = TxnSignature;

            return that;
        }
    }

    internal class MemoInfo : ICloneable
    {
        public MemoDataInfo Memo { get; set; }

        public object Clone()
        {
            return new MemoInfo
            {
                Memo = Memo != null ? (MemoDataInfo)Memo.Clone() : null
            };
        }
    }

    internal class MemoDataInfo : ICloneable
    {
        public string MemoData { get; set; }

        public string MemoFormat { get; internal set; }

        public string MemoType { get; internal set; }

        public object Clone()
        {
            return new MemoDataInfo
            {
                MemoData = MemoData,
                MemoType = MemoType,
                MemoFormat= MemoFormat
            };
        }
    }

    internal class PaymentTxData : TxData
    {
        public string Destination { get; set; }

        public override object Clone()
        {
            var that = (PaymentTxData)base.Clone();
            that.Destination = Destination;
            return that;
        }
    }

    internal class ContractTxData : TxData
    {
        public string Destination { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int Method { get; set; }
        public string Payload { get; set; }
        public IEnumerable<ArgInfo> Args { get; set; }
        public string ContractMethod { get; internal set; }

        public override object Clone()
        {
            var that = (ContractTxData)base.Clone();
            that.Destination = Destination;
            that.Method = Method;
            that.Payload = Payload;
            that.ContractMethod = ContractMethod;
            if(Args != null)
            {
                var args = new List<ArgInfo>();
                foreach(var arg in Args)
                {
                    args.Add((ArgInfo)arg.Clone());
                }

                that.Args = args;
            }
            return that;
        }
    }

    internal class ArgInfo : ICloneable
    {
        public ParameterInfo Arg { get; internal set; }

        public object Clone()
        {
            var that = new ArgInfo();
            that.Arg = (ParameterInfo)Arg.Clone();
            return that;
        }
    }

    public class ParameterInfo : ICloneable
    {
        public string Parameter { get; internal set; }

        public object Clone()
        {
            var that = new ParameterInfo { Parameter = Parameter };
            return that;
        }
    }

    internal class RelationTxData : TxData
    {
        public AmountSettings LimitAmount { get; internal set; }
        public UInt32? QualityIn { get; internal set; }
        public UInt32? QualityOut { get; internal set; }
        public string Target { get; internal set; }
        public UInt32? RelationType { get; internal set; }

        public override object Clone()
        {
            var that = (RelationTxData)base.Clone();
            that.LimitAmount = LimitAmount == null ?  null : (AmountSettings)LimitAmount.Clone();
            that.QualityIn = QualityIn;
            that.QualityOut = QualityOut;
            that.Target = Target;
            that.RelationType = RelationType;
            return that;
        }
    }

    internal class AccountSetTxData : TxData
    {
        public UInt32? ClearFlag { get; internal set; }
        public string RegularKey { get; internal set; }
        public UInt32? SetFlag { get; internal set; }

        public override object Clone()
        {
            var that = (AccountSetTxData)base.Clone();
            that.SetFlag = SetFlag;
            that.ClearFlag = ClearFlag;
            that.RegularKey = RegularKey;
            return that;
        }
    }

    internal class OfferCreateTxData : TxData
    {
        public object TakerPays { get; set; }
        public object TakerGets { get; set; }

        public override object Clone()
        {
            var that = (OfferCreateTxData)base.Clone();
            if (TakerPays is ICloneable takerPaysClone)
            {
                that.TakerPays = takerPaysClone.Clone();
            }
            else
            {
                that.TakerPays = TakerPays;
            }

            if (TakerGets is ICloneable takerGetsClone)
            {
                that.TakerGets = takerGetsClone.Clone();
            }
            else
            {
                that.TakerGets = TakerGets;
            }
            return that;
        }
    }

    internal class OfferCancelTxData : TxData
    {
        public UInt32? OfferSequence { get; set; }

        public override object Clone()
        {
            var that = (OfferCancelTxData)base.Clone();
            that.OfferSequence = OfferSequence;
            return that;
        }
    }
}
