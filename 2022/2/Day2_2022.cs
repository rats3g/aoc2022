namespace AdventOfCode;

internal class Day2_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lookup = new Dictionary<(char, char), int>
        {
            { ('A', 'X'), 4 }, // Rock-Rock tie
            { ('B', 'X'), 1 }, // Paper-Rock loss
            { ('C', 'X'), 7 }, // Scissors-Rock win
            { ('A', 'Y'), 8 }, // Rock-Paper win
            { ('B', 'Y'), 5 }, // Paper-Paper tie
            { ('C', 'Y'), 2 }, // Scissors-Paper loss
            { ('A', 'Z'), 3 }, // Rock-Scissors loss
            { ('B', 'Z'), 9 }, // Paper-Scissors win
            { ('C', 'Z'), 6 }, // Scissors-Scissors tie
        };

        return File.ReadLines(inputFile).Aggregate(0, 
            (currentScore, line) => currentScore + lookup[(line[0], line[2])]).ToString();
    }

public async Task<string> SolvePartTwo(string inputFile)
    {
        var lookup = new Dictionary<(char, char), int>
        {
            { ('A', 'X'), 3 }, // Rock-Scissors loss
            { ('B', 'X'), 1 }, // Paper-Rock lose
            { ('C', 'X'), 2 }, // Scissors-Paper lose
            { ('A', 'Y'), 4 }, // Rock-Rock tie
            { ('B', 'Y'), 5 }, // Paper-Paper tie
            { ('C', 'Y'), 6 }, // Scissors-Scissors tie
            { ('A', 'Z'), 8 }, // Rock-Paper win
            { ('B', 'Z'), 9 }, // Paper-Scissors win
            { ('C', 'Z'), 7 }, // Scissors-Rock win
        };

        return File.ReadLines(inputFile).Aggregate(0,
            (currentScore, line) => currentScore + lookup[(line[0], line[2])]).ToString();
    }
}