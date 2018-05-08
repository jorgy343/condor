namespace Casm.Base.Behaviors
{
    public class ImmediateConstBehavior : OperandBehavior
    {
        private readonly uint _immediateValue;

        public ImmediateConstBehavior(int operandPosition, uint immediateValue) : base(operandPosition)
        {
            _immediateValue = immediateValue;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_1111_0000_0000_0000_0000) | ((_immediateValue & 0b1111_1111_1111_1111) << 0);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            var immediate = instruction & 0b0000_0000_0000_0000_1111_1111_1111_1111;

            return $"0x{immediate:x}";
        }
    }
}