using Casm.Assembler.Behaviors;

namespace Casm.Assembler
{
    public class InstructionDefinition
    {
        private readonly InstructionBehavior[] _behaviors;

        public InstructionDefinition(params InstructionBehavior[] behaviors)
        {
            _behaviors = behaviors;
        }

        public virtual uint CreateMachineCode(params Operand[] operands)
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