namespace Casm.Assembler.Behaviors
{
    public class OpcodeBehavior : InstructionBehavior
    {
        public OpcodeBehavior(uint opcode)
        {
            Opcode = opcode;
        }

        public uint Opcode { get; }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1100_0000_1111_1111_1111_1111_1111) | ((Opcode & 0b111_111) << 20);
        }
    }
}