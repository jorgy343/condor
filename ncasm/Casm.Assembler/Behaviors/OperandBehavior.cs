namespace Casm.Assembler.Behaviors
{
    public abstract class OperandBehavior : InstructionBehavior
    {
        protected OperandBehavior(int operandPosition)
        {
            OperandPosition = operandPosition;
        }

        public int OperandPosition { get; }

        public virtual string GetStringRepresentation(uint instruction)
        {
            return "";
        }
    }
}