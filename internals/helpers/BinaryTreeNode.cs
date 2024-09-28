using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.helpers
{
    internal class BinaryTreeNode<TData>
    {
        public TData data;
        public BinaryTreeNode<TData> right;
        public BinaryTreeNode<TData> left;
        public BinaryTreeNode(TData data) 
        {
            this.data = data;
        }

        public bool IsLeaf()
        {
            return right == null && left == null;
        }
        public static void PrintTree(BinaryTreeNode<TData> root, string indent = "", bool isRight = true)
        {
            if (root == null)
            {
                return;
            }

            if (root.right != null)
            {
                PrintTree(root.right, indent + (isRight ? "        " : " |      "), true);
            }

            Console.WriteLine(indent + (isRight ? " /---- " : " \\---- ") + root.data);

            if (root.left != null)
            {
                PrintTree(root.left, indent + (isRight ? " |      " : "        "), false);
            }
        }
    }
}
