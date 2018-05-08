namespace Casm.Base.Behaviors
{
    public class RegisterBBehavior : OperandBehavior
    {
        public RegisterBBehavior(int operandPosition) : base(operandPosition)
        {

        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            var operand = (RegisterOperand)operands[OperandPosition];

            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_0000_1111) | ((operand.RegisterIndex & 0b1111) << 4);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            var registerIndex = (instruction & 0b0000_0000_0000_0000_0000_0000_1111_0000) >> 4;

            return $"r{registerIndex}";
        }
    }
}