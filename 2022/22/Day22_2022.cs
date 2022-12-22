using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day22_2022 : ISolution
{
    private record Instruction;
    private record Turn(Direction direction) : Instruction;
    private record Distance(int distance) : Instruction;

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var input = lines[..^2];
        var instructions = ParseInstructions(lines[^1]);

        var position = new Position(input[0].LastIndexOf(' ') + 1, 0);
        var direction = Direction.Right;

        var map = input.Select(line => line.ToCharArray()).ToArray();
        foreach (var instruction in instructions)
        {
            if (instruction is Turn turn)
            {
                direction = turn.direction switch
                {
                    Direction.Left => direction switch
                    {
                        Direction.Up => Direction.Left,
                        Direction.Right => Direction.Up,
                        Direction.Down => Direction.Right,
                        Direction.Left => Direction.Down
                    },

                    Direction.Right => direction switch
                    {
                        Direction.Up => Direction.Right,
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up
                    }
                };
            }
            else if (instruction is Distance distance)
            {
                for (var i = 0; i < distance.distance; i++)
                {
                    map[position.y][position.x] = direction switch
                    {
                        Direction.Up => '^',
                        Direction.Right => '>',
                        Direction.Down => 'v',
                        Direction.Left => '<'
                    };

                    var newPosition = direction switch
                    {
                        Direction.Up => position with { y = position.y - 1 },
                        Direction.Right => position with { x = position.x + 1 },
                        Direction.Down => position with { y = position.y + 1 },
                        Direction.Left => position with { x = position.x - 1 }
                    };

                    if (newPosition.y < 0 || 
                        newPosition.x < 0 || 
                        newPosition.y >= map.Length ||
                        newPosition.x >= map[newPosition.y].Length ||                             
                        map[newPosition.y][newPosition.x] == ' ')
                    {
                        newPosition = WrapAround(input, position, direction);
                    }

                    if (map[newPosition.y][newPosition.x] == '#') break;

                    position = newPosition;
                }
            }
        }

        return (1000 * (position.y + 1) + 4 * (position.x + 1) + direction switch
        {
            Direction.Up => 3,
            Direction.Right => 0,
            Direction.Down => 1,
            Direction.Left => 2
        }).ToString();
    }

    private Position WrapAround(string[] map, Position position, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                for (var i = map.Length - 1; i >= 0; i--)
                {
                    if (position.x < map[i].Length && map[i][position.x] != ' ')
                    {
                        return position with { y = i };
                    }
                }
                break;

            case Direction.Right:
                return position with { x = map[position.y].LastIndexOf(' ') + 1 };

            case Direction.Down:
                for (var i = 0; i < map.Length; i++)
                {
                    if (position.x < map[i].Length && map[i][position.x] != ' ')
                    {
                        return position with { y = i };
                    }
                }
                break;

            case Direction.Left:
                return position with { x = map[position.y].Length - 1 };
        }

        throw new Exception("Unknown direction");
    }

    private IEnumerable<Instruction> ParseInstructions(string line) =>
        Regex.Matches(line, @"(\d+|\w)")
            .Select<Match, Instruction>(
                match => int.TryParse(match.Value, out var distance) 
                    ? new Distance(distance) 
                    : new Turn(match.Value == "L" 
                        ? Direction.Left 
                        : Direction.Right));

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = await File.ReadAllLinesAsync(inputFile);

        var input = lines[..^2];
        var instructions = ParseInstructions(lines[^1]);

        var position = new Position(input[0].LastIndexOf(' ') + 1, 0);
        var direction = Direction.Right;

        var map = input.Select(line => line.ToCharArray()).ToArray();
        foreach (var instruction in instructions)
        {
            if (instruction is Turn turn)
            {
                direction = turn.direction switch
                {
                    Direction.Left => direction switch
                    {
                        Direction.Up => Direction.Left,
                        Direction.Right => Direction.Up,
                        Direction.Down => Direction.Right,
                        Direction.Left => Direction.Down
                    },

                    Direction.Right => direction switch
                    {
                        Direction.Up => Direction.Right,
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up
                    }
                };
            }
            else if (instruction is Distance distance)
            {
                for (var i = 0; i < distance.distance; i++)
                {
                    map[position.y][position.x] = direction switch
                    {
                        Direction.Up => '^',
                        Direction.Right => '>',
                        Direction.Down => 'v',
                        Direction.Left => '<'
                    };

                    var newPosition = direction switch
                    {
                        Direction.Up => position with { y = position.y - 1 },
                        Direction.Right => position with { x = position.x + 1 },
                        Direction.Down => position with { y = position.y + 1 },
                        Direction.Left => position with { x = position.x - 1 }
                    };

                    var newDirection = direction;
                    if (newPosition.y < 0 || 
                        newPosition.x < 0 || 
                        newPosition.y >= map.Length ||
                        newPosition.x >= map[newPosition.y].Length ||                             
                        map[newPosition.y][newPosition.x] == ' ')
                    {
                        (newPosition, newDirection) = WrapAround2(input, newPosition, direction);
                    }

                    try{if (map[newPosition.y][newPosition.x] == '#') break;}
                    catch{
                        var x = 0;
                    }

                    position = newPosition;
                    direction = newDirection;
                }
            }
        }

        return (1000 * (position.y + 1) + 4 * (position.x + 1) + direction switch
        {
            Direction.Up => 3,
            Direction.Right => 0,
            Direction.Down => 1,
            Direction.Left => 2
        }).ToString();
    }

    private (Position, Direction) WrapAround2(string[] map, Position position, Direction direction)
    {
        if (position.x >= 50 && position.x < 100 && position.y == -1)
        {
            return (new Position(0, position.x + 100), Direction.Right);
        }

        if (position.x >= 100 && position.x < 150 && position.y == -1)
        {
            return (new Position(position.x - 100, 199), Direction.Up);
        }

        if (position.x == 150)
        {
            return (new Position(99, 149 - position.y), Direction.Left);
        }

        if (position.x >= 100 && position.x < 150 && position.y == 50)
        {
            return (new Position(99, position.x - 50), Direction.Left);
        }

        if (position.x == 100 && position.y >= 50 && position.y < 100)
        {
            return (new Position(position.y + 50, 49), Direction.Up);
        }

        if (position.x == 100 && position.y >= 100 && position.y < 150)
        {
            return (new Position(149, Math.Abs(position.x - 149)), Direction.Left);
        }

        if (position.x >= 50 && position.x < 100 && position.y == 150)
        {
            return (new Position(49, position.x + 100), Direction.Left);
        }

        if (position.x == 50 && position.y >= 150 && position.y < 200)
        {
            return (new Position(position.y - 100, 149), Direction.Up);
        }

        if (position.x >= 0 && position.x < 50 && position.y == 200)
        {
            return (new Position(position.x + 100, 0), Direction.Down);
        }

        if (position.x == -1 && position.y >= 100 && position.y < 150)
        {
            return (new Position(50, Math.Abs(position.y - 149)), Direction.Right);
        }

        if (position.x == -1 && position.y >= 150 && position.y < 200)
        {
            
            return (new Position(position.y - 100, 0), Direction.Down);
        }

        if (position.x >= 0 && position.x < 50 && position.y == 99)
        {
            return (new Position(50, position.x + 50), Direction.Right);
        }

        if (position.x == 49 && position.y >= 50 && position.y < 100)
        {
            return (new Position(position.y - 50, 100), Direction.Down);
        }

        if (position.x == 49 && position.y >= 0 && position.y < 50)
        {
            return (new Position(0, 149 - position.y), Direction.Right);
        }

        throw new Exception("Unhandled wrapping");
    }
}