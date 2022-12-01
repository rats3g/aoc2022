namespace AdventOfCode;

internal class Day1_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var max = 0;
        var current = 0;
        

        await foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                max = Math.Max(max, current);
                current = 0;
            }
            else
            {
                current += int.Parse(line);
            }
        }

        return Math.Max(max, current).ToString();
    }

public async Task<string> SolvePartTwo(string inputFile)
    {
        return "unsolved";
    }
}