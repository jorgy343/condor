namespace Casm.Assembler.Behaviors
{
    public class FunctionSelectBehavior : InstructionBehavior
    {
        public FunctionSelectBehavior(uint functionSelect)
        {
            FunctionSelect = functionSelect;
        }

        public uint FunctionSelect { get; }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b0001_1111_1111_1111_1111_1111_1111_1111) | ((FunctionSelect & 0b111) << 29);
        }
    }
}