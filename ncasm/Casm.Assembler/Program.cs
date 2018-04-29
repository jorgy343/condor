using System.Collections.Generic;
using Casm.Assembler.Base;
using System.Linq;
using static System.Console;

namespace Casm.Assembler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string input = @"
                movl 117,r3
                mov r3,r5
                test: j test
                ";

            var compiler = new Compiler();
            var tree = compiler.Compile(input);

            // Display the nodes.
            foreach (var node in tree.Nodes)
            {
                switch (node)
                {
                    case Label label:
                        WriteLine($"{label.Position}: Label: {label.Name}");
                        break;

                    case Instr instruction:
                        WriteLine($"{instruction.Position}: Instruction: 0x{instruction.CreateMachineCode():x8}");
                        break;
                }
            }
        }
    }
}