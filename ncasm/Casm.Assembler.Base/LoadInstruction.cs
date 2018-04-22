namespace Casm.Assembler.Base
{
    public class LoadInstruction : Instruction
    {
        public LoadInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand registerAddress, RegisterOperand registerD)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            RegisterAddress = registerAddress;
            RegisterD = registerD;
        }

        public RegisterOperand RegisterAddress { get; }
        public RegisterOperand RegisterD { get; }

        public override uint ToMachineCode()
        {
            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((RegisterD.RegisterIndex & 0b1111) << 16)
                | ((RegisterAddress.RegisterIndex & 0b1111) << 4);
        }
    }
}