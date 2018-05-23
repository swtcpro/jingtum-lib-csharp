using Newtonsoft.Json;
using Org.BouncyCastle.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JingTum.Lib
{
    internal interface ISerializedType<T>
    {
        void Serialize(Serializer so, T val, bool noMarker = false);
        T Parse(Serializer so);
    }

    internal interface ISerializedType
    {
        void Serialize(Serializer so, object val, bool noMarker = false);
        object Parse(Serializer so);
    }

    internal class SerializedInt8 : ISerializedType<byte>, ISerializedType
    {
        public byte Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, byte value, bool noMarker = false)
        {
            so.Append(new byte[] { value });
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            var ov = (byte)Convert.ChangeType(val, typeof(byte));
            Serialize(so, ov, noMarker);
        }
    }

    internal class SerializedInt16 : ISerializedType<ushort>, ISerializedType
    {
        public ushort Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, ushort val, bool noMarker = false)
        {
            so.Append(Bits.GetBytes(val));
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            var ov = (ushort)Convert.ChangeType(val, typeof(ushort));
            Serialize(so, ov, noMarker);
        }
    }

    internal class SerializedMemo : ISerializedType<MemoDataInfo>, ISerializedType
    {
        public MemoDataInfo Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, MemoDataInfo val, bool noMarker = false)
        {
            var properties = val.GetType().GetProperties()
                .Where(p => p.Name != p.Name.ToLower())
                .ToArray();

            var invalid = properties.FirstOrDefault(p => !SerializedTypeHelper.InverseFieldsMap.ContainsKey(p.Name));
            if (invalid != null)
            {
                throw new InvalidOperationException("JSON contains unknown field: '" + invalid.Name + "'");
            }

            properties = SerializedTypeHelper.SortProperties(properties);

            // memo format always be string
            var isJson = val.MemoFormat == "json";

            foreach(var property in properties)
            {
                object value = property.GetValue(val);
                if (value == null) continue;

                switch (property.Name)
                {
                    case "MemoType":
                        value = SerializedTypeHelper.ConvertStringToHex(val.MemoType);
                        break;
                    case "MemoFormat":
                        value = SerializedTypeHelper.ConvertStringToHex(val.MemoFormat);
                        break;
                    case "MemoData":
                        if(val.MemoData is string str)
                        {
                            value = SerializedTypeHelper.ConvertStringToHex(str);
                            break;
                        }

                        if(isJson)
                        {
                            var json = JsonConvert.SerializeObject(val.MemoData);
                            value = SerializedTypeHelper.ConvertStringToHex(json);
                            break;
                        }

                        throw new InvalidOperationException("MemoData can only be a JSON object with a valid json MemoFormat.");
                }

                SerializedTypeHelper.Serialize(so, property.Name, value);

            }

            if(!noMarker)
            {
                //Object ending marker
                SerializedTypeHelper.STInt8.Serialize(so, 0xe1);
            }
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (MemoDataInfo)val, noMarker);
        }
    }

    internal class SerializedArg : ISerializedType<ParameterInfo>, ISerializedType
    {
        public ParameterInfo Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, ParameterInfo val, bool noMarker = false)
        {
            var properties = val.GetType().GetProperties()
                .Where(p => p.Name != p.Name.ToLower())
                .ToArray();

            var invalid = properties.FirstOrDefault(p => !SerializedTypeHelper.InverseFieldsMap.ContainsKey(p.Name));
            if (invalid != null)
            {
                throw new InvalidOperationException("JSON contains unknown field: '" + invalid.Name + "'");
            }

            properties = SerializedTypeHelper.SortProperties(properties);

            foreach (var property in properties)
            {
                object value = property.GetValue(val);
                if (value == null) continue;

                switch (property.Name)
                {
                    case "Parameter":
                        break;
                }

                SerializedTypeHelper.Serialize(so, property.Name, value);

            }

            if (!noMarker)
            {
                //Object ending marker
                SerializedTypeHelper.STInt8.Serialize(so, 0xe1);
            }
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (ParameterInfo)val, noMarker);
        }
    }

    internal class SerializedInt32 : ISerializedType<uint>, ISerializedType
    {
        public uint Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, uint val, bool noMarker = false)
        {
            so.Append(Bits.GetBytes(val));
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            var ov = (uint)Convert.ChangeType(val, typeof(uint));
            Serialize(so, ov, noMarker);
        }
    }

    internal class SerializedInt64 : ISerializedType<ulong>, ISerializedType
    {
        public ulong Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, ulong val, bool noMarker = false)
        {
            so.Append(Bits.GetBytes(val));
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            ulong? number = Utils.TryGetNumber<ulong>(val, false);
            if(number.HasValue)
            {
                Serialize(so, number.Value, noMarker);
                return;
            }

            if(val is string str)
            {
                if(!Utils.IsHexString(str))
                {
                    throw new ArgumentException("Invalid hex string.");
                }

                if (str.Length > 16)
                {
                    throw new ArgumentException("Int64 is too large.");
                }

                while(str.Length < 16)
                {
                    str = "0" + str;
                }

                SerializedTypeHelper.SerializeHex(so, str, true);
                return;
            }

            throw new ArgumentException("Invalid type for Int64.");
        }
    }

    internal class SerializedHash128 : ISerializedType<string>, ISerializedType
    {
        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
           if(val != null && Regex.IsMatch(val, "^[0-9A-F]{0,32}$", RegexOptions.IgnoreCase) 
                && val.Length <= 32)
            {
                SerializedTypeHelper.SerializeHex(so, val, true);
                return;
            }

            throw new ArgumentException("Invalid Hash128.");
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val, noMarker);
        }
    }

    internal class SerializedHash256 : ISerializedType<string>, ISerializedType
    {
        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
            if (val != null && Regex.IsMatch(val, "^[0-9A-F]{0,64}$", RegexOptions.IgnoreCase)
                 && val.Length <= 64)
            {
                SerializedTypeHelper.SerializeHex(so, val, true);
                return;
            }

            throw new ArgumentException("Invalid Hash256.");
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val, noMarker);
        }
    }

    internal class SerializedAmount : ISerializedType<TumAmount>, ISerializedType
    {
        public TumAmount Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, TumAmount val, bool noMarker = false)
        {
            if (!val.IsValid)
            {
                throw new Exception("Not a valid Amount object.");
            }

            //For SWT, offset is 0
            //only convert the value
            if (val.IsNative)
            {
                var valueHex = val.Value.ToString(16);

                // Enforce correct length (64 bits)
                if (valueHex.Length > 16)
                {
                    throw new Exception("Amount value out of bounds.");
                }

                while (valueHex.Length < 16)
                {
                    valueHex = '0' + valueHex;
                }

                //Convert the HEX value to bytes array
                var valueBytes = Utils.HexToBytes(valueHex);

                // Clear most significant two bits - these bits should already be 0 if
                // Amount enforces the range correctly, but we'll clear them anyway just
                // so this code can make certain guarantees about the encoded value.
                valueBytes[0] &= 0x3f;

                if (!val.IsNegative)
                {
                    valueBytes[0] |= 0x40;
                }

                so.Append(valueBytes);
            }
            else
            {
                //For other non-native currency
                //1. Serialize the currency value with offset
                //Put offset
                long hi = 0, lo = 0;

                // First bit: non-native
                hi |= 1 << 31;

                if (!val.IsZero)
                {
                    // Second bit: non-negative?
                    if (!val.IsNegative)
                    {
                        hi |= 1 << 30;
                    }

                    // Next eight bits: offset/exponent
                    hi |= ((97L + val.Offset) & 0xff) << 22;
                    // Remaining 54 bits: mantissa
                    hi |= val.Value.ShiftRight(32).LongValue & 0x3fffff;
                    lo = val.Value.LongValue & 0xffffffff;
                }

                /** Convert from a bitArray to an array of bytes.
                 **/
                var arr = new long[]{ hi, lo};
                int l = arr.Length;
                long bl;

                if (l == 0)
                {
                    bl = 0;
                }
                else
                {
                    var x = arr[l - 1];
                    var roundX = x / 0x10000000000; // result as Math.Round(x/0x10000000000)
                    if (roundX == 0) roundX = 32;
                    bl = (l - 1) * 32 + roundX;
                }

                //Setup a new byte array and filled the byte data in
                //Results should not longer than 8 bytes as defined earlier
                var tmparray = new List<byte>();

                long tmp = 0;
                for (var i = 0; i < bl / 8; i++)
                {
                    if ((i & 3) == 0)
                    {
                        tmp = arr[i / 4];
                    }
                    tmparray.Add((byte)(tmp >> 24));
                    tmp <<= 8;
                }

                if (tmparray.Count > 8)
                {
                    throw new Exception("Invalid byte array length in AMOUNT value representation");
                }

                var valueBytes = tmparray.ToArray();
                so.Append(valueBytes);

                //2. Serialize the currency info with currency code
                //   and issuer
                //console.log("Serial non-native AMOUNT ......");
                // Currency (160-bit hash)
                var tum_bytes = val.TumToBytes();
                so.Append(tum_bytes);

                // Issuer (160-bit hash)
                //so.append(amount.issuer().to_bytes());
                so.Append(SerializedTypeHelper.ConvertAddressToBytes(val.Issuer));
            }
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            var amount = TumAmount.FromJson(val);
            Serialize(so, amount);
        }
    }

    internal class SerializedCurrency : ISerializedType<string>, ISerializedType
    {
        private const int CURRENCY_NAME_LEN = 3;
        private const int CURRENCY_NAME_LEN2 = 6;

        public static byte[] FromJsonToBytes(string currencty)
        {
            var result = new byte[20];
            if (currencty != null)
            {
                //For Tum code with 40 chars, such as
                //800000000000000000000000A95EFD7EC3101635
                //treat as HEX string, convert to the 20 bytes array
                if (Utils.IsHexString(currencty) && currencty.Length == 40)
                {

                    result = Utils.HexToBytes(currencty);
                }
                else if (Utils.IsValidCurrency(currencty))
                {
                    //For Tum code with 3 letters/digits, such as
                    //CNY, USD, 

                    // the condition is always true
                    if (currencty.Length >= CURRENCY_NAME_LEN && currencty.Length <= CURRENCY_NAME_LEN2)
                    {
                        var end = 14;
                        var len = currencty.Length - 1;
                        for (var x = len; x >= 0; x--)
                        {
                            result[end - x] = (byte)(currencty[len - x] & 0xff);
                        }
                    }
                }
                else
                {

                    //Input not match the naming format
                    throw new Exception("Input tum code invalid!");
                }
            }
            else
            {
                throw new Exception("Input tum code invalid!");
            }

            return result;
        }

        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
            var bytes = FromJsonToBytes(val);
            so.Append(bytes);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val);
        }
    }

    internal class SerializedObject : ISerializedType<object>, ISerializedType
    {
        public object Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, object val, bool noMarker = false)
        {
            var properties = val.GetType().GetProperties()
                .Where(p => p.Name != p.Name.ToLower())
                .ToArray();

            var invalid = properties.FirstOrDefault(p => !SerializedTypeHelper.InverseFieldsMap.ContainsKey(p.Name));
            if (invalid != null)
            {
                throw new InvalidOperationException("JSON contains unknown field: '" + invalid.Name + "'");
            }

            properties = SerializedTypeHelper.SortProperties(properties);

            foreach (var property in properties)
            {
                var pval = property.GetValue(val);
                if (pval == null) continue;

                SerializedTypeHelper.Serialize(so, property.Name, pval);
            }

            if (!noMarker)
            {
                //Object ending marker
                SerializedTypeHelper.STInt8.Serialize(so, 0xe1);
            }
        }
    }

    internal class SerializedArray : ISerializedType<IEnumerable>, ISerializedType
    {
        public IEnumerable Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, IEnumerable val, bool noMarker = false)
        {
            foreach(var item in val)
            {
                if(item == null || item.GetType().IsValueType)
                {
                    throw new ArgumentException("Cannot serialize an array containing null or value type object.");
                }

                var properties = item.GetType().GetProperties();
                if(properties.Length != 1)
                {
                    throw new ArgumentException("Cannot serialize an array containing non-single-key objects");
                }

                var property = properties.First();
                SerializedTypeHelper.Serialize(so, property.Name, property.GetValue(item));
            }

            //Array ending marker
            SerializedTypeHelper.STInt8.Serialize(so, 0xf1);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (IEnumerable)val);
        }
    }

    internal class SerializedHash160 : ISerializedType<string>, ISerializedType
    {
        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
            SerializedTypeHelper.SerializeHex(so, val, true);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val);
        }
    }

    internal class SerializedPathSet : ISerializedType<PathComputed[][]>, ISerializedType
    {
        private const int typeBoundary = 0xff;
        private const int typeEnd = 0x00;
        private const int typeAccount = 0x01;
        private const int typeCurrency = 0x10;
        private const int typeIssuer = 0x20;

        public PathComputed[][] Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, PathComputed[][] val, bool noMarker = false)
        {
            if(val != null)
            {
                for(int i = 0, l = val.Length; i < l; i++)
                {
                    //Boundary,
                    if (i > 0)
                    {
                        SerializedTypeHelper.STInt8.Serialize(so, typeBoundary);
                    }

                    var pathes = val[i];
                    for (int j = 0, l2 = pathes.Length; j < l2; j++)
                    {
                        var entry = pathes[j];

                        byte type;
                        if (entry.Type != null)
                        {
                            type = entry.Type.Value;
                        }
                        else
                        {
                            type = 0;
                            if (entry.Account != null)
                            {
                                type |= typeAccount;
                            }
                            if (entry.Currency != null)
                            {
                                type |= typeCurrency;
                            }
                            if (entry.Issuer != null)
                            {
                                type |= typeIssuer;
                            }
                        }

                        SerializedTypeHelper.STInt8.Serialize(so, type);

                        if (entry.Account != null)
                        {
                            so.Append(SerializedTypeHelper.ConvertAddressToBytes(entry.Account));
                        }

                        if (entry.Currency != null)
                        {
                            var currencyBytes = SerializedCurrency.FromJsonToBytes(entry.Currency);
                            so.Append(currencyBytes);
                        }

                        if (entry.Issuer != null)
                        {
                            so.Append(SerializedTypeHelper.ConvertAddressToBytes(entry.Issuer));
                        }
                    }
                }
            }

            SerializedTypeHelper.STInt8.Serialize(so, typeEnd);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (PathComputed[][])val);
        }
    }

    internal class SerializedVector256 : ISerializedType<IEnumerable<string>>, ISerializedType
    {
        public IEnumerable<string> Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, IEnumerable<string> val, bool noMarker = false)
        {
            var array = val.ToArray();
            SerializedTypeHelper.SerializeVarint(so, (uint)array.Length * 32);
            foreach(var item in array)
            {
                SerializedTypeHelper.STHash256.Serialize(so, item);
            }
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (IEnumerable<string>)val);
        }
    }

    internal class SerializedVariableLength : ISerializedType<string>, ISerializedType
    {
        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
            SerializedTypeHelper.SerializeHex(so, val);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val);
        }
    }

    internal class SerializedAccount : ISerializedType<string>, ISerializedType
    {
        public string Parse(Serializer so)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Serializer so, string val, bool noMarker = false)
        {
            var bytes = Core.Config.GetB58IdentiferCodecs().DecodeAddress(val);
            SerializedTypeHelper.SerializeVarint(so, (uint)bytes.Length);
            so.Append(bytes);
        }

        object ISerializedType.Parse(Serializer so)
        {
            return Parse(so);
        }

        void ISerializedType.Serialize(Serializer so, object val, bool noMarker = false)
        {
            Serialize(so, (string)val);
        }
    }
}