using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the account stub used to subscribe events of account.
    /// </summary>
    public class Account
    {
        private Remote _remote;
        private IDictionary<string, MessageCallback<TxResult>> _accounts = new Dictionary<string, MessageCallback<TxResult>>();

        internal Account(Remote remote)
        {
            _remote = remote;
            _remote.Transactions += _remote_Transactions;
        }

        /// <summary>
        /// Subscrible the event for specific account.
        /// </summary>
        /// <param name="account">The account address.</param>
        /// <param name="callback">The callback delegate.</param>
        public void Subscribe(string account, MessageCallback<TxResult> callback)
        {
            if (!Utils.IsValidAddress(account))
            {
                callback(new MessageResult<TxResult>(null, new Exception("Invalid accound")));
                return;
            }

            _accounts[account] = callback;
        }

        /// <summary>
        /// Unsubscribe the event for specific account.
        /// </summary>
        /// <param name="account">The acount address.</param>
        public void Unsubscribe(string account)
        {
            if (_accounts.ContainsKey(account))
            {
                _accounts.Remove(account);
            }
        }

        private void _remote_Transactions(object sender, TransactionsEventArgs e)
        {
            if (_accounts.Count == 0) return;

            var response = e.Result;
            var accounts = ResponseParser.AffectedAccounts(response.Transaction, response.Meta);

            foreach(var account in accounts)
            {
                if (_accounts.ContainsKey(account))
                {
                    var result = ResponseParser.ProcessTx(response.Transaction, response.Meta, account);
                    _accounts[account].Invoke(new MessageResult<TxResult>(e.Message, null, result));
                }
            }
        }
    }
}
