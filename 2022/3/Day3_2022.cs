namespace AdventOfCode;

internal class Day3_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var sum = 0;

        await foreach (var line in lines)
        {
            var compartment = new HashSet<char>(line[(line.Length / 2)..]);
            var mismatch = line[..(line.Length / 2)].FirstOrDefault(compartment.Contains);
            sum += GetPriority(mismatch);
        }

        return sum.ToString();
    }

    private static int GetPriority(char character)
    {
        var value = (int)character;
        return value > 96 ? value - 96 : value - 38;
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile).GetAsyncEnumerator();

        var sum = 0;

        while (await lines.MoveNextAsync())
        {
            var rucksack = new HashSet<char>(lines.Current);

            await lines.MoveNextAsync();
            var match1 = new HashSet<char>(lines.Current.Where(rucksack.Contains));

            await lines.MoveNextAsync();
            var match2 = lines.Current.First(match1.Contains);

            sum += GetPriority(match2);
        }

        return sum.ToString();
    }
}