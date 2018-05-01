using Antlr4.Runtime.Misc;
using Casm.Antlr;
using System;
using System.Collections.Generic;

namespace Casm.Assembler
{
    public class CompilerListener : CasmBaseListener
    {
        private readonly RegisterOperand _stackRegister = new RegisterOperand(15);
        private readonly RegisterOperand _tempRegister1 = new RegisterOperand(14);
        private readonly RegisterOperand _tempRegister2 = new RegisterOperand(13);

        private readonly Tree _tree;

        private uint _currentBytePosition;

        public CompilerListener(Tree tree)
        {
            _tree = tree;
        }

        public override void EnterLabel([NotNull] CasmParser.LabelContext context)
        {
            var labelText = context.LABEL().GetText();

            _tree.AddNodes(new Label(_currentBytePosition, labelText.TrimEnd(':')));
        }

        public override void EnterInstruction([NotNull] CasmParser.InstructionContext context)
        {
            var instructionName = context?.NAME()?.GetText();

            if (instructionName == null)
            {
                throw new InvalidOperationException("Could not find the opcode for the instruction.");
            }

            var operandTypes = new List<OperandType>();
            var operands = new List<Operand>();

            foreach (var expression in context.expressionList().expression())
            {
                if (expression.register() != null)
                {
                    operandTypes.Add(OperandType.Register);
                    operands.Add(context.ExtractRegister(operands.Count));
                }
                else if (expression.registerReference() != null)
                {
                    operandTypes.Add(OperandType.RegisterReference);
                    operands.Add(context.ExtractRegisterReference(operands.Count));
                }
                else if (expression.NUMBER() != null)
                {
                    operandTypes.Add(OperandType.Immediate);
                    operands.Add(context.ExtractImmediate(operands.Count));
                }
                else if (expression.NAME() != null)
                {
                    operandTypes.Add(OperandType.Label);
                    operands.Add(new ImmediateOperand(expression.NAME().GetText()));
                }
            }

            if (InstructionMap.InstructionDefinitions.TryGetValue(new InstructionKey(instructionName, operandTypes.ToArray()), out var instructionDefinitions))
            {
                foreach (var instructionDefinition in instructionDefinitions)
                {
                    _tree.AddNodes(new Instruction(_currentBytePosition, instructionDefinition, operands.ToArray()));
                    _currentBytePosition += 4;
                }

                return;
            }
        }

        public override void EnterDirective([NotNull] CasmParser.DirectiveContext context)
        {
            var directive = context.DIRECTIVE().GetText();

            switch (directive.ToLowerInvariant())
            {
                case "@org":
                    _tree.AddNodes(new Directive(_currentBytePosition));
                    _currentBytePosition = uint.Parse(context.expressionList().expression(0).NUMBER().GetText());

                    break;
            }
        }
    }
}