namespace AdventOfCode;

internal abstract class Shape
{
    protected readonly World world;

    protected Position position;

    protected Shape(World world)
    {
        this.world = world;
    }

    public abstract bool MoveLeft();

    public abstract bool MoveRight();

    public abstract bool MoveDown();

    public abstract void Commit();
}