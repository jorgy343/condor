using Casm.Antlr;

namespace Casm.Assembler.Behaviors
{
    public abstract class InstructionBehavior
    {
        public abstract void ApplyBehavior(ref uint instruction, params Operand[] operands);
    }
}