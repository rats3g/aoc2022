namespace AdventOfCode;

internal class Day8_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var size = lines.Length;

        var visible = new bool[size - 2, size - 2];

        double max;
        for (var i = 1; i < size - 1; i++)
        {
            max = char.GetNumericValue(lines[i][0]);
            for (var j = 1; j < size - 1; j++)
            {
                if (char.GetNumericValue(lines[i][j]) > max)
                {
                    visible[i - 1, j - 1] = true;
                    max = char.GetNumericValue(lines[i][j]);
                }
            }

            max = char.GetNumericValue(lines[i][size - 1]);
            for (var j = size - 2; j > 0; j--)
            {
                if (char.GetNumericValue(lines[i][j]) > max)
                {
                    visible[i - 1, j - 1] = true;
                    max = char.GetNumericValue(lines[i][j]);
                }
            }
        }

        for (var j = 1; j < size - 1; j++)
        {
            max = char.GetNumericValue(lines[0][j]);
            for (var i = 1; i < size - 1; i++)
            {
                if (char.GetNumericValue(lines[i][j]) > max)
                {
                    visible[i - 1, j - 1] = true;
                    max = char.GetNumericValue(lines[i][j]);
                }
            }

            max = char.GetNumericValue(lines[size - 1][j]);
            for (var i = size - 2; i > 0; i--)
            {
                if (char.GetNumericValue(lines[i][j]) > max)
                {
                    visible[i - 1, j - 1] = true;
                    max = char.GetNumericValue(lines[i][j]);
                }
            }
        }

        var count = 0;
        foreach (var tree in visible)
        {
            if (tree) count++;
        }

        count += size * 2 + (size - 2) * 2;

        return count.ToString();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var size = lines.Length;

        var max = 0;

        int count;
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var score = 1;

                var current = char.GetNumericValue(lines[i][j]);
                count = 0;
                for (var up = j - 1; up > -1; up--)
                {
                    count++;
                    if (char.GetNumericValue(lines[i][up]) >= current) break;
                }
                score *= count;

                count = 0;
                for (var down = j + 1; down < size; down++)
                {
                    count++;
                    if (char.GetNumericValue(lines[i][down]) >= current) break;
                }
                score *= count;

                count = 0;
                for (var left = i - 1; left > -1; left--)
                {
                    count++;
                    if (char.GetNumericValue(lines[left][j]) >= current) break;
                }
                score *= count;

                count = 0;
                for (var right = i + 1; right < size; right++)
                {
                    count++;
                    if (char.GetNumericValue(lines[right][j]) >= current) break;
                }
                score *= count;

                max = Math.Max(score, max);
            }
        }

        return max.ToString();
    }
}