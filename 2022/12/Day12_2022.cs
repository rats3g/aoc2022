using System.Xml.Linq;

namespace AdventOfCode;

internal class Day12_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var nodes = GenerateGraph(lines);

        Node? start = null;
        Node? end = null;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S') start = nodes[(i, j)];
                if (lines[i][j] == 'E') end = nodes[(i, j)];
            }
        }        

        return ShortestPath(nodes.Values, end!)[start!].ToString();
    }

    private static Dictionary<(int, int), Node> GenerateGraph(string[] lines)
    {
        var nodes = new Dictionary<(int, int), Node>();

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

            }
        }

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (i > 0 && nodes[(i - 1, j)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i - 1, j)].AddNeighbor(nodes[(i, j)]);
                }

                if (i < lines.Length - 1 && nodes[(i + 1, j)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i + 1, j)].AddNeighbor(nodes[(i, j)]);
                }

                if (j > 0 && nodes[(i, j - 1)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j - 1)].AddNeighbor(nodes[(i, j)]);
                }

                if (j < lines[i].Length - 1 && nodes[(i, j + 1)].Height - nodes[(i, j)].Height < 2)
                {
                    nodes[(i, j + 1)].AddNeighbor(nodes[(i, j)]);
                }
            }
        }

        return nodes;
    }

    private static Dictionary<INode, int> ShortestPath(IEnumerable<INode> nodes, INode start)
    {
        var distance = new Dictionary<INode, int>();
        var remaining = nodes.ToList();

        foreach (var node in nodes)
        {
            distance[node] = int.MaxValue;
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
                }
            }
        }

        return distance;
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var nodes = GenerateGraph(lines);

        var startingNodes = new List<INode>();
        Node? end = null;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S' || lines[i][j] == 'a') startingNodes.Add(nodes[(i, j)]);
                if (lines[i][j] == 'E') end = nodes[(i, j)];
            }
        }

        var distances = ShortestPath(nodes.Values, end!);
        return startingNodes.Where(start => distances[start] > 0).Min(start => distances[start]).ToString();
    }
}