using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal static class BitReader
    {
        public static (List<bool>? bits, ErrorCodes error) ReadFromFile(FileStream source)
        {
            var bits = new List<bool>();
            try
            {
                using (BinaryReader reader = new BinaryReader(source))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var nextByte = reader.ReadByte();
                        for (int i = 7; i >= 0; i--)
                        {
                            bits.Add((nextByte & (1 << i)) != 0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
                return (null, ErrorCodes.FAILED_TO_READ_FROM_FILE);
            }

            return (bits, ErrorCodes.NO_ERROR);
        }
    }
}
