namespace AdventOfCode;

internal class Day9_2022 : ISolution
{
    public record struct Position(int X, int Y);

    public async Task<string> SolvePartOne(string inputFile)
    {
        var head = new Position(0, 0);
        var tail = new Position(0, 0);

        var lines = File.ReadLinesAsync(inputFile);

        var visited = new HashSet<Position>
        {
            new Position(0, 0)
        };

        await foreach (var line in lines)
        {
            var distance = int.Parse(line[2..]);
            for (var i = 0; i < distance; i++)
            {
                head = MoveHead(head, line[0]);
                tail = calculateTailMovement(head, tail);

                visited.Add(tail);
            }
        }

        return visited.Count().ToString();
    }

    private static Position MoveHead(Position head, char direction)
    {
        head = direction switch
        {
            'U' => new Position(head.X, head.Y + 1),
            'D' => new Position(head.X, head.Y - 1),
            'L' => new Position(head.X - 1, head.Y),
            'R' => new Position(head.X + 1, head.Y),
            _ => throw new Exception("Invalid direction input")
        };
        return head;
    }

    private static Position calculateTailMovement(Position head, Position tail)
    {
        if (head.Y - tail.Y > 1)
        {
            tail = (head.X - tail.X) switch
            {
                > 0 => new Position(tail.X + 1, tail.Y + 1),
                0 => new Position(tail.X, tail.Y + 1),
                < 0 => new Position(tail.X - 1, tail.Y + 1)
            };
        }
        else if (head.Y - tail.Y < -1)
        {
            tail = (head.X - tail.X) switch
            {
                > 0 => new Position(tail.X + 1, tail.Y - 1),
                0 => new Position(tail.X, tail.Y - 1),
                < 0 => new Position(tail.X - 1, tail.Y - 1)
            };
        }
        else if (head.X - tail.X > 1)
        {
            tail = (head.Y - tail.Y) switch
            {
                > 0 => new Position(tail.X + 1, tail.Y + 1),
                0 => new Position(tail.X + 1, tail.Y),
                < 0 => new Position(tail.X + 1, tail.Y - 1)
            };
        }
        else if (head.X - tail.X < -1)
        {
            tail = (head.Y - tail.Y) switch
            {
                > 0 => new Position(tail.X - 1, tail.Y + 1),
                0 => new Position(tail.X - 1, tail.Y),
                < 0 => new Position(tail.X - 1, tail.Y - 1)
            };
        }

        return tail;
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var knots = new Position[10];

        var lines = File.ReadLinesAsync(inputFile);

        var visited = new HashSet<Position>
        {
            new Position(0, 0)
        };

        await foreach (var line in lines)
        {
            var distance = int.Parse(line[2..]);
            for (var i = 0; i < distance; i++)
            {
                knots[0] = MoveHead(knots[0], line[0]);
                
                for (var j = 1; j < knots.Length; j++)
                {
                    knots[j] = calculateTailMovement(knots[j - 1], knots[j]);
                }

                visited.Add(knots[^1]);
            }
        }

        return visited.Count().ToString();
    }
}