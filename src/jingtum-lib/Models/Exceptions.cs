using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    public class ResponseException : Exception
    {
        public ResponseException(string message) : base(message)
        {

        }

        public string Error { get; internal set; }
        public int ErrorCode { get; internal set; }
        public string Request { get; internal set; }

        public override string ToString()
        {
            return string.Format("({0}: {1}): {2}", Error == null ? "" : Error, ErrorCode.ToString(), Message == null ? "" : Message);
        }

    }

    public class InvalidSecretException : Exception
    {
        public InvalidSecretException(string secret, Exception innerException = null) : base("Invalid secret.", innerException)
        {
            Secret = secret;
        }
        
        public string Secret { get; private set; }

        public override string Message => Secret == null ? "Secret is null." : string.Format("Invalid secret \"{0}\".", Secret);
    }

    public class InvalidAddressException : Exception
    {
        public InvalidAddressException(string address, string name = null, string message=null, Exception innerException = null) 
            : base(message, innerException)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }

        public override string Message => base.Message ?? (Address == null ? "Address is null." : string.Format("Invalid addess \"\".", Address));
    }

    public class InvalidHashException : Exception
    {
        public InvalidHashException(string hash, string name = null, string message = null, Exception innerException = null) 
            : base(message, innerException)
        {
            Hash = hash;
            Name = name;
        }

        public string Name { get; private set; }
        public string Hash { get; private set; }

        public override string Message => base.Message ?? (Hash == null ? "Hash is null." : string.Format("Invalid hash \"\".", Hash));
    }

    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(AmountSettings amount, string name = null, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Amount = amount;
            Name = name;
        }

        public string Name { get; private set; }
        public AmountSettings Amount { get; private set; }

        public override string Message => base.Message ?? (Amount == null ? "Amount is null." : "Invalid amount.");
    }
}
