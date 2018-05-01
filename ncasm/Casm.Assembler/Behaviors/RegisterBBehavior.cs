namespace Casm.Assembler.Behaviors
{
    public class RegisterBBehavior : InstructionBehavior
    {
        private readonly int _operandPosition;

        public RegisterBBehavior(int operandPosition)
        {
            _operandPosition = operandPosition;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (RegisterOperand)operands[_operandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_0000_1111) | ((operand.RegisterIndex & 0b1111) << 4);
        }
    }
}