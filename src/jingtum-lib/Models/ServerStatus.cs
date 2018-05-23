using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the server status.
    /// </summary>
    public class ServerStatus
    {
        [JsonProperty("fee_base")]
        public int FeeBase { get; internal set; }
        [JsonProperty("fee_ref")]
        public int FeeRef { get; internal set; }
        [JsonProperty("ledger_index")]
        public long LedgerIndex { get; internal set; }
        [JsonProperty("ledger_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LedgerTime { get; internal set; }
        [JsonProperty("pubkey_node")]
        public string PubkeyNode { get; internal set; }
        [JsonProperty("load_base")]
        public int LoadBase { get; internal set; }
        [JsonProperty("load_factor")]
        public int LoadFactor { get; internal set; }
        [JsonProperty("reserve_base")]
        public int ReserveBase { get; internal set; }
        [JsonProperty("reserve_inc")]
        public int ReserveInc { get; internal set; }
        [JsonProperty("server_status")]
        public string Status { get; internal set; }
    }
}
