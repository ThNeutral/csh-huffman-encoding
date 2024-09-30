// See https://aka.ms/new-console-template for more information

using System.Text;
using c__huffman_encoding.internals.cli;
using c__huffman_encoding.internals.encoding;
using c__huffman_encoding.internals.helpers;

var argsProcessingError = CLI.ProcessCommandArguments(args);
if (argsProcessingError != ErrorCodes.NO_ERROR)
{
    CLI.PrintError(argsProcessingError);
    return; 
}

Logger.WriteLine(AppParams.ToString());

FileStream source;
try
{
    source = File.Open(AppParams.sourceLocation!, FileMode.Open);
}
catch
{
    CLI.PrintError(ErrorCodes.SOURCE_FILE_DOES_NOT_EXIST);
    return;
}

if (AppParams.isEncoding)
{
    var (bin, encodingError) = EncodingHandler.HuffmanEncode(source);
    if (encodingError != ErrorCodes.NO_ERROR)
    {
        CLI.PrintError(encodingError);
        return;
    }

    var writingError = BitWriter.WriteToFile(bin, AppParams.outputLocation);
    if (writingError != ErrorCodes.NO_ERROR)
    {
        CLI.PrintError(writingError);
        return;
    }
}
else
{
    var (bits, readingError) = BitReader.ReadFromFile(source);
    if (readingError != ErrorCodes.NO_ERROR)
    {
        CLI.PrintError(readingError);
        return;
    }
    var (str, decodingError) = DecodingHandler.HuffmanDecode(bits);
    if (decodingError != ErrorCodes.NO_ERROR)
    {
        CLI.PrintError(decodingError);
        return;
    }
    Logger.WriteLine(str);
    var outputFile = File.Open(AppParams.outputLocation, FileMode.Create);
    using (StreamWriter outputStreamWriter = new StreamWriter(outputFile))
    {
        outputStreamWriter.Write(str);
    }
}
