using Antlr4.Runtime.Misc;
using Casm.Antlr;
using System;
using System.Collections.Generic;
using Casm.Assembler.Base.Behaviors;

namespace Casm.Assembler.Base
{
    public class CompilerListener : CasmBaseListener
    {
        private readonly RegisterOperand _stackRegister = new RegisterOperand(15);
        private readonly RegisterOperand _tempRegister1 = new RegisterOperand(14);
        private readonly RegisterOperand _tempRegister2 = new RegisterOperand(13);

        private readonly Tree _tree;

        private uint _currentBytePosition;

        private readonly Dictionary<InstructionKey, InstructionDefinition> _instructionDefinitions = new Dictionary<InstructionKey, InstructionDefinition>
        {
            [new InstructionKey("movl", OperandType.Immediate, OperandType.Register)] = new InstructionDefinition(
                new FunctionSelectBehavior(0b000),
                new DBusSelectBehavior(0b000),
                new OpcodeBehavior(0b000_000),
                new RegisterDBehavior(1),
                new ImmediateBehavior(0))
        };

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

            foreach (var expression in context.expressionList().expression())
            {
                if (expression.register() != null)
                {
                    operandTypes.Add(OperandType.Register);
                }
                else if (expression.registerReference() != null)
                {
                    operandTypes.Add(OperandType.RegisterReference);
                }
                else if (expression.NUMBER() != null)
                {
                    operandTypes.Add(OperandType.Immediate);
                }
                else if (expression.NAME() != null)
                {
                    operandTypes.Add(OperandType.Label);
                }
            }

            if (_instructionDefinitions.TryGetValue(new InstructionKey(instructionName, operandTypes.ToArray()), out var instructionDefinition))
            {

            }

            switch (instructionName.ToLowerInvariant())
            {
                case "movl":
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_000, context.ExtractRegister2(), context.ExtractImmediate1(ImmediateType.Low), ImmediateType.Low));
                    _currentBytePosition += 4;
                    break;

                case "movh":
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_001, context.ExtractRegister2(), context.ExtractImmediate1(ImmediateType.Low), ImmediateType.Low));
                    _currentBytePosition += 4;
                    break;

                case "movlh":
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_000, context.ExtractRegister2(), context.ExtractImmediate1(ImmediateType.Low), ImmediateType.Low));
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition + 4, 0b000, 0b000, 0b000_001, context.ExtractRegister2(), context.ExtractImmediate1(ImmediateType.High), ImmediateType.Low));

                    _currentBytePosition += 8;
                    break;

                case "mov":
                    _tree.AddNodes(new MoveRegisterToRegister(_currentBytePosition, 0b000, 0b001, 0b000_010, context.ExtractRegister1(), context.ExtractRegister2()));
                    _currentBytePosition += 4;
                    break;

                case "ldr":
                    _tree.AddNodes(new LoadInstruction(_currentBytePosition, 0b000, 0b010, 0b000_011, context.ExtractRegisterReference1(), context.ExtractRegister2()));
                    _currentBytePosition += 4;
                    break;

                case "str":
                    _tree.AddNodes(new StoreInstruction(_currentBytePosition, 0b000, 0b001, 0b000_100, context.ExtractRegister1(), context.ExtractRegisterReference2()));
                    _currentBytePosition += 4;
                    break;

                case "movf":
                    _tree.AddNodes(new MoveRegisterToRegister(_currentBytePosition, 0b000, 0b100, 0b000_101, RegisterOperand.None, context.ExtractRegister1()));
                    _currentBytePosition += 4;
                    break;

                case "add":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_000, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "sub":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_001, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "mul":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_010, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "div":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_011, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "rem":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_100, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "neg":
                    _tree.AddNodes(new TwoOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_101, context.ExtractRegister1(), context.ExtractRegister2()));
                    _currentBytePosition += 4;
                    break;

                case "and":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_110, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "or":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_111, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "xor":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_000, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "not":
                    _tree.AddNodes(new TwoOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_001, context.ExtractRegister1(), context.ExtractRegister2()));
                    _currentBytePosition += 4;
                    break;

                case "lsh":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_010, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "rsh":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_011, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "arsh":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_100, context.ExtractRegister1(), context.ExtractRegister2(), context.ExtractRegister3()));
                    _currentBytePosition += 4;
                    break;

                case "cmp":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_101, context.ExtractRegister1(), context.ExtractRegister2(), RegisterOperand.None));
                    _currentBytePosition += 4;
                    break;

                case "test":
                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b001_110, context.ExtractRegister1(), context.ExtractRegister2(), RegisterOperand.None));
                    _currentBytePosition += 4;
                    break;

                case "j":
                    AddJumpInstruction(context, 0b000_000);
                    break;

                case "je":
                    AddJumpInstruction(context, 0b000_001);
                    break;

                case "jne":
                    AddJumpInstruction(context, 0b000_010);
                    break;

                case "jl":
                    AddJumpInstruction(context, 0b000_011);
                    break;

                case "jg":
                    AddJumpInstruction(context, 0b000_100);
                    break;

                case "push":
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_000, _tempRegister1, new ImmediateOperand(4), ImmediateType.Low));
                    _currentBytePosition += 4;

                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_001, _stackRegister, _tempRegister1, _stackRegister));
                    _currentBytePosition += 4;

                    _tree.AddNodes(new StoreInstruction(_currentBytePosition, 0b000, 0b001, 0b000_100, context.ExtractRegister1(), _stackRegister));
                    _currentBytePosition += 4;
                    break;

                case "pop":
                    _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_000, _tempRegister1, new ImmediateOperand(4), ImmediateType.Low));
                    _currentBytePosition += 4;

                    _tree.AddNodes(new LoadInstruction(_currentBytePosition, 0b000, 0b010, 0b000_011, _stackRegister, context.ExtractRegister1()));
                    _currentBytePosition += 4;

                    _tree.AddNodes(new ThreeOperandAluInstruction(_currentBytePosition, 0b001, 0b011, 0b000_000, _stackRegister, _tempRegister1, _stackRegister));
                    _currentBytePosition += 4;
                    break;
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

        protected void AddJumpInstruction(CasmParser.InstructionContext context, uint opcode)
        {
            var labelText = context.expressionList().expression(0).NAME()?.GetText();

            if (labelText != null)
            {
                _tree.AddNodes(new ImmediateInstruction(_currentBytePosition, 0b000, 0b000, 0b000_000, _tempRegister1, new ImmediateOperand(labelText), ImmediateType.Low));
                _tree.AddNodes(new ImmediateInstruction(_currentBytePosition + 4, 0b000, 0b000, 0b000_001, _tempRegister1, new ImmediateOperand(labelText), ImmediateType.High));
                _tree.AddNodes(new JumpInstruction(_currentBytePosition + 8, 0b010, 0b001, opcode, _tempRegister1));

                _currentBytePosition += 12;
            }
            else
            {
                _tree.AddNodes(new JumpInstruction(_currentBytePosition, 0b010, 0b001, opcode, context.ExtractRegister1()));
                _currentBytePosition += 4;
            }
        }
    }
}