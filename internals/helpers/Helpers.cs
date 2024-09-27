using c__huffman_encoding.internals.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal static class Helpers
    {
        public static void WriteLine<TKey, TValue>(Dictionary<TKey, TValue> dict) where TKey : notnull
        {
            if (AppParams.isDebug)
            {
                foreach (KeyValuePair<TKey, TValue> kvp in dict)
                {
                    Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value}");
                }
            }
        }

        public static void WriteLine(string str)
        {
            if (AppParams.isDebug)
            {
                Console.WriteLine(str);
            }
        }
    }
}
