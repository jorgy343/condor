using System;
using Casm.Assembler.Behaviors;
using System.Collections.Generic;
using System.Linq;

namespace Casm.Assembler
{
    public static class InstructionMap
    {
        public static readonly Dictionary<InstructionKey, IEnumerable<InstructionDefinition>> InstructionDefinitions = new Dictionary<InstructionKey, IEnumerable<InstructionDefinition>>();
        public static readonly Dictionary<(uint functionSelect, uint dBusSelect, uint opcode), InstructionDefinition> ReversedInstructionDefinitions = new Dictionary<(uint functionSelect, uint dBusSelect, uint opcode), InstructionDefinition>();

        static InstructionMap()
        {
            InstructionDefinitions[new InstructionKey("movl", OperandType.Immediate, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "movl",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("movh", OperandType.Immediate, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "movh",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new ImmediateBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("mov", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "mov",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_010),
                    new RegisterABehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("ldr", OperandType.RegisterReference, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "ldr",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b010),
                    new OpcodeBehavior(0b000_011),
                    new RegisterBBehavior(0),
                    new RegisterDBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("str", OperandType.Register, OperandType.RegisterReference)] = new[]
            {
                new InstructionDefinition(
                    "str",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_100),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("cmp", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "cmp",
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b001_101),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1))
            };

            InstructionDefinitions[new InstructionKey("test", OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "test",
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

            CreateJumpInstruction("j", 0b000_000);
            CreateJumpInstruction("je", 0b000_001);
            CreateJumpInstruction("jne", 0b000_010);
            CreateJumpInstruction("jl", 0b000_011);
            CreateJumpInstruction("jg", 0b000_100);

            CreateJumpInstructionLabel("j", 0b000_000);
            CreateJumpInstructionLabel("je", 0b000_001);
            CreateJumpInstructionLabel("jne", 0b000_010);
            CreateJumpInstructionLabel("jl", 0b000_011);
            CreateJumpInstructionLabel("jg", 0b000_100);

            InstructionDefinitions[new InstructionKey("push", OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "movl",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateConstBehavior(0, 4),
                    new RegisterDConstIndexBehavior(1, 14)),

                new InstructionDefinition(
                    "movh",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new ImmediateConstBehavior(0, 0),
                    new RegisterDConstIndexBehavior(1, 14)),

                new InstructionDefinition(
                    "sub",
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b000_001),
                    new RegisterAConstIndexBehavior(0, 15),
                    new RegisterBConstIndexBehavior(1, 14),
                    new RegisterDConstIndexBehavior(2, 15)),

                new InstructionDefinition(
                    "str",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(0b000_100),
                    new RegisterABehavior(0),
                    new RegisterBConstIndexBehavior(1, 15))
            };

            InstructionDefinitions[new InstructionKey("pop", OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    "movl",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new ImmediateConstBehavior(0, 4),
                    new RegisterDConstIndexBehavior(1, 14)),

                new InstructionDefinition(
                    "movh",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new ImmediateConstBehavior(0, 0),
                    new RegisterDConstIndexBehavior(1, 14)),

                new InstructionDefinition(
                    "ldr",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b010),
                    new OpcodeBehavior(0b000_011),
                    new RegisterBConstIndexBehavior(1, 15),
                    new RegisterDBehavior(0)),

                new InstructionDefinition(
                    "add",
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(0b000_000),
                    new RegisterAConstIndexBehavior(0, 15),
                    new RegisterBConstIndexBehavior(1, 14),
                    new RegisterDConstIndexBehavior(2, 15)),
            };

            CreateReversedLookup();
        }

        private static void CreateReversedLookup()
        {
            foreach (var instructionDefinitionGroup in InstructionDefinitions.Values.Where(x => x.Count() == 1))
            {
                foreach (var instructionDefinition in instructionDefinitionGroup)
                {
                    var functionSelect = instructionDefinition.Behaviors.OfType<FunctionSelectBehavior>().Single().FunctionSelect;
                    var dBusSelect = instructionDefinition.Behaviors.OfType<DBusSelectBehavior>().Single().DBusSelect;
                    var opcode = instructionDefinition.Behaviors.OfType<OpcodeBehavior>().Single().Opcode;

                    ReversedInstructionDefinitions[(functionSelect, dBusSelect, opcode)] = instructionDefinition;
                }
            }
        }

        private static void CreateTwoOperandAluInstruction(string name, uint opcode)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            InstructionDefinitions[new InstructionKey(name, OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    name,
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0),
                    new RegisterDBehavior(1))
            };
        }

        private static void CreateThreeOperandAluInstruction(string name, uint opcode)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            InstructionDefinitions[new InstructionKey(name, OperandType.Register, OperandType.Register, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    name,
                    new FunctionSelectBehavior(0b001),
                    new DBusSelectBehavior(0b011),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0),
                    new RegisterBBehavior(1),
                    new RegisterDBehavior(2))
            };
        }

        private static void CreateJumpInstruction(string name, uint opcode)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            InstructionDefinitions[new InstructionKey(name, OperandType.Register)] = new[]
            {
                new InstructionDefinition(
                    name,
                    new FunctionSelectBehavior(0b010),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(opcode),
                    new RegisterABehavior(0))
            };
        }

        private static void CreateJumpInstructionLabel(string name, uint opcode)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            InstructionDefinitions[new InstructionKey(name, OperandType.Label)] = new[]
            {
                new InstructionDefinition(
                    "movl",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_000),
                    new RegisterDConstIndexLabelBehavior(0, 14, ImmediateType.Low)),

                new InstructionDefinition(
                    "movh",
                    new FunctionSelectBehavior(0b000),
                    new DBusSelectBehavior(0b000),
                    new OpcodeBehavior(0b000_001),
                    new RegisterDConstIndexLabelBehavior(0, 14, ImmediateType.High)),

                new InstructionDefinition(
                    name,
                    new FunctionSelectBehavior(0b010),
                    new DBusSelectBehavior(0b001),
                    new OpcodeBehavior(opcode),
                    new RegisterAConstIndexBehavior(0, 14))
            };
        }
    }
}