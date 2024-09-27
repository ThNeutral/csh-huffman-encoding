using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum ErrorCodes
{
    TOO_FEW_ARGUMENTS,
    UNKNOWN_MODE,
    MALFORMED_OUTPUT_FLAG,
    FLAG_IN_INCORRECT_POSITION,
    SOURCE_FILE_DOES_NOT_EXIST,
    EMPTY_FILE
}

namespace c__huffman_encoding.internals.cli
{
    static internal class CLI
    {
        public static bool ProcessCommandArguments(string[] args)
        {
            if (args.Length == 1 && args[0] == "help") 
            {
                PrintHelp();
                return true;
            }
            if (args.Length < 2)
            {
                PrintError(ErrorCodes.TOO_FEW_ARGUMENTS);
                return true;
            }
            if (args[0] != "encode" && args[0] != "decode")
            {
                PrintError(ErrorCodes.UNKNOWN_MODE);
                return true;
            }
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith('-') && (i == 0 || i == 1))
                {
                    PrintError(ErrorCodes.FLAG_IN_INCORRECT_POSITION);
                    return true;
                }
                if (i == 0)
                {
                    AppParams.isEncoding = arg == "encode";
                    continue;
                }
                if (i == 1)
                {
                    AppParams.sourceLocation = arg;
                    continue;
                }
                if (arg.StartsWith('-'))
                {
                    switch (arg)
                    {
                        case "-o":
                            {
                                if (i + 1 >= args.Length)
                                    {
                                        PrintError(ErrorCodes.MALFORMED_OUTPUT_FLAG);
                                        return true;
                                    }
                                AppParams.outputLocation = args[i+1];
                                break;
                            }
                        case "--debug":
                            {
                                AppParams.isDebug = true; 
                                break;
                            }
                    }
                }
            }

            return false;
        }

        public static void PrintError(ErrorCodes errorCodes)
        {
            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            switch (errorCodes)
            {
                case ErrorCodes.TOO_FEW_ARGUMENTS:
                    {
                        Console.WriteLine($"Too few arguments.\nCorrect usage is: {appName} <encode|decode> <text file location> <...flags>");
                        break;
                    }
                case ErrorCodes.UNKNOWN_MODE:
                    {
                        Console.WriteLine($"Expected first argument to be either <encode> or <decode>.\nCorrect usage is: {appName} <encode|decode> <text file location> <...flags>");
                        break;
                    }
                case ErrorCodes.MALFORMED_OUTPUT_FLAG:
                    {
                        Console.WriteLine("Expected output location after -o flag, encountered nothing.");
                        break;
                    }
                case ErrorCodes.FLAG_IN_INCORRECT_POSITION:
                    {
                        Console.WriteLine($"Encountered flag in position of primary arguments.\nCorrect usage is: {appName} <encode|decode> <text file location> <...flags>");
                        break;
                    }
                case ErrorCodes.SOURCE_FILE_DOES_NOT_EXIST:
                    {
                        Console.WriteLine("Could not find source file");
                        break;
                    }
                case ErrorCodes.EMPTY_FILE:
                    {
                        Console.WriteLine("Source file is empty");
                        break;
                    }
            }
        }

        private static void PrintHelp()
        {
            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"Usage format: {appName} <encode|decode> <text file location> <...flags>\nFlags:\n\t-o <location>\tspecifies output file location. By default output is placed file to the same folder as source file");
        }
    }
}
