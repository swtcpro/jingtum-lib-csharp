using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// The delegate for <see cref="Request{T}"/> and <see cref="Transaction{T}"/> callback.
    /// </summary>
    /// <param name="result">Result of the <see cref="Request{T}"/> and <see cref="Transaction{T}"/> submit.</param>
    public delegate void MessageCallback<T>( MessageResult<T> result);

    /// <summary>
    /// Represents the callback result of the <see cref="Request{T}"/> and <see cref="Transaction{T}"/> submit.
    /// </summary>
    public class MessageResult<T>
    {
        /// <summary>
        /// Creates new instances of <see cref="MessageResult{T}"/> object.
        /// </summary>
        /// <param name="message">The json message string.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="result">The parsed result.</param>
        public MessageResult(string message, Exception exception = null, T result = default(T))
        {
            Message = message;
            Exception = exception;
            Result = result;
        }

        /// <summary>
        /// Gets the raw message in json which is received from server.
        /// </summary>
        public string Message { get; internal set; }
        /// <summary>
        /// Gets the <see cref="Exception"/> object if has error.
        /// </summary>
        public Exception Exception { get; internal set; }
        /// <summary>
        /// Gets the parsed result.
        /// </summary>
        public T Result { get; internal set; }
    }
}
