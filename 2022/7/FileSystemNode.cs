namespace AdventOfCode
{
    internal abstract class FileSystemNode
    {
        public FileSystemNode(string name, DirectoryNode? parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }

        public DirectoryNode? Parent { get; }

        public abstract int Size { get; }
    }
}
