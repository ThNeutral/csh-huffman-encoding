using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal static class BitWriter
    {
        public static ErrorCodes WriteToFile(List<bool> data, string fileName, FileMode fm = FileMode.Create, FileAccess fa = FileAccess.Write)
        {
            try
            {
                var directory = Path.GetDirectoryName(fileName);
                if (directory != null && !Directory.Exists(directory)) 
                {
                    Directory.CreateDirectory(directory);
                }

                using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, fm, fa)))
                {
                    byte currentByte = 0;
                    int index = 0;

                    foreach (bool b in data)
                    {
                        if (b)
                        {
                            currentByte |= (byte)(1 << (7 - index));
                        }

                        index++;

                        if (index == 8)
                        {
                            writer.Write(currentByte);
                            currentByte = 0;
                            index = 0;
                        }
                    }

                    if (index > 0)
                    {
                        writer.Write(currentByte);
                    }
                }

                return ErrorCodes.NO_ERROR;
            }
            catch (Exception e) 
            {
                Logger.WriteLine(e.ToString());
                return ErrorCodes.FAILED_TO_WRITE_TO_FILE;
            }
        }
    }
}
