using JingTum.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JingTum.Lib
{
    internal class Utils
    {
        private static Regex CurrencyRegex = new Regex("^([a-zA-Z0-9]{3,6}|[A-F0-9]{40})$", RegexOptions.Compiled);
        public static bool IsValidCurrency(string currency)
        {
            if (string.IsNullOrEmpty(currency))
            {
                return false;
            }
            return CurrencyRegex.IsMatch(currency);
        }

        private static Regex HashRegex = new Regex("^[A-F0-9]{64}$", RegexOptions.Compiled);
        public static bool IsValidHash(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return false;

            return HashRegex.IsMatch(hash);
        }

        public static bool IsValidAddress(string address)
        {
            if (string.IsNullOrEmpty(address)) return false;
            return Wallet.IsValidAddress(address);
        }

        public static bool IsValidAmount(IAmount amount)
        {
            // check amount value
            decimal value;
            if (amount.Value == null || !Decimal.TryParse(amount.Value, out value))
            {
                return false;
            }

            return IsValidAmount0(amount);
        }

        public static bool IsValidAmount0(IAmount amount)
        {
            // check amount currency
            if ( !IsValidCurrency(amount.Currency))
            {
                return false;
            }

            // native currency issuer is empty
            if (amount.Currency == Config.Currency && !string.IsNullOrEmpty(amount.Issuer))
            {
                return false;
            }

            // non native currency issuer is not allowed to be empty
            if (amount.Currency != Config.Currency && !IsValidAddress(amount.Issuer))
            {
                return false;
            }

            return true;
        }

        public static object ToAmount(AmountSettings amount)
        {
            if (amount == null) return null;

            decimal value = 0;
            if (amount.Value != null && !decimal.TryParse(amount.Value, out value))
            {
                return new Exception("Invalid amount value.");
            }

            if (value > new decimal(100000000000))
            {
                return new Exception("Invalid amount: amount\'s maximum value is 100000000000");
            }

            if (amount.Currency == Config.Currency)
            {
                return (value * 1000000).ToString("0");
            }

            return amount;
        }

        public static string Sha1(string data)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var bytes = Encoding.ASCII.GetBytes(data);
            hash.ComputeHash(bytes);
            return BytesToHex(hash.Hash);
        }

        public static string StringToHex(string s)
        {
            var sb = new StringBuilder();
            foreach(var c in s)
            {
                sb.Append(((int)c).ToString("X2"));
            }
            return sb.ToString();
        }

        public static string HexToString(string hex)
        {
            var sb = new StringBuilder();
            int i = 0;
            if (hex.Length % 2 == 1)
            {
                sb.Append((char)Convert.ToInt32(hex.Substring(0, 1), 16));
            }

            for (; i < hex.Length; i += 2)
            {
                sb.Append((char)Convert.ToInt32(hex.Substring(i, 2), 16));
            }

            return sb.ToString();
        }

        public static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach(var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public static byte[] HexToBytes(string hex)
        {
            int i = 0, j = 0;
            byte[] bytes;
            var len = hex.Length / 2;

            if (hex.Length % 2 == 1)
            {
                len += 1;
                bytes = new byte[len];
                bytes[0] = Convert.ToByte(hex.Substring(0, 1), 16);
                i = 1;
                j = 1;
            }
            else
            {
                bytes = new byte[len];
            }

            for (; i < hex.Length; i += 2,j++)
            {
                bytes[j] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static Nullable<T> TryGetNumber<T>(object value, bool parseString = true) where T : struct
        {
            if (value == null) return null;
            if (value is string && parseString)
            {
                decimal dValue;
                if (decimal.TryParse((string)value, out dValue))
                {
                    return (Nullable<T>)Convert.ChangeType(dValue, typeof(T));
                }

                return null;
            }

            if (IsNumber(value))
            {
                return (Nullable<T>)Convert.ChangeType(value, typeof(T));
            }

            return null;
        }

        public static bool IsNumber(object value)
        {
            return (value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal);
        }

        public static DateTime UnitTimeToDateTime(long unitTime)
        {
            return Utility.ConvertUnixTime2DateTime(unitTime + 0x386D4380);
        }

        public static bool IsHexString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
