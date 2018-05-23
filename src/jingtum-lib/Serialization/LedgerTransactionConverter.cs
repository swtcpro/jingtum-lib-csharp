using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class LedgerTransactionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LedgerTransaction);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType== JsonToken.String)
            {
                var value = reader.Value as string;
                if (value == null) return null;

                var tx = new LedgerTransaction();
                tx.Hash = value;
                return tx;
            }
            else if(reader.TokenType == JsonToken.StartObject)
            {
                var txObject = JObject.Load(reader);
                var tx = new LedgerTransaction();
                serializer.Populate(txObject.CreateReader(), tx);
                tx.RawData = txObject.ToString();
                tx.IsExpanded = true;
                return tx;
            }

            return null;
        }
    }
}
