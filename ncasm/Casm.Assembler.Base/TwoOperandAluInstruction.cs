﻿namespace Casm.Assembler.Base
{
    public class TwoOperandAluInstruction : Instruction
    {
        public TwoOperandAluInstruction(uint position, uint functionSelect, uint dBusSelect, uint opcode, RegisterOperand registerA, RegisterOperand registerD)
            : base(position, functionSelect, dBusSelect, opcode)
        {
            RegisterA = registerA;
            RegisterD = registerD;
        }

        public RegisterOperand RegisterA { get; }
        public RegisterOperand RegisterD { get; }

        public override uint ToMachineCode()
        {
            return
                ((FunctionSelect & 0b111) << 29)
                | ((DBusSelect & 0b111) << 26)
                | ((Opcode & 0b111_111) << 20)
                | ((RegisterD.RegisterIndex & 0b1111) << 16)
                | ((RegisterA.RegisterIndex & 0b1111) << 0);
        }
    }
}