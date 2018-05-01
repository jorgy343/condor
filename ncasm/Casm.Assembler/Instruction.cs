namespace Casm.Assembler
{
    public class Instruction : Node
    {
        public Instruction(uint position, InstructionDefinition instructionDefinition, params Operand[] operands) : base(position)
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