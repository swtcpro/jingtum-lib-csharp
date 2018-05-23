using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities.Encoders;

namespace JingTum.Lib.Core.Crypto.Ecdsa
{
    internal class SECP256K1
    {
        private static readonly ECDomainParameters _params = CreateParameters();
        
        private static BigInteger FromHex(String hex)
        {
            return new BigInteger(1, Hex.Decode(hex));
        }

        public static BigInteger Order
        {
            get { return _params.N; }
        }

        public static ECPoint BasePoint
        {
            get { return _params.G; }
        }

        public static byte[] BasePointMultipliedBy(BigInteger secret)
        {
            return BasePoint.Multiply(secret).GetEncoded(true);
        }

        private static ECDomainParameters CreateParameters()
        {
            // p = 2^256 - 2^32 - 2^9 - 2^8 - 2^7 - 2^6 - 2^4 - 1
            BigInteger p = FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F");
            BigInteger a = BigInteger.ValueOf(0);
            BigInteger b = BigInteger.ValueOf(7);
            byte[] S = null;
            BigInteger n = FromHex("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141");
            BigInteger h = BigInteger.ValueOf(1);

            ECCurve curve = new FpCurve(p, a, b);
            ECPoint G = curve.DecodePoint(Hex.Decode("04"
                + "79BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798"
                + "483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8"));

            return new ECDomainParameters(curve, G, n, h, S);
        }

        public static ECDomainParameters Params
        {
            get { return _params; }
        }
    }
}
