using Casm.Assembler.Base.Behaviors;

namespace Casm.Assembler.Base
{
    public class InstructionDefinition
    {
        private readonly InstructionBehavior[] _behaviors;

        public InstructionDefinition(params InstructionBehavior[] behaviors)
        {
            _behaviors = behaviors;
        }

        public uint CreateMachineCode(params Operand[] operands)
        {
            var machineCode = 0u;

            foreach (var behavior in _behaviors)
            {
                behavior.ApplyBehavior(ref machineCode, operands);
            }

            return machineCode;
        }
    }
}