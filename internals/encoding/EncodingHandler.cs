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
        public static (List<bool>? bits, ErrorCodes error) HuffmanEncode(FileStream source)
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

            return HuffmanEncode(sourceContent);
        }
        public static (List<bool>? bits, ErrorCodes error) HuffmanEncode(string source)
        {
            var (table, tableError) = GetHuffmanTable(source);
            if (tableError != ErrorCodes.NO_ERROR)
            {
                return (null, tableError);
            }

            var binData = EncodeString(source, table);
            Logger.WriteLine(binData);

            return (MergeTableAndBinData(table, binData), ErrorCodes.NO_ERROR);
            
        }

        private static List<bool> MergeTableAndBinData(Dictionary<char, List<bool>> table, List<bool> binData)
        {
            var result = new List<bool>();
            // Number of Huffman table entries (UInt16)
            result = result.Concat(BitOperations.ToBits((UInt16)table.Count)).ToList();
            // Huffman table entries
            foreach (var kvp in table)
            {
                // Character (UInt16)
                result = result.Concat(BitOperations.ToBits(kvp.Key)).ToList();
                // Encoding size (UInt8)
                result = result.Concat(BitOperations.ToBits((byte)kvp.Value.Count)).ToList();
                // Encoding 
                result = result.Concat(kvp.Value).ToList();
            }
            // Encoded data size (UInt32)
            result = result.Concat(BitOperations.ToBits((UInt32)binData.Count)).ToList();
            // Encoded data
            result = result.Concat(binData).ToList();
            Logger.WriteLine(result);
            return result;
        }

        private static List<bool> EncodeString(string source, Dictionary<char, List<bool>> table)
        {
            var binData = new List<bool>();
            foreach (char ch in source)
            {
                var encodingBits = table[ch];
                binData = binData.Concat(encodingBits).ToList();
            }
            return binData;
        }

        private static (Dictionary<char, List<bool>>? table, ErrorCodes errorCode) GetHuffmanTable(string source)
        {
            var occurances = CountCharOccurancesInText(source);
            Logger.WriteLine(occurances);

            var head = GenerateBinaryTree(occurances);
            Logger.WriteLine(head);

            var table = GenerateHuffmanTable(head);
            Logger.WriteLine(table);

            return (table, ErrorCodes.NO_ERROR);
        }

        private static List<KeyValuePair<char, int>> CountCharOccurancesInText(string input) 
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
                    occurances[c] = 1;
                }
            }
            return occurances.OrderBy(kvp => -kvp.Value).ToList();
        }

        private static BinaryTreeNode<char> GenerateBinaryTree(List<KeyValuePair<char, int>> list)
        {
            PriorityQueue<BinaryTreeNode<char>, int> priorityQueue = new();
            foreach (KeyValuePair<char, int> kvp in list)
            {
                priorityQueue.Enqueue(new BinaryTreeNode<char>(kvp.Key), kvp.Value);
            }

            while (priorityQueue.Count != 1)
            {
                int priority1, priority2;
                BinaryTreeNode<char> node1, node2;
                priorityQueue.TryDequeue(out node1, out priority1);
                priorityQueue.TryDequeue(out node2, out priority2);

                BinaryTreeNode<char> newNode = new BinaryTreeNode<char>('\0');
                newNode.left = node1;
                newNode.right = node2;
                priorityQueue.Enqueue(newNode, priority1 + priority2);
            }

            return priorityQueue.Dequeue();
        }
        private static Dictionary<char, List<bool>> GenerateHuffmanTable(BinaryTreeNode<char> head)
        {
            var dict = new Dictionary<char, List<bool>>();
            var bits = new List<bool>(0);
            WalkTree(head, bits, dict);
            return dict;
        }

        private static void WalkTree(BinaryTreeNode<char> node, List<bool> bits, Dictionary<char, List<bool>> dict)
        {
            if (node.IsLeaf())
            {
                dict[node.data] = bits;
                return;
            }
            WalkTree(node.left, BitOperations.AddBit(bits, false), dict);
            WalkTree(node.right, BitOperations.AddBit(bits, true), dict);
        }
    }
}
