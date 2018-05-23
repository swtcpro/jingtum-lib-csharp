using JingTum.Lib.Core;
using JingTum.Lib.Core.Crypto.Ecdsa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    public class Wallet
    {
        private IKeyPair _keyPair;

        private Wallet(string address, string secret)
        {
            Address = address;
            Secret = secret;
            _keyPair = Seed.GetKeyPair(secret);
        }

        public string Address { get; private set; }
        public string Secret { get; private set; }

        public static Wallet Generate()
        {
            var secret = Seed.GenerateSecret();
            var address = Seed.ComputeAddress(secret);
            return new Wallet(address, secret);
        }

        public static Wallet FromSecret(string secret)
        {
            if (secret == null)
            {
                throw new ArgumentNullException("secret");
            }

            try
            {
                var address = Seed.ComputeAddress(secret);
                return new Wallet(address, secret);
            }
            catch(Exception ex)
            {
                throw new InvalidSecretException(secret, ex);
            }
        }

        internal static bool IsValidAddress(string address)
        {
            return Core.Utility.IsValidAddress(address);
        }

        internal static bool IsvValidSecret(string secret)
        {
            return Core.Utility.IsValidSecret(secret);
        }

        internal string Sign(string message)
        {
            var hash = Hash(message);
            var der =  _keyPair.Sign(hash);
            return Utils.BytesToHex(der);
        }

        internal string Sign(byte[] data)
        {
            var der = _keyPair.Sign(data);
            return Utils.BytesToHex(der);
        }

        private byte[] Hash(string message)
        {
            var sha512 = new Sha512();
            var bytes = Encoding.ASCII.GetBytes(message);
            sha512.Add(bytes);
            return sha512.Finish256();
        }

        public Org.BouncyCastle.Math.BigInteger GetPublicKey()
        {
            return _keyPair?.Pub;
        }
    }
}
