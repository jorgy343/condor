namespace Casm.Assembler.Base.Behaviors
{
    public class RegisterABehavior : InstructionBehavior
    {
        private readonly int _operandPosition;

        public RegisterABehavior(int operandPosition)
        {
            _operandPosition = operandPosition;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (RegisterOperand)operands[_operandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_1111_0000) | ((operand.RegisterIndex & 0b1111) << 0);
        }
    }
}