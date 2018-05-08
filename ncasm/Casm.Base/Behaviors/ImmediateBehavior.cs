namespace Casm.Base.Behaviors
{
    public class ImmediateBehavior : OperandBehavior
    {
        public ImmediateBehavior(int operandPosition) : base(operandPosition)
        {

        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (ImmediateOperand)operands[OperandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_0000_0000_0000_0000) | ((operand.Value & 0b1111_1111_1111_1111) << 0);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            var immediate = instruction & 0b0000_0000_0000_0000_1111_1111_1111_1111;

            return $"0x{immediate:x}";
        }
    }
}