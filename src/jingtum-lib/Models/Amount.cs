using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the tum amount.
    /// </summary>
#if !NETSTANDARD
    [TypeConverter(typeof(ExpandableObjectConverterEx))]
#endif
    [JsonConverter(typeof(AmountConverter))]
    public class Amount : ICloneable
    {
        /// <summary>
        /// Creates a new instance of <see cref="Amount"/> object.
        /// </summary>
        public Amount()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="Amount"/> object for SWT currency.
        /// </summary>
        /// /// <param name="value">The amount value.</param>
        /// <returns>The <see cref="Amount"/> object.</returns>
        public static Amount SWT(string value = null)
        {
            return new Amount("SWT", "", value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Amount"/> object.
        /// </summary>
        /// <param name="currency">The amount currency.</param>
        /// <param name="issuer">The amount issuer.</param>
        /// <param name="value">The amount value.</param>
        public Amount(string currency, string issuer, string value = null)
        {
            Currency = currency;
            Issuer = issuer;
            Value = value;
        }

        /// <summary>
        /// Optional. The value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
        /// <summary>
        /// Required. The currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Required. The issuer.
        /// </summary>
        /// <remarks>
        /// The issuer is ignored if the currency is "SWT".
        /// </remarks>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            var that = new Amount();
            that.Value = Value;
            that.Currency = Currency;
            that.Issuer = Issuer;
            return that;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Currency == Config.Currency) return Currency;
            return string.Format("{0}:{1}", Currency, Issuer);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var that = obj as Amount;
            if (that == null) return false;

            return this.Currency == that.Currency && this.Issuer == that.Issuer && this.Value == that.Value;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (Currency == null ? 0 : Currency.GetHashCode())
                ^ (Issuer == null ? 0 : Issuer.GetHashCode())
                ^ (Value == null ? 0 : Value.GetHashCode());
        }
    }
}
