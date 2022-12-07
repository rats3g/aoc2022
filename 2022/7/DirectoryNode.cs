namespace AdventOfCode
{
    internal class DirectoryNode : FileSystemNode
    {
        private readonly IDictionary<string, FileSystemNode> _children;

        public DirectoryNode(string name, DirectoryNode? parent) : base(name, parent)
        {
            _children = new Dictionary<string, FileSystemNode>();
        }

        public IEnumerable<FileSystemNode> Children => _children.Values;

        public override int Size => _children.Values.Sum(child => child.Size);

        public FileSystemNode GetChild(string name)
        {
            return _children[name];
        }

        public void AddChild(FileSystemNode child)
        {
            _children.Add(child.Name, child);
        }
    }
}
