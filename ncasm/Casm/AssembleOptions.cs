using CommandLine;

namespace Casm
{
    [Verb("assemble")]
    public class AssembleOptions
    {
        [Option('f', "format", Default = AssembleFormat.Pretty)]
        public AssembleFormat Format { get; set; }

        [Option('l', "labels", Default = false)]
        public bool ShowLabels { get; set; }

        [Option('d', "directives", Default = false)]
        public bool ShowDirectives { get; set; }

        [Option('i', "in")]
        public string InputFile { get; set; }

        [Option('o', "out")]
        public string OutputFile { get; set; }

        [Value(0)]
        public string Input { get; set; }
    }
}