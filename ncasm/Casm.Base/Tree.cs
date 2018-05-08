using System.Collections.Generic;

namespace Casm.Base
{
    public class Tree
    {
        public IList<Node> Nodes { get; } = new List<Node>();

        public void AddNodes(params Node[] nodes)
        {
            foreach (var node in nodes)
            {
                Nodes.Add(node);
            }
        }
    }
}