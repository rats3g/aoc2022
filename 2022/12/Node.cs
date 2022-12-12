namespace AdventOfCode
{
    internal class Node : INode
    {
        private readonly ISet<INode> _neighbors;

        public Node(char height)
        {
            _neighbors = new HashSet<INode>();
            Height = height;
        }

        public char Height { get; }

        public IEnumerable<INode> GetNeighbors() => _neighbors;

        public bool AddNeighbor(INode node) => _neighbors.Add(node);

        public bool IsNeighbor(INode node) => _neighbors.Contains(node);
    }
}
