using Casm.Assembler.Behaviors;
using System;
using System.Collections.Immutable;

namespace Casm.Assembler
{
    public class InstructionDefinition
    {
        public InstructionDefinition(string name, params InstructionBehavior[] behaviors)
        {
            if (behaviors == null) throw new ArgumentNullException(nameof(behaviors));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Behaviors = ImmutableArray.Create(behaviors);
        }

        public string Name { get; }
        public ImmutableArray<InstructionBehavior> Behaviors { get; }

        public virtual uint CreateMachineCode(params Operand[] operands)
        {
            var machineCode = 0u;

            foreach (var behavior in Behaviors)
            {
                behavior.ApplyBehavior(ref machineCode, operands);
            }

            return machineCode;
        }
    }
}