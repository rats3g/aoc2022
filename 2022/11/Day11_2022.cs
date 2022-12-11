namespace AdventOfCode;

internal class Day11_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var monkeys = new List<Monkey>();
        for (var i = 1; i < lines.Length - 4; i += 7)
        {
            monkeys.Add(ParseMonkey(lines[i..(i + 5)], Part.One));
        }

        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                var throws = monkey.InspectAndTest();
                foreach (var (item, target) in throws)
                {
                    monkeys[target].AddItem(item);
                }
            }
        }

        return monkeys
            .OrderByDescending(monkey => monkey.Inspections)
            .Take(2)
            .Aggregate(1ul, (value, monkey) => value * monkey.Inspections)
            .ToString();
    }

    private static Monkey ParseMonkey(IList<string> lines, Part part)
    {
        var startingItems = lines[0][18..].Split(',', StringSplitOptions.TrimEntries).Select(ulong.Parse);
        var operation = ParseOperation(lines[1]);
        var test = (ulong value) => value % uint.Parse(lines[2][21..]) == 0
            ? (value, int.Parse(lines[3][29..]))
            : (value, int.Parse(lines[4][30..]));
        
        return new Monkey(startingItems, operation, test, part);
    }

    private static Func<ulong, ulong> ParseOperation(string line)
    {
        var parameter = line[25..];
        if (line[23] == '*')
        {
            if (parameter == "old")
            {
                return (ulong old) => old * old;
            }
            else
            {
                return (ulong old) => old * ulong.Parse(parameter);
            }
        }
        else
        {
            if (parameter == "old")
            {
                return (ulong old) => old + old;
            }
            else
            {
                return (ulong old) => old + ulong.Parse(parameter);
            }
        }
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var monkeys = new List<Monkey>();
        for (var i = 1; i < lines.Length - 4; i += 7)
        {
            monkeys.Add(ParseMonkey(lines[i..(i + 5)], Part.Two));
        }

        for (var i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                var throws = monkey.InspectAndTest();
                foreach (var (item, target) in throws)
                {
                    monkeys[target].AddItem(item);
                }
            }
        }

        return monkeys
            .OrderByDescending(monkey => monkey.Inspections)
            .Take(2)
            .Aggregate(1ul, (value, monkey) => value * monkey.Inspections)
            .ToString();
    }
}