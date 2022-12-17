namespace AdventOfCode;

internal class VerticalLine : Shape
{
    public VerticalLine(World world) : base(world)
    {
        var height = world.GetHeight();
        var additionalHeight = height + 7 - world.Length + 1; // 3 empty spaces + 4 space for the line
        for (var i = 0; i < additionalHeight; i++)
        {
            world.AddRow();
        }

        position = new Position(2, height + 4); // reference position from bottom end
    }

    public override void Commit()
    {
        for (var i = 0; i < 4; i++)
        {
            world[position.y + i][position.x] = true;
        }
    }

    public override bool MoveDown()
    {
        if (world[position.y - 1][position.x])
        {
            return false;
        }

        position = new Position(position.x, position.y - 1);
        return true;
    }

    public override bool MoveLeft()
    {
        if (position.x <= 0) return false;

        for (var i = 0; i < 4; i++)
        {
            if (world[position.y + i][position.x - 1])
            {
                return false;
            }
        }

        position = new Position(position.x - 1, position.y);
        return true;
    }

    public override bool MoveRight()
    {
        if (position.x >= 6) return false;

        for (var i = 0; i < 4; i++)
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