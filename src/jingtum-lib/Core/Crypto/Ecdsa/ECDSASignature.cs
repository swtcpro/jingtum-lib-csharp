using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace JingTum.Lib.Core.Crypto.Ecdsa
{
    internal class ECDSASignature
    {
        /// <summary>
        /// Creates a new instance of <see cref="ECDSASignature"/> object with the given components.
        /// </summary>
        /// <param name="r">The r component.</param>
        /// <param name="s">The s component.</param>
        public ECDSASignature(BigInteger r, BigInteger s)
        {
            R = r;
            S = s;
        }

        /// <summary>
        /// Gets the R component of the signature.
        /// </summary>
        public BigInteger R { get; private set; }

        /// <summary>
        /// Gets the S component of the signature.
        /// </summary>
        public BigInteger S { get; private set; }
        
        /// <summary>
        /// Encodes the signature to DER.
        /// </summary>
        /// <remarks>
        /// DER is an international standard for serializing data structures which is widely used in cryptography.
        /// It's somewhat like protocol buffers but less convenient. This method returns a standard DER encoding
        /// of the signature, as recognized by OpenSSL and other libraries.
        /// </remarks>
        /// <returns>A byte buffer as a standard DER encoding of the signature.</returns>
        public byte[] EncodeToDER()
        {
            try
            {
                return DerByteStream().ToArray();
            }
            catch (IOException e)
            {
                throw e;  // Cannot happen.
            }
        }

        protected MemoryStream DerByteStream()
        {
            // Usually 70-72 bytes.
            var bos = new MemoryStream(72);
            var seq = new DerSequenceGenerator(bos);
            seq.AddObject(new DerInteger(R));
            seq.AddObject(new DerInteger(S));
            seq.Close();
            return bos;
        }
    }
}
