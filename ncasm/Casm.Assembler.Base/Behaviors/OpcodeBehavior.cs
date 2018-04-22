namespace Casm.Assembler.Base.Behaviors
{
    public class OpcodeBehavior : InstructionBehavior
    {
        private readonly uint _opcode;

        public OpcodeBehavior(uint opcode)
        {
            _opcode = opcode;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1100_0000_1111_1111_1111_1111_1111) | ((_opcode & 0b111_111) << 20);
        }
    }
}