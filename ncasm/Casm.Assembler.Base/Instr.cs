namespace Casm.Assembler.Base
{
    public class Instr : Node
    {
        public Instr(uint position, InstructionDefinition instructionDefinition, params Operand[] operands) : base(position)
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