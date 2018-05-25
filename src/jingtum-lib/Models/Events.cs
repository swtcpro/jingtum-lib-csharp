using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the event args for event with raw message.
    /// </summary>
    public class MessageEventsArgs : EventArgs
    {
        /// <summary>
        /// Gets the raw json message received from server.
        /// </summary>
        public string Message { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.Transactions"/> event.
    /// </summary>
    public class TransactionsEventArgs : MessageEventsArgs
    {
        /// <summary>
        /// Gets the parsed response result of the transaction.
        /// </summary>
        public TransactionResponse Result { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.LedgerClosed"/> event.
    /// </summary>
    public class LedgerClosedEventArgs : MessageEventsArgs
    {
        /// <summary>
        /// Gets the info of the closed ledger.
        /// </summary>
        public LedgerClosedInfo LedgerClosed { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.ServerStatusChanged"/> event.
    /// </summary>
    public class ServerStatusEventArgs : MessageEventsArgs
    {
        /// <summary>
        /// Gets the server status.
        /// </summary>
        public ServerStatus Status { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.PathFind"/> event.
    /// </summary>
    public class PathFindEventArgs : MessageEventsArgs
    {
    }
}
