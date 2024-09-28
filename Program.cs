// See https://aka.ms/new-console-template for more information

using System.Text;
using c__huffman_encoding.internals.cli;
using c__huffman_encoding.internals.encoding;
using c__huffman_encoding.internals.helpers;

Console.OutputEncoding = Encoding.UTF8;

var argsProcessingError = CLI.ProcessCommandArguments(args);
if (argsProcessingError != ErrorCodes.NO_ERROR)
{
    CLI.PrintError(argsProcessingError);
    return; 
}

Helpers.WriteLine(AppParams.ToString());

FileStream source;
try
{
    Console.WriteLine("Trying to open source file");
    source = File.Open(AppParams.sourceLocation!, FileMode.Open);
}
catch
{
    CLI.PrintError(ErrorCodes.SOURCE_FILE_DOES_NOT_EXIST);
    return;
}

var (table, huffmanTableProcessingError) = EncodingHandler.GetHuffmanTable(source);
if (huffmanTableProcessingError != ErrorCodes.NO_ERROR) 
{
    CLI.PrintError(huffmanTableProcessingError);
    return; 
}