namespace AdventOfCode;

internal class World
{
    private readonly IList<bool[]> _map;

    public World()
    {
        _map = new List<bool[]>
        {
            new bool[7] 
            {
                true, true, true, true, true, true, true
            }
        };
    }

    public int Length => _map.Count;

    public bool[] this[int index] => _map[index];

    public void AddRow()
    {
        _map.Add(new bool[7]);
    }

    public int GetHeight()
    {
        for (var i = Length - 1; i > -1; i--)
        {
            if (_map[i].Any(x => x))
            {
                return i;
            }
        }

        return -1;
    }
}