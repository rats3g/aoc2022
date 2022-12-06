namespace AdventOfCode;

internal class Day6_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var input = await File.ReadAllTextAsync(inputFile);

        var map = new Dictionary<char, int>();

        var leftIndex = 0;
        var rightIndex = 0;
        while (rightIndex < input.Length) 
        {
            var rightChar = input[rightIndex];
            map.TryGetValue(rightChar, out var rightCount);
            map[rightChar] = rightCount + 1;

            while (map[rightChar] > 1)
            {
                map[input[leftIndex]]--;
                leftIndex++;
            }

            if (rightIndex - leftIndex + 1 >= 4)
            {
                return (rightIndex + 1).ToString();
            }

            rightIndex++;
        }

        return "failure";
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var input = await File.ReadAllTextAsync(inputFile);

        var map = new Dictionary<char, int>();

        var leftIndex = 0;
        var rightIndex = 0;
        while (rightIndex < input.Length)
        {
            var rightChar = input[rightIndex];
            map.TryGetValue(rightChar, out var rightCount);
            map[rightChar] = rightCount + 1;

            while (map[rightChar] > 1)
            {
                map[input[leftIndex]]--;
                leftIndex++;
            }

            if (rightIndex - leftIndex + 1 >= 14)
            {
                return (rightIndex + 1).ToString();
            }

            rightIndex++;
        }

        return "failure";
    }
}