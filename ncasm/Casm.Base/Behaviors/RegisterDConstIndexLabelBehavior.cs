namespace Casm.Base.Behaviors
{
    public class RegisterDConstIndexLabelBehavior : OperandBehavior
    {
        private readonly uint _registerIndex;
        private readonly ImmediateType _immediateType;

        public RegisterDConstIndexLabelBehavior(int operandPosition, uint registerIndex, ImmediateType immediateType) : base(operandPosition)
        {
            _registerIndex = registerIndex;
            _immediateType = immediateType;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (ImmediateOperand)operands[OperandPosition];
            var immediate = _immediateType == ImmediateType.Low ? operand.Value & 0xffff : operand.Value >> 16;

            instruction = (instruction & 0b1111_1111_1111_0000_0000_0000_0000_0000) | ((_registerIndex & 0b1111) << 16) | immediate;
        }
    }
}