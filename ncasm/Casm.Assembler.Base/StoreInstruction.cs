namespace Casm.Assembler.Base
{
    public class StoreInstruction : Instruction
    {
        public StoreInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand registerA, RegisterOperand registerAddress)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            RegisterA = registerA;
            RegisterAddress = registerAddress;
        }

        public RegisterOperand RegisterA { get; }
        public RegisterOperand RegisterAddress { get; }

        public override uint ToMachineCode()
        {
            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((RegisterAddress.RegisterIndex & 0b1111) << 4)
                | ((RegisterA.RegisterIndex & 0b1111) << 0);
        }
    }
}