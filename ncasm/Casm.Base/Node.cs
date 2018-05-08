namespace Casm.Base
{
    public abstract class Node
    {
        protected Node(uint position)
        {
            Position = position;
        }

        public uint Position { get; }
    }
}