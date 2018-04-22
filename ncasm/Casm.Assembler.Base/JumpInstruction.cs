namespace Casm.Assembler.Base
{
    public class JumpInstruction : Instruction
    {
        public JumpInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand registerA)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            RegisterA = registerA;
        }

        public RegisterOperand RegisterA { get; }

        public override uint ToMachineCode()
        {
            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((RegisterA.RegisterIndex & 0b1111) << 0);
        }
    }
}