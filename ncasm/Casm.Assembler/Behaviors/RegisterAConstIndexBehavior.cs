namespace Casm.Assembler.Behaviors
{
    public class RegisterAConstIndexBehavior : OperandBehavior
    {
        private readonly uint _registerIndex;

        public RegisterAConstIndexBehavior(int operandPosition, uint registerIndex) : base(operandPosition)
        {
            _registerIndex = registerIndex;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_1111_0000) | ((_registerIndex & 0b1111) << 0);
        }

        public override string GetStringRepresentation(uint instruction)
        {
            return $"r{_registerIndex}";
        }
    }
}