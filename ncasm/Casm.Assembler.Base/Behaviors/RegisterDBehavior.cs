namespace Casm.Assembler.Base.Behaviors
{
    public class RegisterDBehavior : InstructionBehavior
    {
        private readonly int _operandPosition;

        public RegisterDBehavior(int operandPosition)
        {
            _operandPosition = operandPosition;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (RegisterOperand)operands[_operandPosition];

            instruction = (instruction & 0b1111_1111_1111_0000_1111_1111_1111_1111) | ((operand.RegisterIndex & 0b1111) << 16);
        }
    }
}