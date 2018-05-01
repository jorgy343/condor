using Casm.Assembler.Behaviors;
using System.Collections.Generic;

namespace Casm.Assembler
{
    public static class InstructionMap
    {
        public static readonly Dictionary<InstructionKey, IEnumerable<InstructionDefinition>> InstructionDefinitions = new Dictionary<InstructionKey, IEnumerable<InstructionDefinition>>();

        static InstructionMap()
        {
            InstructionDefinitions[new InstructionKey("movl", OperandType.Immediate, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("movh", OperandType.Immediate, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new ImmediateBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("mov", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_010),
                    new RegisterABehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("ldr", OperandType.RegisterReference, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b010),
                    new OpcodeBehavior(0b000_011),
                    new RegisterBBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("str", OperandType.Register, OperandType.RegisterReference)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_100),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("cmp", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b001_101),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("test", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b001_110),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1))
            };

            CreateThreeOperandAluInstruction("add", 0b000_000);
            CreateThreeOperandAluInstruction("sub", 0b000_001);
            CreateThreeOperandAluInstruction("mul", 0b000_010);
            CreateThreeOperandAluInstruction("div", 0b000_011);
            CreateThreeOperandAluInstruction("rem", 0b000_100);
            CreateTwoOperandAluInstruction("neg", 0b000_101);
            CreateThreeOperandAluInstruction("and", 0b000_110);
            CreateThreeOperandAluInstruction("or", 0b000_111);
            CreateThreeOperandAluInstruction("xor", 0b001_000);
            CreateTwoOperandAluInstruction("not", 0b001_001);
            CreateThreeOperandAluInstruction("lsh", 0b001_010);
            CreateThreeOperandAluInstruction("rsh", 0b001_011);
            CreateThreeOperandAluInstruction("arsh", 0b001_100);

            CreateRegisterJumpInstruction("j", 0b000_000);
            CreateRegisterJumpInstruction("je", 0b000_001);
            CreateRegisterJumpInstruction("jne", 0b000_010);
            CreateRegisterJumpInstruction("jl", 0b000_011);
            CreateRegisterJumpInstruction("jg", 0b000_100);

            CreateRegisterJumpInstructionLabel("j", 0b000_000);
            CreateRegisterJumpInstructionLabel("je", 0b000_001);
            CreateRegisterJumpInstructionLabel("jne", 0b000_010);
            CreateRegisterJumpInstructionLabel("jl", 0b000_011);
            CreateRegisterJumpInstructionLabel("jg", 0b000_100);

            InstructionDefinitions[new InstructionKey("push", OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateConstBehavior(4),
                    new RegisterDConstIndexBehavior(14)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b000_001),
                    new RegisterAConstIndexBehavior(15),
                    new RegisterBConstIndexBehavior(14),
                    new RegisterDConstIndexBehavior(15)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_100),
                    new RegisterABehavior(0),
                    new RegisterBConstIndexBehavior(15))
            };

            InstructionDefinitions[new InstructionKey("pop", OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateConstBehavior(4),
                    new RegisterDConstIndexBehavior(14)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b010),
                    new OpcodeBehavior(0b000_011),
                    new RegisterBConstIndexBehavior(15),
                    new RegisterDBehavior(0)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b000_000),
                    new RegisterAConstIndexBehavior(15),
                    new RegisterBConstIndexBehavior(14),
                    new RegisterDConstIndexBehavior(15)),
            };
        }

        private static void CreateTwoOperandAluInstruction(string name, uint opcode)
        {
            InstructionDefinitions[new InstructionKey(name, OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0),
                    new RegisterDBehavior(1))
            };
        }

        private static void CreateThreeOperandAluInstruction(string name, uint opcode)
        {
            InstructionDefinitions[new InstructionKey(name, OperandType.Register, OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1),
                    new RegisterDBehavior(2))
            };
        }

        private static void CreateRegisterJumpInstruction(string name, uint opcode)
        {
            InstructionDefinitions[new InstructionKey(name, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b010),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0))
            };
        }

        private static void CreateRegisterJumpInstructionLabel(string name, uint opcode)
        {
            InstructionDefinitions[new InstructionKey(name, OperandType.Label)] = new[]
            {
                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new RegisterDConstIndexLabelBehavior(0, 14, ImmediateType.Low)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new RegisterDConstIndexLabelBehavior(0, 14, ImmediateType.High)),

                new InstructionDefinition(
                    new FunctionSelectBehavior(0b010),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(opcode),
                    new RegisterAConstIndexBehavior(14))
            };
        }
    }
}