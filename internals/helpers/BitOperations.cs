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
    }
}
