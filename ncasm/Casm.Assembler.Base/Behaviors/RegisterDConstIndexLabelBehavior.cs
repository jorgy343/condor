namespace Casm.Assembler.Base.Behaviors
{
    public class RegisterDConstIndexLabelBehavior : InstructionBehavior
    {
        private readonly int _operandIndex;
        private readonly uint _registerIndex;
        private readonly ImmediateType _immediateType;

        public RegisterDConstIndexLabelBehavior(int operandIndex, uint registerIndex, ImmediateType immediateType)
        {
            _operandIndex = operandIndex;
            _registerIndex = registerIndex;
            _immediateType = immediateType;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (ImmediateOperand)operands[_operandIndex];
            var immediate = _immediateType == ImmediateType.Low ? operand.Value & 0xffff : operand.Value >> 16;

            instruction = (instruction & 0b1111_1111_1111_0000_0000_0000_0000_0000) | ((_registerIndex & 0b1111) << 16) | immediate;
        }
    }
}