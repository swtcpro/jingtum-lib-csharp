using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Utilities.Encoders;

namespace JingTum.Lib.Core
{
    internal class B58IdentiferCodecs
    {
        public const int VER_ACCOUNT_ID        = 0;
        public const int VER_FAMILY_SEED       = 33;
        
        private B58 _b58;

        public B58IdentiferCodecs(B58 base58encoder)
        {
            _b58 = base58encoder;
        }

        public byte[] Decode(String d, int version)
        {
            return _b58.DecodeChecked(d, version);
        }

        public String Encode(byte[] d, int version)
        {
            return _b58.EncodeToStringChecked(d, version);
        }

        public byte[] DecodeFamilySeed(String master_seed)
        {
            byte[] bytes = _b58.DecodeChecked(master_seed, VER_FAMILY_SEED);
            return bytes;
        }

        public String EncodeAddress(byte[] bytes)
        {
            return Encode(bytes, VER_ACCOUNT_ID);
        }

        public byte[] DecodeAddress(String address)
        {
            return Decode(address, VER_ACCOUNT_ID);
        }

        public String EncodeFamilySeed(byte[] bytes)
        {
            return Encode(bytes, VER_FAMILY_SEED);
        }
    }

    internal class B58
    {
        private int[] _indexes;
        private char[] _alphabet;

        public B58(String alphabet)
        {
            SetAlphabet(alphabet);
            BuildIndexes();
        }

        private void SetAlphabet(String alphabet)
        {
            _alphabet = alphabet.ToArray();
        }

        private void BuildIndexes()
        {
            _indexes = new int[128];

            for (int i = 0; i < _indexes.Length; i++)
            {
                _indexes[i] = -1;
            }
            for (int i = 0; i < _alphabet.Length; i++)
            {
                _indexes[_alphabet[i]] = i;
            }
        }

        #region encode/decode by byte

        public String EncodeToStringChecked(byte[] input, int version)
        {
            try
            {
                var bytes = EncodeToBytesChecked(input, version);
                return Encoding.ASCII.GetString(bytes);
            }
            catch (ArgumentException e)
            {
                throw e;  // Cannot happen.
            }
        }

        public byte[] EncodeToBytesChecked(byte[] input, int version)
        {
            byte[] buffer = new byte[input.Length + 1];
            buffer[0] = (byte)version;
            Array.Copy(input, 0, buffer, 1, input.Length);
            byte[] checkSum = Utility.CopyRange(HashUtils.DoubleHash(buffer), 0, 4);
            byte[] output = new byte[buffer.Length + checkSum.Length];
            Array.Copy(buffer, 0, output, 0, buffer.Length);
            Array.Copy(checkSum, 0, output, buffer.Length, checkSum.Length);
            return EncodeToBytes(output);
        }

        /// <summary>
        /// Encodes the given bytes in base58. No checksum is appended.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] EncodeToBytes(byte[] input)
        {
            if (input.Length == 0)
            {
                return new byte[0];
            }
            input = Utility.CopyRange(input, 0, input.Length);
            // Count leading zeroes.
            int zeroCount = 0;
            while (zeroCount < input.Length && input[zeroCount] == 0)
            {
                ++zeroCount;
            }
            // The actual encoding.
            byte[] temp = new byte[input.Length * 2];
            int j = temp.Length;

            int startAt = zeroCount;
            while (startAt < input.Length)
            {
                byte mod = Divmod58(input, startAt);
                if (input[startAt] == 0)
                {
                    ++startAt;
                }
                temp[--j] = (byte)_alphabet[mod];
            }

            // Strip extra '1' if there are some after decoding.
            while (j < temp.Length && temp[j] == _alphabet[0])
            {
                ++j;
            }
            // Add as many leading '1' as there were leading zeros.
            while (--zeroCount >= 0)
            {
                temp[--j] = (byte)_alphabet[0];
            }

            byte[] output;
            output = Utility.CopyRange(temp, j, temp.Length);
            return output;
        }

