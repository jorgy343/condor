namespace Casm.Assembler.Base.Behaviors
{
    public class DBusSelectBehavior : InstructionBehavior
    {
        private readonly uint _dBusSelect;

        public DBusSelectBehavior(uint dBusSelect)
        {
            _dBusSelect = dBusSelect;
        }

        public override void ApplyBehavior(ref uint instruction, params Operand[] operands)
        {
            instruction = (instruction & 0b1110_0011_1111_1111_1111_1111_1111_1111) | ((_dBusSelect & 0b111) << 26);
        }
    }
}