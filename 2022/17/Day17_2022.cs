namespace AdventOfCode;

internal class Day17_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var line = await File.ReadAllTextAsync(inputFile);

        var world = new World();
        var directions = GetJetDirections(line).GetEnumerator();

        var i = 0;
        var first = true;
        foreach (var shape in GetShapes(world).Take(2022))
        {
            if (i == 1742 && first) {
                directions.MoveNext();
                first = false;
            }

            do
            {
                directions.MoveNext();
                if (directions.Current == Direction.Left)
                {
                    shape.MoveLeft();
                }
                else
                {
                    shape.MoveRight();
                }
            } 
            while (shape.MoveDown());

            shape.Commit();
            i++;
        }

        //PrintWorld(world);

        return world.GetHeight().ToString();
    }

    private void PrintWorld(World world)
    {
        for (var i = world.Length - 1; i > 0; i--)
        {
            Console.WriteLine($"|{string.Concat(world[i].Select(x => x ? '#' : '.'))}|");
        }

        Console.WriteLine("+-------+");
    }

    private IEnumerable<Direction> GetJetDirections(string line)
    {
        var jets = line.Select(character => character == '<' ? Direction.Left : Direction.Right);

        while (true)
        {
            foreach (var jet in jets)
            {
                yield return jet;
            }
        }
    }

    private IEnumerable<Shape> GetShapes(World world)
    {
        while (true)
        {
            yield return new HorizontalLine(world);
            yield return new Plus(world);
            yield return new RightAngle(world);
            yield return new VerticalLine(world);
            yield return new Square(world);
        }
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var line = await File.ReadAllTextAsync(inputFile);

        var world = new World();
        var directions = GetJetDirections(line).GetEnumerator();

        var i = 0;
        var first = true;
        foreach (var shape in GetShapes(world).Take(2022))
        {
            if (i == 1742 && first) {
                directions.MoveNext();
                first = false;
            }

            do
            {
                directions.MoveNext();
                if (directions.Current == Direction.Left)
                {
                    shape.MoveLeft();
                }
                else
                {
                    shape.MoveRight();
                }
            } 
            while (shape.MoveDown());

            shape.Commit();
            i++;
        }

        //PrintWorld(world);

        return world.GetHeight().ToString();
    }
}