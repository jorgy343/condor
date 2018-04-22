namespace Casm.Assembler.Base
{
    public class Label : Node
    {
        public Label(uint position, string name) : base(position)
        {
            Name = name;
        }

        public string Name { get; }
    }
}