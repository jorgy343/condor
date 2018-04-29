using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Casm.Antlr;
using System.Collections.Generic;
using System.Linq;

namespace Casm.Assembler.Base
{
    public class Compiler
    {
        public Tree Compile(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var casmLexer = new CasmLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(casmLexer);
            var casmParser = new CasmParser(commonTokenStream);

            var progContext = casmParser.prog();
            var tree = new Tree();

            var compilerListener = new CompilerListener(tree);

            var walker = new ParseTreeWalker();
            walker.Walk(compilerListener, progContext);

            // Replace immediate operands that have label placeholders with the label's position.
            var labelMap = new Dictionary<string, uint>();

            foreach (var label in tree.Nodes.OfType<Label>())
            {
                labelMap[label.Name] = label.Position;
            }

            foreach (var instruction in tree.Nodes.OfType<Instr>())
            {
                foreach (var immediateOperand in instruction.Operands.OfType<ImmediateOperand>())
                {
                    if (immediateOperand.LabelName != null)
                    {
                        immediateOperand.Value = labelMap[immediateOperand.LabelName];
                    }
                }
            }

            return tree;
        }
    }
}