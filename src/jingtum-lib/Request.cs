using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Requests server and account info without secret.
    /// </summary>
    /// <typeparam name="T">The type of the parsed response message.</typeparam>
    /// <remarks>
    /// Request is used to get server, account, orderbook and path info.
    /// Request is not secret required, and will be public to every one.
    /// All requests are asynchronized and should provide a callback.
    /// </remarks>
    public class Request<T>
    {
        private Remote _remote;
        private string _command;
        private Func<object, T> _filter;
        private Type _responseDataType;
        private dynamic _message = new ExpandoObject();

        internal Request(Remote remote, string command = null, Func<object, T> filter = null, Type responseDataType = null)
        {
            _remote = remote;
            _command = command;
            _filter = filter;
            _responseDataType = responseDataType;
        }

        internal string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        internal Request<T> SetFilter(Func<object, T> filter)
        {
            _filter = filter;
            return this;
        }

        internal dynamic Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Callback entry for request.
        /// </summary>
        /// <param name="callback">The callback for the request result.</param>
        public async void Submit(MessageCallback<T> callback = null)
        {
            await SubmitAsync(callback);
        }

        /// <summary>
        /// The async version of <see cref="Submit(MessageCallback{T})"/>.
        /// </summary>
        /// <param name="callback">The callback for the request result.</param>
        /// <param name="timeout">The millisends to wait for the result. Default is 60000.</param>
        /// <returns>The task.</returns>
        public async Task<MessageResult<T>> SubmitAsync(MessageCallback<T> callback = null, int timeout = 60000)
        {
            MessageResult<T> result = null;
            MessageCallback<T> callbackWrapper = r =>
            {
                result = r;
                callback?.Invoke(r);
            };

            foreach (KeyValuePair<string, object> pair in _message)
            {
                if (pair.Value is Exception exception)
                {
                    callbackWrapper(new MessageResult<T>(null, exception));
                    return result;
                }
            }

            var resetEvent = new AutoResetEvent(false);
            var task = new Task(() =>
            {
                if (!resetEvent.WaitOne(timeout))
                {
                    callbackWrapper(new MessageResult<T>(null, new TimeoutException()));
                }
                resetEvent.Dispose();
            });
            task.Start();

            _remote.Submit(_command, _message, _filter, callbackWrapper, resetEvent);
            await task;
            return result;
        }

        /// <summary>
        /// Selects ledger for current request.
        /// </summary>
        /// <param name="state">The ledger state.</param>
        public void SelectLedger(LedgerState state)
        {
            _message.ledger_index = state.ToString().ToLower();
        }

        /// <summary>
        /// Selects ledger for current request.
        /// </summary>
        /// <param name="index">The ledger index.</param>
        public void SelectLedger(UInt32 index)
        {
            _message.ledger_index = index;
        }

        /// <summary>
        /// Selects ledger for current request.
        /// </summary>
        /// <param name="hash">The ledger hash.</param>
        public void SelectLedger(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return;

            var regex = new Regex("^[A-F0-9]+$");
            if (!regex.IsMatch(hash))
            {
                _message.ledger_hash = new Exception("Invalid ledger hash.");
                return;
            }

            _message.ledger_hash = hash;
        }
    }
}
