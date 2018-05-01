namespace Casm.Assembler.Behaviors
{
    public class RegisterDConstIndexBehavior : InstructionBehavior
    {
        private readonly uint _registerIndex;

        public RegisterDConstIndexBehavior(uint registerIndex)
        {
            _registerIndex = registerIndex;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_0000_1111_1111_1111_1111) | ((_registerIndex & 0b1111) << 16);
        }
    }
}