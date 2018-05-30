using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal static class SerializedTypeHelper
    {
        public static readonly Dictionary<string, KeyValuePair<int, int>> InverseFieldsMap = new Dictionary<string, KeyValuePair<int, int>>
        {
            // Int16
            { "LedgerEntryType", new KeyValuePair<int, int>(1,1) },
            { "TransactionType", new KeyValuePair<int, int>(1, 2) },
            // Int32
            { "Flags", new KeyValuePair<int, int>(2, 2) },
            { "SourceTag", new KeyValuePair<int, int>(2, 3) },
            { "Sequence", new KeyValuePair<int, int>(2, 4) },
            { "PreviousTxnLgrSeq", new KeyValuePair<int, int>(2, 5) },
            { "LedgerSequence", new KeyValuePair<int, int>(2, 6) },
            { "CloseTime", new KeyValuePair<int, int>(2, 7) },
            { "ParentCloseTime", new KeyValuePair<int, int>(2, 8) },
            { "SigningTime", new KeyValuePair<int, int>(2, 9) },
            { "Expiration", new KeyValuePair<int, int>(2, 10) },
            { "TransferRate", new KeyValuePair<int, int>(2, 11) },
            { "WalletSize", new KeyValuePair<int, int>(2, 12) },
            { "OwnerCount", new KeyValuePair<int, int>(2, 13) },
            { "DestinationTag", new KeyValuePair<int, int>(2, 14) },
            { "Timestamp", new KeyValuePair<int, int>(2, 15) },
            { "HighQualityIn", new KeyValuePair<int, int>(2, 16) },
            { "HighQualityOut", new KeyValuePair<int, int>(2, 17) },
            { "LowQualityIn", new KeyValuePair<int, int>(2, 18) },
            { "LowQualityOut", new KeyValuePair<int, int>(2, 19) },
            { "QualityIn", new KeyValuePair<int, int>(2, 20) },
            { "QualityOut", new KeyValuePair<int, int>(2, 21) },
            { "StampEscrow", new KeyValuePair<int, int>(2, 22) },
            { "BondAmount", new KeyValuePair<int, int>(2, 23) },
            { "LoadFee", new KeyValuePair<int, int>(2, 24) },
            { "OfferSequence", new KeyValuePair<int, int>(2, 25) },
            { "FirstLedgerSequence", new KeyValuePair<int, int>(2, 26) },
            { "LastLedgerSequence", new KeyValuePair<int, int>(2, 27) },
            { "TransactionIndex", new KeyValuePair<int, int>(2, 28) },
            { "OperationLimit", new KeyValuePair<int, int>(2, 29) },
            { "ReferenceFeeUnits", new KeyValuePair<int, int>(2, 30) },
            { "ReserveBase", new KeyValuePair<int, int>(2, 31) },
            { "ReserveIncrement", new KeyValuePair<int, int>(2, 32) },
            { "SetFlag", new KeyValuePair<int, int>(2, 33) },
            { "ClearFlag", new KeyValuePair<int, int>(2, 34) },
            { "RelationType", new KeyValuePair<int, int>(2, 35) },
            { "Method", new KeyValuePair<int, int>(2, 36) },
            { "Contracttype", new KeyValuePair<int, int>(2, 39) },
            //Int64
            { "IndexNext", new KeyValuePair<int, int>(3, 1) },
            { "IndexPrevious", new KeyValuePair<int, int>(3, 2) },
            { "BookNode", new KeyValuePair<int, int>(3, 3) },
            { "OwnerNode", new KeyValuePair<int, int>(3, 4) },
            { "BaseFee", new KeyValuePair<int, int>(3, 5) },
            { "ExchangeRate", new KeyValuePair<int, int>(3, 6) },
            { "LowNode", new KeyValuePair<int, int>(3, 7) },
            { "HighNode", new KeyValuePair<int, int>(3, 8) },
            //Hash128
            { "EmailHash", new KeyValuePair<int, int>(4, 1) },
            //Hash256
            { "LedgerHash", new KeyValuePair<int, int>(5, 1) },
            { "ParentHash", new KeyValuePair<int, int>(5, 2) },
            { "TransactionHash", new KeyValuePair<int, int>(5, 3) },
            { "AccountHash", new KeyValuePair<int, int>(5, 4) },
            { "PreviousTxnID", new KeyValuePair<int, int>(5, 5) },
            { "LedgerIndex", new KeyValuePair<int, int>(5, 6) },
            { "WalletLocator", new KeyValuePair<int, int>(5, 7) },
            { "RootIndex", new KeyValuePair<int, int>(5, 8) },
            { "AccountTxnID", new KeyValuePair<int, int>(5, 9) },
            { "BookDirectory", new KeyValuePair<int, int>(5, 16) },
            { "InvoiceID", new KeyValuePair<int, int>(5, 17) },
            { "Nickname", new KeyValuePair<int, int>(5, 18) },
            { "Amendment", new KeyValuePair<int, int>(5, 19) },
            { "TicketID", new KeyValuePair<int, int>(5, 20) },
            //Amount
            { "Amount", new KeyValuePair<int, int>(6, 1) },
            { "Balance", new KeyValuePair<int, int>(6, 2) },
            { "LimitAmount", new KeyValuePair<int, int>(6, 3) },
            { "TakerPays", new KeyValuePair<int, int>(6, 4) },
            { "TakerGets", new KeyValuePair<int, int>(6, 5) },
            { "LowLimit", new KeyValuePair<int, int>(6, 6) },
            { "HighLimit", new KeyValuePair<int, int>(6, 7) },
            { "Fee", new KeyValuePair<int, int>(6, 8) },
            { "SendMax", new KeyValuePair<int, int>(6, 9) },
            { "MinimumOffer", new KeyValuePair<int, int>(6, 16) },
            { "JingtumEscrow", new KeyValuePair<int, int>(6, 17) },
            { "DeliveredAmount", new KeyValuePair<int, int>(6, 18) },
            //VL
            { "PublicKey", new KeyValuePair<int, int>(7, 1) },
            { "MessageKey", new KeyValuePair<int, int>(7, 2) },
            { "SigningPubKey", new KeyValuePair<int, int>(7, 3) },
            { "TxnSignature", new KeyValuePair<int, int>(7, 4) },
            { "Generator", new KeyValuePair<int, int>(7, 5) },
            { "Signature", new KeyValuePair<int, int>(7, 6) },
            { "Domain", new KeyValuePair<int, int>(7, 7) },
            { "FundCode", new KeyValuePair<int, int>(7, 8) },
            { "RemoveCode", new KeyValuePair<int, int>(7, 9) },
            { "ExpireCode", new KeyValuePair<int, int>(7, 10) },
            { "CreateCode", new KeyValuePair<int, int>(7, 11) },
            { "MemoType", new KeyValuePair<int, int>(7, 12) },
            { "MemoData", new KeyValuePair<int, int>(7, 13) },
            { "MemoFormat", new KeyValuePair<int, int>(7, 14) },
            { "Payload", new KeyValuePair<int, int>(7, 15) },
            { "ContractMethod", new KeyValuePair<int, int>(7, 17) },
            { "Parameter", new KeyValuePair<int, int>(7, 18) },
            //Acount
            { "Account", new KeyValuePair<int, int>(8, 1) },
            { "Owner", new KeyValuePair<int, int>(8, 2) },
            { "Destination", new KeyValuePair<int, int>(8, 3) },
            { "Issuer", new KeyValuePair<int, int>(8, 4) },
            { "Target", new KeyValuePair<int, int>(8, 7) },
            { "RegularKey", new KeyValuePair<int, int>(8, 8) },
            { "undefined", new KeyValuePair<int, int>(15, 1) },
            // Object
            // 1-> endof object
            { "TransactionMetaData", new KeyValuePair<int, int>(14, 2) },
            { "CreatedNode", new KeyValuePair<int, int>(14, 3) },
            { "DeletedNode", new KeyValuePair<int, int>(14, 4) },
            { "ModifiedNode", new KeyValuePair<int, int>(14, 5) },
            { "PreviousFields", new KeyValuePair<int, int>(14, 6) },
            { "FinalFields", new KeyValuePair<int, int>(14, 7) },
            { "NewFields", new KeyValuePair<int, int>(14, 8) },
            { "TemplateEntry", new KeyValuePair<int, int>(14, 9) },
            { "Memo", new KeyValuePair<int, int>(14, 10) },
            { "Arg", new KeyValuePair<int, int>(14, 11) },
            //Array
            { "SigningAccounts", new KeyValuePair<int, int>(15, 2) },
            { "TxnSignatures", new KeyValuePair<int, int>(15, 3) },
            { "Signatures", new KeyValuePair<int, int>(15, 4) },
            { "Template", new KeyValuePair<int, int>(15, 5) },
            { "Necessary", new KeyValuePair<int, int>(15, 6) },
            { "Sufficient", new KeyValuePair<int, int>(15, 7) },
            { "AffectedNodes", new KeyValuePair<int, int>(15, 8) },
            { "Memos", new KeyValuePair<int, int>(15, 9) },
            { "Args", new KeyValuePair<int, int>(15, 10) },
            //Int8
            { "CloseResolution", new KeyValuePair<int, int>(16, 1) },
            { "TemplateEntryType", new KeyValuePair<int, int>(16, 2) },
            { "TransactionResult", new KeyValuePair<int, int>(16, 3) },
            //Hash160
            { "TakerPaysCurrency", new KeyValuePair<int, int>(17, 1) },
            { "TakerPaysIssuer", new KeyValuePair<int, int>(17, 2) },
            { "TakerGetsCurrency", new KeyValuePair<int, int>(17, 3) },
            { "TakerGetsIssuer", new KeyValuePair<int, int>(17, 4) },
            //PathSet
            { "Paths", new KeyValuePair<int, int>(18, 1) },
            //Vector256
            { "Indexes", new KeyValuePair<int, int>(19, 1) },
            { "Hashes", new KeyValuePair<int, int>(19, 2) },
            { "Amendments", new KeyValuePair<int, int>(19, 3)}
        };

        private class PropertyComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                var xMap = InverseFieldsMap[x];
                var xTypeBits = xMap.Key;
                var xFieldBits = xMap.Value;

                var yMap = InverseFieldsMap[y];
                var yTypeBits = yMap.Key;
                var yFieldBits = yMap.Value;

                // Sort by type id first, then by field id
                return xTypeBits != yTypeBits ? xTypeBits - yTypeBits : xFieldBits - yFieldBits;
            }
        }

        public static PropertyInfo[] SortProperties(PropertyInfo[] properties)
        {
            return properties.OrderBy(p => p.Name,new PropertyComparer()).ToArray();
        }

        public static string ConvertStringToHex(string str)
        {
            var hexList = Encoding.UTF8.GetBytes(str).Select(b => b.ToString("X2"));
            return string.Join("", hexList);
        }

        public static byte[] ConvertAddressToBytes(string address)
        {
            return Core.Config.GetB58IdentiferCodecs().DecodeAddress(address);
        }

        public static void SerializeHex(Serializer so, string val, bool noLength = false)
        {
            var bytes = Utils.HexToBytes(val);
            if (bytes.Length == 0)
            {
                bytes = new byte[] { 0 };
            }

            if(!noLength)
            {
                SerializeVarint(so, (uint)bytes.Length);
            }

            so.Append(bytes);
        }

        public static void SerializeVarint(Serializer so, uint val)
        {
            if (val < 0)
            {
                throw new ArgumentOutOfRangeException("Variable integers are unsigned.");
            }

            if (val <= 192)
            {
                so.Append(new byte[] { (byte)val });
            }
            else if (val <= 12480)
            {
                val -= 193;
                so.Append(new byte[] { (byte)(193 + (val >> 8)), (byte)(val & 0xff) });
            }
            else if (val <= 918744)
            {
                val -= 12481;
                so.Append(new byte[] { (byte)(241 + (val >> 16)), (byte)(val >> 8 & 0xff), (byte)(val & 0xff) });
            }
            else
            {
                throw new ArgumentOutOfRangeException("Variable integer overflow.");
            }
        }

        public static SerializedHash256 STHash256 = new SerializedHash256();
        public static SerializedInt8 STInt8 = new SerializedInt8();
        public static SerializedInt32 STInt32 = new SerializedInt32();
        public static SerializedMemo STMemo = new SerializedMemo();
        public static SerializedArg STArg = new SerializedArg();
        public static SerializedObject STObject = new SerializedObject();

        public static readonly IDictionary<int, ISerializedType> _typesMap = new Dictionary<int, ISerializedType>
        {
            { 1, new SerializedInt16() },
            { 2, STInt32 },
            { 3, new SerializedInt64() },
            { 4, new SerializedHash128() },
            { 5, STHash256 },
            { 6, new SerializedAmount() },
            { 7, new SerializedVariableLength() },
            { 8, new SerializedAccount() },
            { 14, STObject },
            { 15, new SerializedArray() },

            //Uncommon
            { 16, STInt8 },
            { 17, new SerializedHash160() },
            { 18, new SerializedPathSet() },
            { 19, new SerializedVector256() },
        };

        public static void Serialize(Serializer so, string fieldName, object value)
        {
            var fieldCoordinates = InverseFieldsMap[fieldName];
            var typeBits = fieldCoordinates.Key;
            var fieldBits = fieldCoordinates.Value;
            var tagByte = (typeBits < 16 ? typeBits << 4 : 0) | (fieldBits < 16 ? fieldBits : 0);

            if (value is string str)
            {
                if (fieldName == "LedgerEntryType")
                {
                    value = (int)Enum.Parse(typeof(LedgerEntryType), str);
                }
                else if (fieldName == "TransactionResult")
                {
                    value = (int)Enum.Parse(typeof(TransactionType), str);
                }
            }

            STInt8.Serialize(so, (byte)tagByte);

            if(typeBits >= 16)
            {
                STInt8.Serialize(so, (byte)typeBits);
            }

            if (fieldBits >= 16)
            {
                STInt8.Serialize(so, (byte)fieldBits);
            }

            ISerializedType serializedType = null;
            if(fieldName == "Memo" && value is MemoDataInfo)
            {
                serializedType = STMemo;
            }
            else if(fieldName == "Arg" && value is ParameterInfo)
            {
                serializedType = STArg;
            }
            else
            {
                serializedType = _typesMap[typeBits];
            }

            try
            {
                serializedType.Serialize(so, value);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Field with name '{fieldName}' cannot be serialized.", ex);
            }
        }
    }
}
