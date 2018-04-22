namespace Casm.Assembler.Base
{
    public class ImmediateInstruction : Instruction
    {
        public ImmediateInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand dRegister, ImmediateOperand immediate, ImmediateType immediateType)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            DRegister = dRegister;
            ImmediateOperand = immediate;
            ImmediateType = immediateType;
        }

        public RegisterOperand DRegister { get; }
        public ImmediateOperand ImmediateOperand { get; }
        public ImmediateType ImmediateType { get; }

        public override uint ToMachineCode()
        {
            var immediateValue = ImmediateType == ImmediateType.Low ? ImmediateOperand.Value & 0x0000_ffff : ImmediateOperand.Value >> 16;

            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((DRegister.RegisterIndex & 0b1111) << 16)
                | ((immediateValue & 0xffff) << 0);
        }
    }
}