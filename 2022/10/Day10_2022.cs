namespace AdventOfCode;

internal class Day10_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var outputCycles = new HashSet<int> 
        { 
            20, 60, 100, 140, 180, 220
        };

        var value = 1;
        var cycle = 0;

        var sum = 0;

        await foreach (var line in lines)
        {         
            if (outputCycles.Contains(++cycle))
            {                
                sum += value * cycle;
            }

            switch (line[..4])
            {
                case "addx":
                    if (outputCycles.Contains(++cycle))
                    {                        
                        sum += value * cycle;
                    }
                    value += int.Parse(line[5..]);
                    break;
                case "noop":
                    break;
                default:
                    throw new Exception("Invalid instruction type");
            }
        }

        return sum.ToString();
    }    

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var outputCycles = new HashSet<int>
        {
            20, 60, 100, 140, 180, 220
        };

        var value = 1;
        var cycle = 0;

        var sum = 0;

        await foreach (var line in lines)
        {
            if (outputCycles.Contains(++cycle))
            {
                sum += value * cycle;
            }
            CrtPrint(value, cycle);

            switch (line[..4])
            {
                case "addx":
                    if (outputCycles.Contains(++cycle))
                    {
                        sum += value * cycle;
                    }
                    CrtPrint(value, cycle);
                    value += int.Parse(line[5..]);
                    break;
                case "noop":
                    break;
                default:
                    throw new Exception("Invalid instruction type");
            }
        }

        return "Image above";
    }

    private static void CrtPrint(int value, int cycle)
    {
        var position = (cycle - 1) % 40;

        if (position == 0) Console.WriteLine();

        if (Math.Abs(value - position) < 2)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
    }
}