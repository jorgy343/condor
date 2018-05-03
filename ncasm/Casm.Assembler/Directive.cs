using System;

namespace Casm.Assembler
{
    public class Directive : Node
    {
        public Directive(uint position, string name) : base(position)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}