        public byte[] Decode(string input)
        {
            if (input.Length == 0)
            {
                return new byte[0];
            }

            byte[] input58 = new byte[input.Length];
            // Transform the String to a base58 byte sequence
            for (int i = 0; i < input.Length; ++i)
            {
                char c = input[i];

                int digit58 = -1;
                if (c >= 0 && c < 128)
                {
                    digit58 = _indexes[c];
                }

                if (digit58 < 0)
                {   
                    throw new Exception("Illegal character " + c + " at " + i);
                }
                input58[i] = (byte)digit58;
            }

            // Count leading zeroes
            int zeroCount = 0;
            while (zeroCount < input58.Length && input58[zeroCount] == 0)
            {
                ++zeroCount;
            }
            // The encoding
            byte[] temp = new byte[input.Length];
            int j = temp.Length;

            int startAt = zeroCount;
            while (startAt < input58.Length)
            {
                byte mod = Divmod256(input58, startAt);

                if (input58[startAt] == 0)
                {
                    ++startAt;
                }

                temp[--j] = mod;
            }
            // Do no add extra leading zeroes, move j to first non null byte.
            while (j < temp.Length && temp[j] == 0)
            {
                ++j;
            }

            return Utility.CopyRange(temp, j - zeroCount, temp.Length);
        }

        public byte[] DecodeChecked(string input, int version)
        {
            byte[] buffer = Decode(input);

            if (buffer.Length < 4)
            {
                throw new Exception("Input too short");
            }

            byte actualVersion = buffer[0];
            if (actualVersion != version)
            {
                throw new Exception("Bro, version is wrong yo");
            }

            byte[] toHash = Utility.CopyRange(buffer, 0, buffer.Length - 4);
            byte[] hashed = Utility.CopyRange(HashUtils.DoubleHash(toHash), 0, 4);
            byte[] checksum = Utility.CopyRange(buffer, buffer.Length - 4, buffer.Length);

            //if (!hashed.Equals(checksum))
            if(!Utility.IfByteArrayEquals(hashed, checksum))
            {
                throw new Exception("Checksum does not validate");
            }

            return Utility.CopyRange(buffer, 1, buffer.Length - 4);
        }

        //
        // number -> number / 58, returns number % 58
        //
        private byte Divmod58(byte[] number, int startAt)
        {
            int remainder = 0;
            for (int i = startAt; i < number.Length; i++)
            {
                int digit256 = (int)number[i] & 0xFF;
                int temp = remainder * 256 + digit256;

                number[i] = (byte)(temp / 58);

                remainder = temp % 58;
            }

            return (byte)remainder;
        }


        //
        // number -> number / 256, returns number % 256
        //
        private byte Divmod256(byte[] number58, int startAt)
        {
            int remainder = 0;
            for (int i = startAt; i < number58.Length; i++)
            {
                int digit58 = (int)number58[i] & 0xFF;
                int temp = remainder * 58 + digit58;

                //Console.WriteLine("i:" + i + "   digit58:" + digit58);

                number58[i] = (byte)(temp / 256);

                remainder = temp % 256;
            }

            //-128~127
            //remainder = (remainder > 127) ? (remainder - 256) : remainder;
            return (byte)remainder;
        }
        #endregion
    }

    internal class B16
    {
        public static String ToStringTrimmed(byte[] bytes)
        {
            int offset = 0;
            if (bytes[0] == 0)
            {
                offset = 1;
            }
            return Hex.ToHexString(bytes, offset, bytes.Length - offset).ToUpper();
        }
    }

    internal class Sha512
    {
        private SHA512CryptoServiceProvider _csp = new SHA512CryptoServiceProvider();
        private byte[] _bytes = new byte[0];

        public Sha512()
        {
        }

        public Sha512(byte[] start)
        {
            Add(start);
        }

        public Sha512 Add(byte[] bytes)
        {
            _bytes = _bytes.Concat(bytes).ToArray();
            return this;
        }

        public Sha512 Add32(int i)
        {
            var values = new byte[4];
            values[0] = (byte)((i >> 24) & 0xFF);
            values[1] = (byte)((i >> 16) & 0xFF);
            values[2] = (byte)((i >> 8) & 0xFF);
            values[3] = (byte)((i) & 0xFF);
            return Add(values);
        }

        private byte[] FinishTaking(int size)
        {
            byte[] hash = new byte[size];
            Array.Copy(_csp.ComputeHash(_bytes), 0, hash, 0, size);
            return hash;
        }

        public byte[] Finish128()
        {
            return FinishTaking(16);
        }

        public byte[] Finish256()
        {
            return FinishTaking(32);
        }

        public byte[] Finish()
        {
            return _csp.ComputeHash(_bytes);
        }
    }
}
