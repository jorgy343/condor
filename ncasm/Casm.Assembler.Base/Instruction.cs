namespace Casm.Assembler.Base
{
    public abstract class Instruction : Node
    {
        protected Instruction(uint position, uint functionSelect, uint dBusSelect, uint opcode) : base(position)
        {
            FunctionSelect = functionSelect;
            DBusSelect = dBusSelect;
            Opcode = opcode;
        }

        public uint FunctionSelect { get; }
        public uint DBusSelect { get; }
        public uint Opcode { get; }

        public abstract uint ToMachineCode();
    }
}