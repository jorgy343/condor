namespace Casm.Base.Behaviors
{
    public class RegisterDConstIndexBehavior : OperandBehavior
    {
        private readonly uint _registerIndex;

        public RegisterDConstIndexBehavior(int operandPosition, uint registerIndex) : base(operandPosition)
        {
            _registerIndex = registerIndex;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_0000_1111_1111_1111_1111) | ((_registerIndex & 0b1111) << 16);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            return $"r{_registerIndex}";
        }
    }
}