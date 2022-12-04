using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day4_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var fullyContained = 0;

        await foreach (var line in lines)
        {
            var match = Regex.Match(line, @"(?<elf1Left>\d+)-(?<elf1Right>\d+),(?<elf2Left>\d+)-(?<elf2Right>\d+)");

            var elf1Left = int.Parse(match.Groups["elf1Left"].Value);
            var elf2Left = int.Parse(match.Groups["elf2Left"].Value);
            var elf1Right = int.Parse(match.Groups["elf1Right"].Value);
            var elf2Right = int.Parse(match.Groups["elf2Right"].Value);

            if (match.Success && (elf1Left <= elf2Left && elf2Right <= elf1Right || elf2Left <= elf1Left && elf1Right <= elf2Right))
            {
                fullyContained++;
            }
        }

        return fullyContained.ToString();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var overlap = 0;

        await foreach (var line in lines)
        {
            var match = Regex.Match(line, @"(?<elf1Left>\d+)-(?<elf1Right>\d+),(?<elf2Left>\d+)-(?<elf2Right>\d+)");

            var elf1Left = int.Parse(match.Groups["elf1Left"].Value);
            var elf2Left = int.Parse(match.Groups["elf2Left"].Value);
            var elf1Right = int.Parse(match.Groups["elf1Right"].Value);
            var elf2Right = int.Parse(match.Groups["elf2Right"].Value);

            if (match.Success && (elf1Left <= elf2Left && elf2Left <= elf1Right || elf2Left <= elf1Left && elf1Left <= elf2Right))
            {
                overlap++;
            }
        }

        return overlap.ToString();
    }
}