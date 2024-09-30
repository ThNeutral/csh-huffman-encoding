using c__huffman_encoding.internals.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.encoding
{
    internal static class DecodingHandler
    {
        public static (string str, ErrorCodes error) HuffmanDecode(List<bool> bits)
        {
            try
            {
                var tableSize = BitOperations.ToUInt16(BitOperations.GetNextNBits(bits, 16));
                var table = new Dictionary<string, char>();
                for (int j = 0; j < tableSize; j++)
                {
                    var ch = (char)BitOperations.ToUInt16(BitOperations.GetNextNBits(bits, 16));
                    var size = BitOperations.ToUInt8(BitOperations.GetNextNBits(bits, 8));
                    var encoding = BitOperations.GetNextNBits(bits, size);
                    table[BitOperations.BitsToString(encoding)] = ch;
                }
                Logger.WriteLine(table);
                var dataSize = BitOperations.ToUInt32(BitOperations.GetNextNBits(bits, 32));
                var encodingBits = new List<bool>();
                var res = "";
                for (int j = 0; j < dataSize; j++)
                {
                    encodingBits = encodingBits.Concat(BitOperations.GetNextNBits(bits, 1)).ToList();
                    if (table.ContainsKey(BitOperations.BitsToString(encodingBits)))
                    {
                        res += table[BitOperations.BitsToString(encodingBits)];
                        encodingBits = new List<bool>();
                    }
                }
                return (res, ErrorCodes.NO_ERROR);
            }
            catch  (Exception e)
            {
                Logger.WriteLine(e.ToString());
                return ("", ErrorCodes.FAILED_TO_DECODE_FILE);
            }

            throw new NotImplementedException();
        }
    }
}
