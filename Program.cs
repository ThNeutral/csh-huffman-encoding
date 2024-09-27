// See https://aka.ms/new-console-template for more information

using System.Text;
using c__huffman_encoding.internals.cli;
using c__huffman_encoding.internals.encoding;
using c__huffman_encoding.internals.helpers;

Console.OutputEncoding = Encoding.UTF8;

var isArgsProcessingError = CLI.ProcessCommandArguments(args);
if (isArgsProcessingError) { return; }

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

var (table, error) = EncodingHandler.GetHuffmanTable(source);
if (table == null) 
{
    CLI.PrintError(error);
    return; 
}