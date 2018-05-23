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
        /// Gets the raw message received from server.
        /// </summary>
        public string Message { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.Transactions"/> event.
    /// </summary>
    public class TransactionsEventArgs : MessageEventsArgs
    {
        public TransactionResponse Result { get; internal set; }
    }

    /// <summary>
    /// Represents the event args for <see cref="Remote.LedgerClosed"/> event.
    /// </summary>
    public class LedgerClosedEventArgs : MessageEventsArgs
    {
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
