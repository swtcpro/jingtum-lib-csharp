using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    /// <summary>
    /// Represents the exception for the request.
    /// </summary>
    public class ResponseException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ResponseException"/> object.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ResponseException(string message) : base(message)
        {

        }

        /// <summary>
        /// Gets the error string.
        /// </summary>
        public string Error { get; internal set; }
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; internal set; }
        /// <summary>
        /// Gets the request json data.
        /// </summary>
        public string Request { get; internal set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("({0}: {1}): {2}", Error == null ? "" : Error, ErrorCode.ToString(), Message == null ? "" : Message);
        }

    }

    /// <summary>
    /// Represents the exception for invalid secret.
    /// </summary>
    public class InvalidSecretException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="InvalidSecretException"/> object.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidSecretException(string secret, Exception innerException = null) : base("Invalid secret.", innerException)
        {
            Secret = secret;
        }

        /// <summary>
        /// Gets the secret string.
        /// </summary>
        public string Secret { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => Secret == null ? "Secret is null." : string.Format("Invalid secret \"{0}\".", Secret);
    }

    /// <summary>
    /// Represents the exception for invalid address.
    /// </summary>
    public class InvalidAddressException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="InvalidAddressException"/> object.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="name">The name for the option.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidAddressException(string address, string name = null, string message=null, Exception innerException = null) 
            : base(message, innerException)
        {
            Name = name;
            Address = address;
        }

        /// <summary>
        /// Gets the options name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the address string.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => base.Message ?? (Address == null ? "Address is null." : string.Format("Invalid addess \"\".", Address));
    }

    /// <summary>
    /// Represents the exception for invalid hash.
    /// </summary>
    public class InvalidHashException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="InvalidHashException"/> object.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="name">The name of the option.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidHashException(string hash, string name = null, string message = null, Exception innerException = null) 
            : base(message, innerException)
        {
            Hash = hash;
            Name = name;
        }

        /// <summary>
        /// Gets the option name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the hash string.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => base.Message ?? (Hash == null ? "Hash is null." : string.Format("Invalid hash \"\".", Hash));
    }

    /// <summary>
    /// Represents the exception for invalid amount.
    /// </summary>
    public class InvalidAmountException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="InvalidAmountException"/> object.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="name">The name of the option.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidAmountException(Amount amount, string name = null, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Amount = amount;
            Name = name;
        }

        /// <summary>
        /// Gets the option name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public Amount Amount { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => base.Message ?? (Amount == null ? "Amount is null." : "Invalid amount.");
    }
}
