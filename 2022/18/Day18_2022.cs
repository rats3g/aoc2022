namespace AdventOfCode;

internal class Day18_2022 : ISolution
{
    public record struct Cube(int x, int y, int z);

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var cubes = new HashSet<Cube>();

        await foreach (var line in lines)
        {
            var coordinates = line.Split(',');

            cubes.Add(new Cube(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2])));
        }

        var surfaceArea = 0;
        foreach (var cube in cubes)
        {
            if (!cubes.Contains(new Cube(cube.x + 1, cube.y, cube.z))) surfaceArea++;
            if (!cubes.Contains(new Cube(cube.x - 1, cube.y, cube.z))) surfaceArea++;
            if (!cubes.Contains(new Cube(cube.x, cube.y + 1, cube.z))) surfaceArea++;
            if (!cubes.Contains(new Cube(cube.x, cube.y - 1, cube.z))) surfaceArea++;
            if (!cubes.Contains(new Cube(cube.x, cube.y, cube.z + 1))) surfaceArea++;
            if (!cubes.Contains(new Cube(cube.x, cube.y, cube.z - 1))) surfaceArea++;
        }

        return surfaceArea.ToString();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var cubes = new HashSet<Cube>();

        var minimumCube = new Cube(int.MaxValue, int.MaxValue, int.MaxValue);
        var maximumCube = new Cube(int.MinValue, int.MinValue, int.MinValue);

        await foreach (var line in lines)
        {
            var coordinates = line.Split(',');

            var cube = new Cube(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));

            if (cube.x > maximumCube.x)
            {
                maximumCube = new Cube(cube.x, maximumCube.y, maximumCube.z);
            }

            if (cube.x < minimumCube.x)
            {
                minimumCube = new Cube(cube.x, minimumCube.y, minimumCube.z);
            }

            if (cube.y > maximumCube.y)
            {
                maximumCube = new Cube(maximumCube.x, cube.y, maximumCube.z);
            }

            if (cube.y < minimumCube.y)
            {
                minimumCube = new Cube(minimumCube.x, cube.y, minimumCube.z);
            }

            if (cube.z > maximumCube.z)
            {
                maximumCube = new Cube(maximumCube.x, maximumCube.y, cube.z);
            }

            if (cube.z < minimumCube.z)
            {
                minimumCube = new Cube(minimumCube.x, minimumCube.y, cube.z);
            }

            cubes.Add(cube);
        }

        Console.WriteLine($"{minimumCube.x}, {minimumCube.y}, {minimumCube.z}");
        Console.WriteLine($"{maximumCube.x}, {maximumCube.y}, {maximumCube.z}");

        var steam = FloodFill(cubes, minimumCube, maximumCube);

        var surfaceArea = 0;
        foreach (var cube in cubes)
        {
            if (steam.Contains(new Cube(cube.x + 1, cube.y, cube.z))) surfaceArea++;
            if (steam.Contains(new Cube(cube.x - 1, cube.y, cube.z))) surfaceArea++;
            if (steam.Contains(new Cube(cube.x, cube.y + 1, cube.z))) surfaceArea++;
            if (steam.Contains(new Cube(cube.x, cube.y - 1, cube.z))) surfaceArea++;
            if (steam.Contains(new Cube(cube.x, cube.y, cube.z + 1))) surfaceArea++;
            if (steam.Contains(new Cube(cube.x, cube.y, cube.z - 1))) surfaceArea++;
        }
        
        return surfaceArea.ToString();
    }

    private ISet<Cube> FloodFill(ISet<Cube> cubes, Cube minimum, Cube maximum)
    {
        var reached = new HashSet<Cube>();
        var queue = new Queue<Cube>();

        queue.Enqueue(new Cube(minimum.x - 1, minimum.y - 1, minimum.z - 1));

        while (queue.Count > 0)
        {
            var coordinates = queue.Dequeue();

            var left = new Cube(coordinates.x - 1, coordinates.y, coordinates.z);
            if (!cubes.Contains(left) && !reached.Contains(left) && coordinates.x > minimum.x - 1)
            {
                reached.Add(left);
                queue.Enqueue(left);
            } 

            var right = new Cube(coordinates.x + 1, coordinates.y, coordinates.z);
            if (!cubes.Contains(right) && !reached.Contains(right) && coordinates.x < maximum.x + 1) 
            {
                reached.Add(right);
                queue.Enqueue(right);
            }

            var back = new Cube(coordinates.x, coordinates.y - 1, coordinates.z);
            if (!cubes.Contains(back) && !reached.Contains(back) && coordinates.y > minimum.y - 1) 
            {
                reached.Add(back);
                queue.Enqueue(back);
            }

            var forward = new Cube(coordinates.x, coordinates.y + 1, coordinates.z);
            if (!cubes.Contains(forward) && !reached.Contains(forward) && coordinates.y < maximum.y + 1)
            {
                reached.Add(forward);
                queue.Enqueue(forward);
            } 

            var down = new Cube(coordinates.x, coordinates.y, coordinates.z - 1);
            if (!cubes.Contains(down) && !reached.Contains(down) && coordinates.z > minimum.z - 1) 
            {
                reached.Add(down);
                queue.Enqueue(down);
            }

            var up = new Cube(coordinates.x, coordinates.y, coordinates.z + 1);
            if (!cubes.Contains(up) && !reached.Contains(up) && coordinates.z < maximum.z + 1) 
            {

                reached.Add(up);
                queue.Enqueue(up);
            }
        }

        return reached;
    }
}