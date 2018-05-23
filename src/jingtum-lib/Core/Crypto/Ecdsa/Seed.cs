using System;
using Org.BouncyCastle.Math;

namespace JingTum.Lib.Core.Crypto.Ecdsa
{
    internal class Seed
    {
        public const string SIGN_HASH_STRING = "Jingtum2016";
        private readonly byte[] _seedBytes;
        
        /// <summary>
        /// Gets the public key from the sceret key. 
        /// </summary>
        /// <param name="secretKey">Secret point on the curve as BigInteger.</param>
        /// <returns>Corresponding public point.</returns>
        private static byte[] GetPublic(BigInteger secretKey)
        {
            return SECP256K1.BasePointMultipliedBy(secretKey);
        }

        public Seed(byte[] seedBytes)
        {
            _seedBytes = seedBytes;
        }

        public override String ToString()
        {
            return Config.GetB58IdentiferCodecs().EncodeFamilySeed(_seedBytes);
        }
        
        /// <summary>
        /// Computes the public key from the private key.
        /// </summary>
        /// <param name="privateGen">Secret point on the curve as BigInteger.</param>
        /// <returns>
        /// The corresponding public key is the public generator (aka public root key, master public key).
        /// Return as byte[] for convenience.
        /// </returns>
        private static byte[] ComputePublicGenerator(BigInteger privateGen)
        {
            return GetPublic(privateGen);
        }

        private static BigInteger ComputePublicKey(BigInteger secret)
        {
            return Utility.UBigInt(GetPublic(secret));
        }

        private static BigInteger ComputePrivateGen(byte[] seedBytes)
        {
            byte[] privateGenBytes;
            BigInteger privateGen;
            int i = 0;

            while (true)
            {
                privateGenBytes = new Sha512().Add(seedBytes)
                                              .Add32(i++)
                                              .Finish256();
                privateGen = Utility.UBigInt(privateGenBytes);
                if (privateGen.CompareTo(SECP256K1.Order) == -1)
                {
                    break;
                }
            }
            return privateGen;
        }

        private static BigInteger ComputeSecretKey(BigInteger privateGen, byte[] publicGenBytes, int accountNumber)
        {
            BigInteger secret;
            
            int i = 0;
            while (true)
            {
                byte[] secretBytes = new Sha512().Add(publicGenBytes)
                                                 .Add32(accountNumber)
                                                 .Add32(i++)
                                                 .Finish256();
                secret = Utility.UBigInt(secretBytes);
                if (secret.CompareTo(SECP256K1.Order) == -1)
                {
                    break;
                }
            }

            secret = secret.Add(privateGen).Mod(SECP256K1.Order);
            return secret;
        }

        public static String ComputeAddress(String secret)
        {
            byte[] pubBytes = null;
            IKeyPair keyPair = Seed.GetKeyPair(secret);
            pubBytes = HashUtils.SHA256_RIPEMD160(keyPair.Pub.ToByteArray());
            return Config.GetB58IdentiferCodecs().EncodeAddress(pubBytes);
        }

        public static IKeyPair GetKeyPair(byte[] seedBytes)
        {
            return CreateKeyPair(seedBytes, 0);
        }

        public static IKeyPair GetKeyPair(String b58)
        {
            byte[] bytes = null;
            try
            {
                bytes = Config.GetB58IdentiferCodecs().DecodeFamilySeed(b58);
            }
            catch (Exception)
            {
                throw new Exception("Invalid Jingtum account secret!");
            }
            return GetKeyPair(bytes);
        }

        public static IKeyPair CreateKeyPair(byte[] seedBytes, int accountNumber)
        {
            BigInteger secret, pub, privateGen;
            // The private generator (aka root private key, master private key)
            privateGen = ComputePrivateGen(seedBytes);
            byte[] publicGenBytes = ComputePublicGenerator(privateGen);

            if (accountNumber == -1)
            {
                // The root keyPair
                return new KeyPair(privateGen, Utility.UBigInt(publicGenBytes));
            }
            else
            {
                secret = ComputeSecretKey(privateGen, publicGenBytes, accountNumber);
                pub = ComputePublicKey(secret);
                return new KeyPair(secret, pub);
            }
        }

        public static Seed FromPassPhrase(String passPhrase)
        {
            return new Seed(PassPhraseToSeedBytes(passPhrase));
        }

        public static byte[] PassPhraseToSeedBytes(String phrase)
        {
            try
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(phrase);
                return new Sha512(bytes).Finish128();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static String GenerateSecret()
        {
            long timestamp = DateTime.Now.Ticks;
            String seedString = SIGN_HASH_STRING + timestamp;
            return FromPassPhrase(seedString).ToString();
        }
    }
}
