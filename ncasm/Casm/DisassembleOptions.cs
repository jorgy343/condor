using CommandLine;

namespace Casm
{
    [Verb("disassemble")]
    public class DisassembleOptions
    {
        [Option('i', "in", Required = true)]
        public string InputFile { get; set; }

        [Option('o', "out")]
        public string OutputFile { get; set; }
    }
}