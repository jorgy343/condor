namespace Casm.Assembler.Behaviors
{
    public class ImmediateConstBehavior : InstructionBehavior
    {
        private readonly uint _immediateValue;

        public ImmediateConstBehavior(uint immediateValue)
        {
            _immediateValue = immediateValue;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_1111_0000_0000_0000_0000) | ((_immediateValue & 0b1111_1111_1111_1111) << 0);
        }
    }
}