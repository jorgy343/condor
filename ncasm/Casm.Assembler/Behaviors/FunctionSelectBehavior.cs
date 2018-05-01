namespace Casm.Assembler.Behaviors
{
    public class FunctionSelectBehavior : InstructionBehavior
    {
        private readonly uint _functionSelect;

        public FunctionSelectBehavior(uint functionSelect)
        {
            _functionSelect = functionSelect;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b0001_1111_1111_1111_1111_1111_1111_1111) | ((_functionSelect & 0b111) << 29);
        }
    }
}