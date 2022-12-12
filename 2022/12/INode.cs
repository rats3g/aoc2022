namespace AdventOfCode
{
    internal interface INode
    {
        IEnumerable<INode> GetNeighbors();
    }
}
