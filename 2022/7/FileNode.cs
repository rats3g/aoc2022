namespace AdventOfCode
{
    internal class FileNode : FileSystemNode
    {
        private readonly int _size;

        public FileNode(string name, DirectoryNode? parent, int size) : base(name, parent)
        {
            _size = size;
        }

        public override int Size => _size;
    }
}
