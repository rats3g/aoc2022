namespace AdventOfCode;

internal class Day12_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);
        
        var nodes = new Dictionary<(int, int), Node>();

        Node? start = null;
        Node? end = null;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {                
                var height = lines[i][j] switch
                {
                    'S' => 'a',
                    'E' => 'z',
                    var x => x
                };

                nodes[(i, j)] = new Node(height);

                if (lines[i][j] == 'S') start = nodes[(i, j)];
                if (lines[i][j] == 'E') end = nodes[(i, j)];
            }
        }

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (i > 0 && nodes[(i - 1, j)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j)].AddNeighbor(nodes[(i - 1, j)]);
                }

                if (i < lines.Length - 1 && nodes[(i + 1, j)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j)].AddNeighbor(nodes[(i + 1, j)]);
                }

                if (j > 0 && nodes[(i, j - 1)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j)].AddNeighbor(nodes[(i, j - 1)]);
                }

                if (j < lines[i].Length - 1 && nodes[(i, j + 1)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j)].AddNeighbor(nodes[(i, j + 1)]);
                }
            }
        }

        return ShortestPath(nodes.Values, start!, end!).ToString();
    }

    private int ShortestPath(IEnumerable<INode> nodes, INode start, INode end)
    {
        var distance = new Dictionary<INode, int>();
        var previous = new Dictionary<INode, INode?>();
        var remaining = nodes.ToList();

        foreach (var node in nodes)
        {
            distance[node] = int.MaxValue;
            previous[node] = null;
        }

        distance[start] = 0;

        while (remaining.Count > 0)
        {
            var shortest = remaining.Aggregate((minimum, current) => minimum == null || distance[current] < distance[minimum] ? current : minimum);
            remaining.Remove(shortest);

            foreach (var neighbor in shortest.GetNeighbors().Intersect(remaining))
            {
                var newDistance = distance[shortest] + 1;
                if (newDistance < distance[neighbor])
                {
                    distance[neighbor] = newDistance;
                    previous[neighbor] = shortest;
                }
            }
        }

        return distance[end];
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        return "unsolved";
    }
}