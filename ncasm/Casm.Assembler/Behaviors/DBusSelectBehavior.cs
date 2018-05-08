namespace Casm.Assembler.Behaviors
{
    public class DBusSelectBehavior : InstructionBehavior
    {
        public DBusSelectBehavior(uint dBusSelect)
        {
            DBusSelect = dBusSelect;
        }

        public uint DBusSelect { get; }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1110_0011_1111_1111_1111_1111_1111_1111) | ((DBusSelect & 0b111) << 26);
        }
    }
}