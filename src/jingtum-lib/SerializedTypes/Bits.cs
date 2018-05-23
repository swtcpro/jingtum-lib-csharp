using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingTum.Lib
{
    internal class Bits
    {
        public static bool IsLittleEndian => BitConverter.IsLittleEndian;

        public static byte[] GetBytes(bool value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(byte value)
        {
            return new[] { value };
        }

        public static byte[] GetBytes(char value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }

        public static byte[] GetBytes(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (IsLittleEndian) bytes = bytes.Reverse().ToArray();
            return bytes;
        }
    }
}
