﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c__huffman_encoding.internals.helpers;

enum ErrorCodes
{
    NO_ERROR,
    NO_ERROR_PRINT_HELP,
    TOO_FEW_ARGUMENTS,
    UNKNOWN_MODE,
    MALFORMED_OUTPUT_FLAG,
    FLAG_IN_INCORRECT_POSITION,
    SOURCE_FILE_DOES_NOT_EXIST,
    EMPTY_FILE,
    FAILED_TO_READ_FROM_FILE,
    FAILED_TO_WRITE_TO_FILE,
    FAILED_TO_DECODE_FILE,
    UNSUPPORTED_FILE_EXTENSION
}

namespace c__huffman_encoding.internals.cli
{
    static internal class CLI
    {
        public static ErrorCodes ProcessCommandArguments(string[] args)
        {
            if (args.Length == 1 && args[0] == "help") 
            {
                PrintHelp();
                return ErrorCodes.NO_ERROR_PRINT_HELP;
            }
            if (args.Length < 2)
            {
                return ErrorCodes.TOO_FEW_ARGUMENTS;
            }
            if (args[0] != "encode" && args[0] != "decode")
            {
                return ErrorCodes.UNKNOWN_MODE;
            }
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith('-') && (i == 0 || i == 1))
                {
                    return ErrorCodes.FLAG_IN_INCORRECT_POSITION;
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
                                        return ErrorCodes.MALFORMED_OUTPUT_FLAG;
                                    }
                                i += 1;
                                AppParams.outputLocation = args[i];
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

            return ProcessOutputLocation();
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
                case ErrorCodes.FAILED_TO_WRITE_TO_FILE:
                    {
                        Console.WriteLine("Failed to write data into file");
                        break;
                    }
                case ErrorCodes.FAILED_TO_READ_FROM_FILE:
                    {
                        Console.WriteLine("Failed to read data from file");
                        break;
                    }
                case ErrorCodes.UNSUPPORTED_FILE_EXTENSION:
                    {
                        Console.WriteLine("Unsupported source file extension. Can be either .txt for encoding or .huffman for decoding");
                        break;
                    }
                case ErrorCodes.FAILED_TO_DECODE_FILE: 
                    {
                        Console.WriteLine("Failed to decode file. Probably source file is malformed");
                        break;
                    }
            }
        }

        private static void PrintHelp()
        {
            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"Usage format: {appName} <encode|decode> <text file location> <...flags>\nFlags:\n\t-o <location>\tspecifies output file location. By default output is placed in the ./output folder");
        }

        private static ErrorCodes ProcessOutputLocation()
        {
            if ((Path.GetExtension(AppParams.sourceLocation).Equals(".txt") && AppParams.isEncoding) || (Path.GetExtension(AppParams.sourceLocation).Equals(".huffman") && !AppParams.isEncoding))
            {
                var sourceFileNameWOExtension = Path.GetFileName(AppParams.sourceLocation).Split(".")[0];
                var extension = AppParams.isEncoding ? ".huffman" : ".txt";
                if (AppParams.outputLocation == null)
                {
                    AppParams.outputLocation = ".\\output\\" + sourceFileNameWOExtension.Split(".")[0] + extension;
                    return ErrorCodes.NO_ERROR;
                }
                if (Path.GetFileName(AppParams.outputLocation).Equals(string.Empty))
                {
                    AppParams.outputLocation += sourceFileNameWOExtension + extension;
                    return ErrorCodes.NO_ERROR;
                }
                Logger.WriteLine(AppParams.outputLocation);
                return ErrorCodes.NO_ERROR;
            } 
            else
            {
                return ErrorCodes.UNSUPPORTED_FILE_EXTENSION;
            }

        }
    }
}
