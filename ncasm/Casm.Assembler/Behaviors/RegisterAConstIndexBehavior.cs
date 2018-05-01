namespace Casm.Assembler.Behaviors
{
    public class RegisterAConstIndexBehavior : InstructionBehavior
    {
        private readonly uint _registerIndex;

        public RegisterAConstIndexBehavior(uint registerIndex)
        {
            _registerIndex = registerIndex;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_1111_0000) | ((_registerIndex & 0b1111) << 0);
        }
    }
}