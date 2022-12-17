namespace AdventOfCode;

internal class RightAngle : Shape
{
    public RightAngle(World world) : base(world)
    {
        var height = world.GetHeight();
        var additionalHeight = height + 6 - world.Length + 1; // 3 empty spaces + 3 space for the line
        for (var i = 0; i < additionalHeight; i++)
        {
            world.AddRow();
        }

        position = new Position(4, height + 4); // reference position from corner
    }

    public override void Commit()
    {
        for (var i = -2; i < 0; i++)
        {
            world[position.y][position.x + i] = true;
        }

        world[position.y][position.x] = true;

        for (var i = 1; i <= 2; i++)
        {
            world[position.y + i][position.x] = true;
        }
    }

    public override bool MoveDown()
    {
        for (var i = -2; i <= 0; i++)
        {
            if (world[position.y - 1][position.x + i])
            {
                return false;
            }
        }

        position = new Position(position.x, position.y - 1);
        return true;
    }

    public override bool MoveLeft()
    {
        if (position.x - 2 <= 0 ||
            world[position.y][position.x - 3] ||
            world[position.y + 1][position.x - 1] ||
            world[position.y + 2][position.x - 1])
        {
            return false;
        }

        position = new Position(position.x - 1, position.y);
        return true;
    }

    public override bool MoveRight()
    {
        if (position.x >= 6) return false;

        for (var i = 0; i < 3; i++)
        {
            if (world[position.y + i][position.x + 1])
            {
                return false;
            }
        }

        position = new Position(position.x + 1, position.y);
        return true;
    }
}