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
    }
}
