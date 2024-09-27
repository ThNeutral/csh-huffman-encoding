using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__huffman_encoding.internals.cli
{
    internal static class AppParams
    {
        
        public static string? sourceLocation;
        public static string? outputLocation;
        public static bool isEncoding;
        public static bool isDebug;
        public static string ToString()
        {
            return $"AppParams {{ sourceLocation: {sourceLocation}, outputLocation: {outputLocation}, isEncoding: {isEncoding}, isDebug: {isDebug} }}";
        }
    }
}
