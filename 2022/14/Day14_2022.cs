namespace AdventOfCode;

internal class Day14_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var cave = new bool[500, 1000];

        await foreach (var line in lines)
        {
            var coordinates = line.Split(" -> ", StringSplitOptions.TrimEntries);
            var previousCoordinate = ParseCoordinate(coordinates[0]);
            foreach (var coordinate in coordinates.Skip(1))
            {
                var currentCoordinate = ParseCoordinate(coordinate);
                if (previousCoordinate.x == currentCoordinate.x)
                {
                    var startingY = Math.Min(previousCoordinate.y, currentCoordinate.y);
                    for (var i = 0; i < Math.Abs(previousCoordinate.y - currentCoordinate.y) + 1; i++)
                    {
                        cave[startingY + i, previousCoordinate.x] = true;
                    }
                }
                else
                {
                    var startingX = Math.Min(previousCoordinate.x, currentCoordinate.x);
                    for (var i = 0; i < Math.Abs(previousCoordinate.x - currentCoordinate.x) + 1; i++)
                    {
                        cave[previousCoordinate.y, startingX + i] = true;
                    }
                }

                previousCoordinate = currentCoordinate;
            }
        }

        var count = 0;
        while (DropSand(cave))
        {
            count++;
        }

        return count.ToString();
    }

    private static bool DropSand(bool[,] cave)
    {
        if (cave[0, 500]) return false;

        var position = (x: 500, y: 0);

        while (position.y < 499)
        {
            if (!cave[position.y + 1, position.x])
            {
                position = (position.x, position.y + 1);
            }
            else if (!cave[position.y + 1, position.x - 1])
            {
                position = (position.x - 1, position.y + 1);
            }
            else if (!cave[position.y + 1, position.x + 1])
            {
                position = (position.x + 1, position.y + 1);
            }
            else
            {
                cave[position.y, position.x] = true;
                return true;
            }
        }

        return false;
    }

    private static (int x, int y) ParseCoordinate(string coordinate)
    {
        var pair = coordinate.Split(',');
        return (int.Parse(pair[0]), int.Parse(pair[1]));
    }
    
    private static void PrintCave(bool[,] cave, int minX, int maxX, int minY, int maxY)
    {
        var xRange = Enumerable.Range(minX, maxX - minX + 1);
        var yRange = Enumerable.Range(minY, maxY - minY + 1);

        Console.WriteLine($"\t{string.Join("", xRange.Select(x => x / 100))}");
        Console.WriteLine($"\t{string.Join("", xRange.Select(x => x % 100 / 10))}");
        Console.WriteLine($"\t{string.Join("", xRange.Select(x => x % 10))}");

        foreach (var y in yRange)
        {
            Console.WriteLine($"{y / 100}{y % 100 / 10}{y % 10}\t{string.Join("", xRange.Select(x => cave[y, x] ? '#' : '.'))}");            
        }
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var cave = new bool[500, 1000];

        var lowestY = 0;
        await foreach (var line in lines)
        {
            var coordinates = line.Split(" -> ", StringSplitOptions.TrimEntries);
            var previousCoordinate = ParseCoordinate(coordinates[0]);

            lowestY = Math.Max(lowestY, previousCoordinate.y);

            foreach (var coordinate in coordinates.Skip(1))
            {
                var currentCoordinate = ParseCoordinate(coordinate);

                lowestY = Math.Max(lowestY, currentCoordinate.y);

                if (previousCoordinate.x == currentCoordinate.x)
                {
                    var startingY = Math.Min(previousCoordinate.y, currentCoordinate.y);
                    for (var i = 0; i < Math.Abs(previousCoordinate.y - currentCoordinate.y) + 1; i++)
                    {
                        cave[startingY + i, previousCoordinate.x] = true;
                    }
                }
                else
                {
                    var startingX = Math.Min(previousCoordinate.x, currentCoordinate.x);
                    for (var i = 0; i < Math.Abs(previousCoordinate.x - currentCoordinate.x) + 1; i++)
                    {
                        cave[previousCoordinate.y, startingX + i] = true;
                    }
                }

                previousCoordinate = currentCoordinate;
            }
        }

        for (var i = 0; i < 1000; i++)
        {
            cave[lowestY + 2, i] = true;
        }

        var count = 0;
        while (DropSand(cave))
        {
            count++;
        }

        return count.ToString();
    }
}