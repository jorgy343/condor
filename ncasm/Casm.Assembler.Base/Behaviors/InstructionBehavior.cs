using Casm.Antlr;

namespace Casm.Assembler.Base.Behaviors
{
    public abstract class InstructionBehavior
    {
        public abstract void ApplyBehavior(ref uint instruction, params Operand[] operands);
    }
}