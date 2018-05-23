using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class EnumConverter<T> : JsonConverter where T:struct
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.String)
            {
                var value = reader.Value as string;
                if (value == null) return default(T);

                value = value.Replace("_", "");
                T result = default(T);
                Enum.TryParse(value, true, out result);
                return result;
            }
            else if(reader.TokenType == JsonToken.Integer)
            {
                T result = (T)Enum.ToObject(typeof(T), reader.Value);
                if (Enum.IsDefined(typeof(T), result))
                {
                    return result;
                }
                else
                {
                    return default(T);
                }
            }

            return default(T);
        }
    }

    internal class NullableEnumConverter<T> : JsonConverter where T : struct
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Nullable<T>);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = reader.Value as string;
                if (value == null) return default(T);

                value = value.Replace("_", "");
                T result = default(T);
                if(Enum.TryParse(value, true, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                T result = (T)Enum.ToObject(typeof(T), reader.Value);
                if (Enum.IsDefined(typeof(T), result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
    }
}
