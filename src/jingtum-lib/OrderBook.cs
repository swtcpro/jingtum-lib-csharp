using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the order book stub for order book events.
    /// </summary>
    public class OrderBook
    {
        private Remote _remote;
        private IDictionary<string, MessageCallback<TxResult>> _books = new Dictionary<string, MessageCallback<TxResult>>();
        internal OrderBook(Remote remote)
        {
            _remote = remote;
            _remote.Transactions += _remote_Transactions;
        }

        /// <summary>
        /// Registers the listener for given order book pair.
        /// </summary>
        /// <param name="gets">The amount of gets.</param>
        /// <param name="pays">The amount of pays.</param>
        /// <param name="callback">The callback.</param>
        public void RegisterListener(Amount gets, Amount pays, MessageCallback<TxResult> callback)
        {
            var key = ResponseParser.GetAmoutPairKey(gets, pays);
            _books[key] = callback;
        }

        /// <summary>
        /// Unregisters the listener for given order book pair.
        /// </summary>
        /// <param name="gets">The gets.</param>
        /// <param name="pays">The pays.</param>
        public void UnregisterListener(Amount gets, Amount pays)
        {
            var key = ResponseParser.GetAmoutPairKey(gets, pays);
            if (_books.ContainsKey(key))
            {
                _books.Remove(key);
            }
        }

        private void _remote_Transactions(object sender, TransactionsEventArgs e)
        {
            if (_books.Count == 0) return;

            var response = e.Result;
            var books = ResponseParser.AffectBooks(response.Transaction, response.Meta);
            if (books == null) return;

            var result = new MessageResult<TxResult>(e.Message, null, response.TxResult);
            foreach(var book in books)
            {
                if (_books.ContainsKey(book))
                {
                    _books[book](result);
                }
            }
        }
    }
}
