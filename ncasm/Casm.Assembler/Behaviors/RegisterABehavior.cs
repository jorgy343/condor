namespace Casm.Assembler.Behaviors
{
    public class RegisterABehavior : OperandBehavior
    {
        public RegisterABehavior(int operandPosition) : base(operandPosition)
        {

        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (RegisterOperand)operands[OperandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_1111_0000) | ((operand.RegisterIndex & 0b1111) << 0);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            var registerIndex = (instruction & 0b0000_0000_0000_0000_0000_0000_0000_1111) >> 0;

            return $"r{registerIndex}";
        }
    }
}