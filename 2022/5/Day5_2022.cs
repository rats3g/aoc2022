using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day5_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var baseIndex = IdentifyBaseLine(lines);

        var stackCount = int.Parse(lines[baseIndex][^2].ToString());

        var stacks = new List<Stack<char>>(stackCount);
        for (var i = 0; i < stackCount; i++)
        {
            stacks.Add(new Stack<char>());
        }

        for (var i = baseIndex - 1; i >= 0; i--)
        {
            for(var j = 0; j < stackCount; j++)
            {
                var container = lines[i][j * 4 + 1];
                if (container != ' ')
                {
                    stacks[j].Push(container);
                }                
            }
        }

        foreach (var line in lines.Skip(baseIndex + 2))
        {
            var match = Regex.Match(line, @"move (?<quantity>\d+) from (?<origin>\d+) to (?<destination>\d+)");

            var origin = int.Parse(match.Groups["origin"].Value) - 1;
            var destination = int.Parse(match.Groups["destination"].Value) - 1;

            for (var i = 0; i < int.Parse(match.Groups["quantity"].Value); i++)
            {
                var container = stacks[origin].Pop();
                stacks[destination].Push(container);
            }
        }

        return stacks.Aggregate(new StringBuilder(), (builder, stack) => builder.Append(stack.Peek())).ToString();
    }

    private static int IdentifyBaseLine(IList<string> lines)
    {
        for (var i = 0; i < lines.Count; i++)
        {
            if (lines[i][1] == '1')
            {
                return i;
            }
        }

        return -1;
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var baseIndex = IdentifyBaseLine(lines);

        var stackCount = int.Parse(lines[baseIndex][^2].ToString());

        var stacks = new List<Stack<char>>(stackCount);
        for (var i = 0; i < stackCount; i++)
        {
            stacks.Add(new Stack<char>());
        }

        for (var i = baseIndex - 1; i >= 0; i--)
        {
            for (var j = 0; j < stackCount; j++)
            {
                var container = lines[i][j * 4 + 1];
                if (container != ' ')
                {
                    stacks[j].Push(container);
                }
            }
        }

        foreach (var line in lines.Skip(baseIndex + 2))
        {
            var match = Regex.Match(line, @"move (?<quantity>\d+) from (?<origin>\d+) to (?<destination>\d+)");

            var origin = int.Parse(match.Groups["origin"].Value) - 1;
            var destination = int.Parse(match.Groups["destination"].Value) - 1;
            var quantity = int.Parse(match.Groups["quantity"].Value);

            var tempStack = new Stack<char>();

            for (var i = 0; i < quantity; i++)
            {
                var container = stacks[origin].Pop();
                tempStack.Push(container);
            }

            for (var i = 0; i < quantity; i++)
            {
                var container = tempStack.Pop();
                stacks[destination].Push(container);
            }
        }

        return stacks.Aggregate(new StringBuilder(), (builder, stack) => builder.Append(stack.Peek())).ToString();
    }
}