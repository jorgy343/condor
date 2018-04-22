namespace Casm.Assembler.Base
{
    public class ThreeOperandAluInstruction : Instruction
    {
        public ThreeOperandAluInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand registerA, RegisterOperand registerB, RegisterOperand registerD)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            RegisterA = registerA;
            RegisterB = registerB;
            RegisterD = registerD;
        }

        public RegisterOperand RegisterA { get; }
        public RegisterOperand RegisterB { get; }
        public RegisterOperand RegisterD { get; }

        public override uint ToMachineCode()
        {
            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((RegisterD.RegisterIndex & 0b1111) << 16)
                | ((RegisterB.RegisterIndex & 0b1111) << 4)
                | ((RegisterA.RegisterIndex & 0b1111) << 0);
        }
    }
}