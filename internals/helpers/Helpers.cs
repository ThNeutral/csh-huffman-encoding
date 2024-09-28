using c__huffman_encoding.internals.cli;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal static class Helpers
    {
        public static void WriteLine<TKey, TValue>(List<KeyValuePair<TKey, TValue>> list) where TKey : notnull
        {
            if (AppParams.isDebug)
            {
                foreach (KeyValuePair<TKey, TValue> kvp in list)
                {
                    Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value}");
                }
            }
        }

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
        public static void WriteLine(Dictionary<char, BitArray> dict)
        {
            if (AppParams.isDebug)
            {
                foreach (KeyValuePair<char, BitArray> kvp in dict)
                {
                    var bits = "";
                    for (int i = 0; i < kvp.Value.Length; i++)
                    {
                        bits += kvp.Value[i] ? 1 : 0;
                    }
                    Console.WriteLine($"Key = {kvp.Key}, Value = {bits}");
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
        public static void WriteLine(int i)
        {
            if (AppParams.isDebug)
            {
                Console.WriteLine(i);
            }
        }

        public static void WriteLine(bool b)
        {
            if (AppParams.isDebug)
            {
                Console.WriteLine(b);
            }
        }

        public static void WriteLine<TData>(BinaryTreeNode<TData> node)
        {
            if (AppParams.isDebug)
            {
                BinaryTreeNode<TData>.PrintTree(node);
            }
        }

        public static BitArray AddBit(BitArray oldBits, bool nextBit)
        {
            BitArray newBits = new BitArray(oldBits.Length+1);

            for (int i = 0; i < oldBits.Length; i++)
            {
                newBits[i] = oldBits[i];
            }

            newBits[newBits.Length-1] = nextBit;
            return newBits;
        }
    }
}
