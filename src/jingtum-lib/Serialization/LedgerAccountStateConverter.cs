using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class LedgerAccountStateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AccountState);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.String)
            {
                var value = reader.Value as string;
                if (value == null) return null;

                var state = new SimpleAccountState();
                state.Index = value;
                return state;
            }
            else if(reader.TokenType == JsonToken.StartObject)
            {
                var stateObject = JObject.Load(reader);
                var entryTypeNode = stateObject["LedgerEntryType"];
                if (entryTypeNode == null) return null;

                AccountState state;
                var entryType = entryTypeNode.Value<string>();
                switch (entryType)
                {
                    case "LedgerHashes":
                        state = new LedgerHashesAccountState();
                        break;
                    case "AccountRoot":
                        state = new AccountRootAccountState();
                        break;
                    case "State":
                        state = new StateAccountState();
                        break;
                    case "FeeSettings":
                        state = new FeeSettingsAccountState();
                        break;
                    case "DirectoryNode":
                        state = new DirectoryNodeAccountState();
                        break;
                    case "SkywellState":
                        state = new SkywellStateAccountState();
                        break;
                    case "ManageIssuer":
                        state = new ManageIssuerAccountState();
                        break;
                    default:
                        state = new UnknownAccountState(entryType);
                        break;
                }

                serializer.Populate(stateObject.CreateReader(), state);
                state.RawData = stateObject.ToString();
                return state;
            }

            return null;
        }
    }
}
