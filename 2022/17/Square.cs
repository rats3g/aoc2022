namespace AdventOfCode;

internal class Square : Shape
{
    public Square(World world) : base(world)
    {
        var height = world.GetHeight();
        var additionalHeight = height + 5 - world.Length + 1; // 3 empty spaces + 2 space for the line
        for (var i = 0; i < additionalHeight; i++)
        {
            world.AddRow();
        }

        position = new Position(2, height + 4); // reference position from bottom-left corner
    }

    public override void Commit()
    {
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                world[position.y + i][position.x + j] = true;
            }
        }
    }

    public override bool MoveDown()
    {
        for (var i = 0; i < 2; i++)
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
        if (position.x <= 0) return false;

        for (var i = 0; i < 2; i++)
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
        if (position.x + 1 >= 6) return false;

        for (var i = 0; i < 2; i++)
        {
            if (world[position.y + i][position.x + 2])
            {
                return false;
            }
        }

        position = new Position(position.x + 1, position.y);
        return true;
    }
}