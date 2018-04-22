namespace Casm.Assembler.Base.Behaviors
{
    public class ImmediateBehavior : InstructionBehavior
    {
        private readonly int _operandPosition;

        public ImmediateBehavior(int operandPosition)
        {
            _operandPosition = operandPosition;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (ImmediateOperand)operands[_operandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_0000_0000_0000_0000) | ((operand.Value & 0b1111_1111_1111_1111) << 0);
        }
    }
}