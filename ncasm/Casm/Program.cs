using Casm.Assembler;
using CommandLine;
using System;
using System.IO;
using System.Linq;
using Casm.Assembler.Behaviors;
using static System.Console;

namespace Casm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var parser = new Parser(settings =>
            {
                settings.CaseInsensitiveEnumValues = true;
            }))
            {
                parser.ParseArguments<AssembleOptions, DisassembleOptions>(args)
                    .WithParsed<AssembleOptions>(RunAssemble)
                    .WithParsed<DisassembleOptions>(RunDisassemble)
                    .WithNotParsed(errors =>
                    {
                        foreach (var error in errors)
                        {
                            WriteLine(error);
                        }
                    });
            }
        }

        private static void RunAssemble(AssembleOptions options)
        {
            string input;

            if (options.InputFile != null)
            {
                if (!File.Exists(options.InputFile))
                {
                    WriteLine("Error: The file specified by the in argument does not exist.");
                    return;
                }

                input = File.ReadAllText(options.InputFile);
            }
            else
            {
                input = options.Input;
            }

            var compiler = new Compiler();
            var tree = compiler.Compile(input);

            var outputStream = options.OutputFile != null ? new FileStream(options.OutputFile, FileMode.Create, FileAccess.Write) : OpenStandardOutput();
            var writer = new StreamWriter(outputStream);

            try
            {
                switch (options.Format)
                {
                    case AssembleFormat.Pretty:
                        foreach (var node in tree.Nodes)
                        {
                            switch (node)
                            {
                                case Label label when options.ShowLabels:
                                    writer.WriteLine($"{label.Position}: Label: {label.Name}");
                                    break;

                                case Directive directive when options.ShowDirectives:
                                    writer.WriteLine($"{directive.Position}: Directive: {directive.Name}");
                                    break;

                                case Instruction instruction:
                                    writer.WriteLine($"{instruction.Position}: Instruction: 0x{instruction.CreateMachineCode():x8}");
                                    break;
                            }
                        }

                        break;

                    case AssembleFormat.Hex:
                        foreach (var node in tree.Nodes)
                        {
                            switch (node)
                            {
                                case Instruction instruction:
                                    writer.Write($"{instruction.CreateMachineCode():x8}");
                                    break;
                            }
                        }

                        break;

                    case AssembleFormat.Binary:
                        foreach (var node in tree.Nodes)
                        {
                            switch (node)
                            {
                                case Instruction instruction:
                                    outputStream.Write(BitConverter.GetBytes(instruction.CreateMachineCode()), 0, 4);
                                    break;
                            }
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            finally
            {
                writer?.Close();
                outputStream?.Close();
            }
        }

        private static void RunDisassemble(DisassembleOptions options)
        {
            if (!File.Exists(options.InputFile))
            {
                WriteLine("Error: The file specified by the in argument does not exist.");
                return;
            }

            var outputStream = options.OutputFile != null ? new FileStream(options.OutputFile, FileMode.Create, FileAccess.Write) : OpenStandardOutput();
            var writer = new StreamWriter(outputStream);

            try
            {
                using (var inputStream = File.Open(options.InputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new BinaryReader(inputStream))
                {
                    while (reader.PeekChar() != -1)
                    {
                        var instruction = reader.ReadUInt32();

                        var functionSelect = (instruction & 0b1110_0000_0000_0000_0000_0000_0000_0000) >> 29;
                        var dBusSelect = (instruction & 0b0001_1100_0000_0000_0000_0000_0000_0000) >> 26;
                        var opcode = (instruction & 0b0000_0011_1111_0000_0000_0000_0000_0000) >> 20;

                        var instructionDefinition = InstructionMap.ReversedInstructionDefinitions[(functionSelect, dBusSelect, opcode)];

                        writer.Write(instructionDefinition.Name);

                        var isFirstOperand = true;
                        foreach (var operand in instructionDefinition.Behaviors.OfType<OperandBehavior>().OrderBy(x => x.OperandPosition))
                        {
                            if (isFirstOperand)
                            {
                                writer.Write(' ');
                                isFirstOperand = false;
                            }
                            else
                            {
                                writer.Write(',');
                            }

                            writer.Write(operand.GetStringRepresentation(instruction));
                        }

                        writer.WriteLine();
                    }
                }
            }
            finally
            {
                writer?.Close();
                outputStream?.Close();
            }
        }
    }
}