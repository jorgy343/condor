namespace Casm.Assembler.Base.Behaviors
{
    public class RegisterBConstIndexBehavior : InstructionBehavior
    {
        private readonly uint _registerIndex;

        public RegisterBConstIndexBehavior(uint registerIndex)
        {
            _registerIndex = registerIndex;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1111_1111_1111_1111_1111_1111_0000_1111) | ((_registerIndex & 0b1111) << 4);
        }
    }
}