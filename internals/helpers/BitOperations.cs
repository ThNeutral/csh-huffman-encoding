using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal class BitOperations
    {
        public static List<bool> AddBit(List<bool> oldBits, bool nextBit)
        {
            var newBits = new List<bool>(oldBits);
            newBits.Add(nextBit);
            return newBits;
        }
        public static List<bool> ToBits(byte num)
        {
            var bits = new List<bool>();
            for (int i = 7; i >= 0; i--)
            {
                bits.Add((num & (1u << i)) != 0);
            }
            return bits;
        }
        public static List<bool> ToBits(UInt16 num)
        {
            var bits = new List<bool>();
            for (int i = 15; i>=0; i--)
            {
                bits.Add((num & (1u << i)) != 0); 
            }
            return bits;
        }
        public static List<bool> ToBits(UInt32 num)
        {
            var bits = new List<bool>();
            for (int i = 31; i >= 0; i--)
            {
                bits.Add((num & (1u << i)) != 0);
            }
            return bits;
        }

        public static List<bool> ToBits(char ch)
        {
            return ToBits((UInt16)ch);
        }

        public static UInt16 ToUInt16(List<bool> bits)
        {
            UInt16 num = 0;
            for (UInt16 i = (ushort)(bits.Count - 1u); i >= 0 && i < bits.Count; i--)
            {
                if (bits[i])
                {
                    num |= (ushort)(1u << (15-i));
                }
            }
            return num;
        }
        public static UInt32 ToUInt32(List<bool> bits)
        {
            UInt32 num = 0;
            for (UInt32 i = (UInt32)(bits.Count - 1u); i >= 0 && i < bits.Count; i--)
            {
                if (bits[(int)(i)])
                {
                    num |= (uint)(1u << (31 - (int)i));
                }
            }
            return num;
        }
        public static byte ToUInt8(List<bool> bits)
        {
            byte num = 0;
            for (byte i = (byte)(bits.Count - 1u); i >= 0 && i < bits.Count; i--)
            {
                if (bits[i])
                {
                    num |= (byte)(1u << (7 - i));
                }
            }
            return num;
        }

        public static List<bool> GetNextNBits(List<bool> bits, int n)
        {
            var res = bits.Take(n).ToList();
            bits.RemoveRange(0, n);
            return res;
        }

        public static string BitsToString(List<bool> bits)
        {
            var b = "";
            for (int i = 0; i < bits.Count; i++)
            {
                b += bits[i] ? 1 : 0;
            }
            return b;
        }
    }
}
