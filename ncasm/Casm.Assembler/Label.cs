namespace Casm.Assembler
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