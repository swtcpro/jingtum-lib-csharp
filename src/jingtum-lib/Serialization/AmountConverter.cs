using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class AmountConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = reader.Value as string;
                if (value == null) return null;

                try
                {
                    var amount = decimal.Parse(value);
                    amount = Decimal.Divide(amount, new decimal(1000000d));
                    return new Amount { Currency = "SWT", Issuer = "", Value = amount.ToString("0.######") };
                }
                catch
                {
                    return null;
                }
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                var amountObject = JObject.Load(reader);
                var amount = new Amount();
                serializer.Populate(amountObject.CreateReader(), amount);
                //var currencty = amountObject.GetValue("currency");
                //if (currencty != null)
                //{
                //    amount.Currency = currencty.Value<string>();
                //}
                //var issuer = amountObject.GetValue("issuer");
                //if (issuer != null)
                //{
                //    amount.Issuer = issuer.Value<string>();
                //}
                //var value = amountObject.GetValue("value");
                //if (value != null)
                //{
                //    amount.Value = value.Value<string>();
                //}
                return amount;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Amount);
        }
    }
}
