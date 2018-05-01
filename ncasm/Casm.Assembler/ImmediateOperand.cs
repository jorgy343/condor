using System;

namespace Casm.Assembler
{
    public class ImmediateOperand : Operand
    {
        public ImmediateOperand(uint value)
        {
            Value = value & 0xffff;
        }

        public ImmediateOperand(string labelName)
        {
            LabelName = labelName ?? throw new ArgumentNullException(nameof(labelName));
        }

        public uint Value { get; set; }
        public string LabelName { get; }
    }
}