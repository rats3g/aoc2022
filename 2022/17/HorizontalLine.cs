namespace AdventOfCode;

internal class HorizontalLine : Shape
{
    public HorizontalLine(World world) : base(world) 
    {
        var height = world.GetHeight();
        var additionalHeight = height + 4 - world.Length + 1; // 3 empty spaces + 1 space for the line
        for (var i = 0; i < additionalHeight; i++)
        {
            world.AddRow();
        }

        position = new Position(2, height + 4); // reference position from left end
    }

    public override void Commit()
    {
        for (var i = 0; i < 4; i++)
        {
            world[position.y][position.x + i] = true;
        }
    }

    public override bool MoveDown()
    {
        for (var i = 0; i < 4; i++)
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
        if (position.x <= 0 || world[position.y][position.x - 1]) {
            return false;
        }
        
        position = new Position(position.x - 1, position.y);
        return true;
    }

    public override bool MoveRight()
    {
        if (position.x + 3 >= 6 || world[position.y][position.x + 4]) {
            return false;
        }

        position = new Position(position.x + 1, position.y);
        return true;
    }    
}