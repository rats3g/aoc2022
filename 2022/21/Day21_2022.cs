using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day21_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var nodes = new Dictionary<string, MonkeyNode>();

        await foreach (var line in lines)
        {
            ParseLine(line, nodes);
        }

        return nodes["root"].GetValue().ToString();
    }

    private void ParseLine(string line, Dictionary<string, MonkeyNode> nodes)
    {
        var match = Regex.Match(line, @"(?<name>\w+):\s+(?<left>\w+)\s+(?<operation>.)\s+(?<right>\w+)");

        if (match.Success)
        {
            var operation = match.Groups["operation"].Value switch
            {
                "+" => Operation.Add,
                "-" => Operation.Subtract,
                "*" => Operation.Multiply,
                "/" => Operation.Divide
            };

            nodes[match.Groups["name"].Value] = new MonkeyNode(nodes, (match.Groups["left"].Value, match.Groups["right"].Value), operation);
        }

        match = Regex.Match(line, @"(?<name>\w+):\s+(?<value>\d+)");

        if (match.Success)
        {
            nodes[match.Groups["name"].Value] = new MonkeyNode(nodes, long.Parse(match.Groups["value"].Value));
        }
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var nodes = new Dictionary<string, MonkeyNode>();

        await foreach (var line in lines)
        {
            ParseLine(line, nodes);
        }

        var path = new Stack<string>();
        FindPath(nodes, "root", path);
        path.Pop();

        var (rootLeft, rootRight) = nodes["root"].GetOperands();
        var target = rootLeft == path.Peek()
            ? nodes[rootRight].GetValue()
            : nodes[rootLeft].GetValue();

        var current = path.Pop();   

        while (current != "humn")
        {
            target = nodes[current].InverseValue(path.Peek(), target);
            current = path.Pop();
        }

        return target.ToString();
    }

    private bool FindPath(Dictionary<string, MonkeyNode> nodes, string node, Stack<string> path)
    {
        if (node == "humn")
        {
            path.Push(node);
            return true;
        }

        var (left, right) = nodes[node].GetOperands();

        if (left != null && FindPath(nodes, left, path))
        {
            path.Push(node);
            return true;
        }

        if (right != null && FindPath(nodes, right, path))
        {
            path.Push(node);
            return true;
        }

        return false;
    }
}