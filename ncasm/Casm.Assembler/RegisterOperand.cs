namespace Casm.Assembler
{
    public class RegisterOperand : Operand
    {
        public static readonly RegisterOperand None = new RegisterOperand(0);

        public RegisterOperand(uint registerIndex)
        {
            RegisterIndex = registerIndex;
        }

        public uint RegisterIndex { get; }
    }
}