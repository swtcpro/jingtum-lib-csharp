using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class TumAmount
    {
        private static readonly BigInteger bi_xns_max = new BigInteger("9000000000000000000"); //9e18 // Json wire limit.
        private static readonly BigInteger bi_xns_min = new BigInteger("-9000000000000000000"); //-9e18 // Json wire limit.
        private const int CURRENCY_NAME_LEN = 3;//货币长度
        private const int CURRENCY_NAME_LEN2 = 6;//货币长度

        private BigInteger _value;
        private int _offerset = 0;
        private bool _isNative = true;
        private bool _isNegative = false;
        private string _currency;
        private string _issuer;

        //Only check the value of the Amount
        public bool IsValid => _value != null;

        public bool IsNative => _isNative;

        public bool IsNegative => _isNegative;

        public bool IsPositive => !IsZero && !IsNegative;

        public bool IsZero => _value == BigInteger.Zero;

        public string Currency => _currency;

        public string Issuer => _issuer;

        public BigInteger Value => _value;

        public int Offset => _offerset;

        public static TumAmount FromJson(object json)
        {
            var amount = new TumAmount();
            amount.ParseJson(json);
            return amount;
        }

        private void ParseJson(object json)
        {
            if (Utils.IsNumber(json))
            {
                ParseSwtValue(json.ToString());
            }
            else if (json is string)
            {
                ParseSwtValue((string)json);
            }
            else if (json is AmountSettings)
            {
                var amount = json as AmountSettings;
                if(amount.Currency == Config.Currency)
                {
                    ParseSwtValue(amount.Value.ToString());
                }
                else
                {
                    _currency = amount.Currency;
                    _isNative = false;
                    if (string.IsNullOrEmpty(amount.Issuer) || !Utils.IsValidAddress(amount.Issuer))
                    {
                        throw new Exception("Input Amount has invalid issuer info!");
                    }
                    else
                    {
                        _issuer = amount.Issuer;

                        decimal value;
                        if(!decimal.TryParse(amount.Value, out value))
                        {
                            throw new Exception("Input Amount has invalid value!");
                        }

                        var valueString = value.ToString("E16", CultureInfo.InvariantCulture);
                        var vpow = int.Parse(valueString.Substring(valueString.IndexOf("E") + 1));
                        var offset = 15 - vpow;
                        var factor = Math.Pow(10, offset);
                        _value = new BigInteger((value * new decimal(factor)).ToString("0"));
                        _offerset = -offset;
                    }
                }
            }
        }

        private void ParseSwtValue(string json)
        {
            decimal value;
            if (!decimal.TryParse(json, out value))
            {
                _value = null;
                return;
            }

            _isNative = true;
            _offerset = 0;
            _isNegative = value < 0;
            if (_isNegative)
            {
                value = -value;
            }

            value = value * 1000000;
            _value = new BigInteger(value.ToString("0"));
            if (_value.CompareTo(bi_xns_max) > 0)
            {
                _value = null;
            }
        }

        public byte[] TumToBytes()
        {
            var currencyData = new byte[20];

            //Only handle the currency with correct symbol
            if (_currency.Length >= CURRENCY_NAME_LEN && _currency.Length <= CURRENCY_NAME_LEN2)
            {
                var currencyCode = _currency;//区分大小写
                var end = 14;
                var len = currencyCode.Length - 1;
                for (var j = len; j >= 0; j--)
                {
                    currencyData[end - j] = (byte)(currencyCode[len - j] & 0xff);
                }
            }
            else if (_currency.Length == 40)
            {
                //for TUM code start with 8
                //should be HEX code
                try
                {
                    var value = new BigInteger(_currency, 16);
                    var bytes = value.ToByteArray();
                    Array.Copy(bytes, 0, currencyData, currencyData.Length - bytes.Length, bytes.Length);
                }
                catch
                {
                    throw new Exception("Invalid currency code.");
                }
            }
            else
            {
                throw new Exception("Incorrect currency code length.");
            }

            return currencyData;
        }
    }
}
