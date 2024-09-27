using c__huffman_encoding.internals.cli;
using c__huffman_encoding.internals.helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.encoding
{
    internal static class EncodingHandler
    {
        public static (Dictionary<char, BitArray>? table, ErrorCodes errorCode) GetHuffmanTable(FileStream source)
        {
            if (source.Length == 0)
            {
                return (null, ErrorCodes.EMPTY_FILE);
            }

            string sourceContent;
            using (var reader = new StreamReader(source))
            {
                sourceContent = reader.ReadToEnd();
            }

            var occurances = CountCharOccurancesInText(sourceContent);
            Helpers.WriteLine(occurances);

            throw new NotImplementedException();
        }

        private static Dictionary<char, int> CountCharOccurancesInText(string input) 
        {
            var occurances = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (occurances.ContainsKey(c))
                {
                    occurances[c] += 1;
                } 
                else
                {
                    occurances[c] = 0;
                }
            }
            return occurances.OrderBy(kvp => -kvp.Value).ToDictionary();
        }
    }
}
