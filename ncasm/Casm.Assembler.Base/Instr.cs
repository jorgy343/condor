namespace Casm.Assembler.Base
{
    public class Instr
    {
        public Instr(InstructionDefinition instructionDefinition, params Operand[] operands)
        {
            InstructionDefinition = instructionDefinition;
            Operands = operands;
        }

        public InstructionDefinition InstructionDefinition { get; }
        public Operand[] Operands { get; }

        public uint CreateMachineCode()
        {
            return InstructionDefinition.CreateMachineCode(Operands);
        }
    }
}