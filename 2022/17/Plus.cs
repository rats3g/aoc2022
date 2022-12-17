namespace AdventOfCode;

internal class Plus : Shape
{
    public Plus(World world) : base(world) 
    {
        var height = world.GetHeight();
        var additionalHeight = height + 6 - world.Length + 1; // 3 empty spaces + 3 spaces for the plus
        for (var i = 0; i < additionalHeight; i++)
        {
            world.AddRow();
        }

        position = new Position(3, height + 5); // reference position from center
    }

    public override void Commit()
    {
        world[position.y + 1][position.x] = true;

        for (var i = -1; i <= 1; i++)
        {
            world[position.y][position.x + i] = true;
        }

        world[position.y - 1][position.x] = true;
    }

    public override bool MoveDown()
    {
        if (world[position.y - 1][position.x - 1] ||
            world[position.y - 2][position.x] ||
            world[position.y - 1][position.x + 1])
        {
            return false;
        }
        
        position = new Position(position.x, position.y - 1);
        return true;
    }

    public override bool MoveLeft()
    {
        if (position.x - 1 <= 0 ||
            world[position.y + 1][position.x - 1] ||
            world[position.y][position.x - 2] ||
            world[position.y - 1][position.x - 1])
        {
            return false;
        }

        position = new Position(position.x - 1, position.y);
        return true;
    }

    public override bool MoveRight()
    {
        if (position.x + 1 >= 6 ||
            world[position.y + 1][position.x + 1] ||
            world[position.y][position.x + 2] ||
            world[position.y - 1][position.x + 1])
        {
            return false;
        }

        position = new Position(position.x + 1, position.y);
        return true;
    }
}