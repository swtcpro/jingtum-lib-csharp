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
        /// <summary>
        /// Gets the base fee for calculation formula factor.
        /// </summary>
        [JsonProperty("fee_base")]
        public int FeeBase { get; internal set; }

        /// <summary>
        /// Gets the reference fee for calculation formula factor.
        /// </summary>
        [JsonProperty("fee_ref")]
        public int FeeRef { get; internal set; }

        /// <summary>
        /// Gets the index of the ledger.
        /// </summary>
        [JsonProperty("ledger_index")]
        public UInt32 LedgerIndex { get; internal set; }

        /// <summary>
        /// Gets the time of ledger closed. (in UTC+8)
        /// </summary>
        [JsonProperty("ledger_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LedgerTime { get; internal set; }

        /// <summary>
        /// Gets the public key of the node.
        /// </summary>
        [JsonProperty("pubkey_node")]
        public string PubkeyNode { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("load_base")]
        public int LoadBase { get; internal set; }

        /// <summary>
        /// todo:
        /// </summary>
        [JsonProperty("load_factor")]
        public int LoadFactor { get; internal set; }

        /// <summary>
        /// Gets the retention swt value for each Account.
        /// </summary>
        [JsonProperty("reserve_base")]
        public int ReserveBase { get; internal set; }

        /// <summary>
        /// Gets the freezed swt value for each offer or trust relation.
        /// </summary>
        [JsonProperty("reserve_inc")]
        public int ReserveInc { get; internal set; }

        /// <summary>
        /// Gets the status of the server.
        /// </summary>
        [JsonProperty("server_status")]
        public string Status { get; internal set; }
    }
}
