namespace AdventOfCode;

internal record Material(int ore, int clay, int obsidian, int geode) 
{
    public static Material operator +(Material a, Material b) =>
        new Material
            (
                a.ore + b.ore,
                a.clay + b.clay,
                a.obsidian + b.obsidian,
                a.geode + b.geode
            );

    public static Material operator -(Material a, Material b) =>
        new Material
            (
                a.ore - b.ore,
                a.clay - b.clay,
                a.obsidian - b.obsidian,
                a.geode - b.geode
            );

    public static bool operator <=(Material a, Material b) =>
        a.ore <= b.ore &&
        a.clay <= b.clay &&
        a.obsidian <= b.obsidian &&
        a.geode <= b.geode;

    public static bool operator >=(Material a, Material b) =>
        a.ore >= b.ore &&
        a.clay >= b.clay &&
        a.obsidian >= b.obsidian &&
        a.geode >= b.geode;
}