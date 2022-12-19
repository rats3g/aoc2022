using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day19_2022 : ISolution
{
    public record Blueprint(int id, Robot ore, Robot clay, Robot obsidian, Robot geode);
    public record Robot(Material cost, Material produces);
    public record State(int remaining, Material available, Material produced);

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);
        var blueprints = new List<Blueprint>();
        await foreach (var line in lines)
        {
            blueprints.Add(ParseBlueprint(line));
        }

        var sum = 0;

        foreach (var blueprint in blueprints)
        {
            sum += blueprint.id * MaxGeodes(blueprint, 24);
        }

        return sum.ToString();
    }

    private int MaxGeodes(Blueprint blueprint, int timeLimit) =>
        MaxGeodes(
            blueprint, 
            new State(
                timeLimit,
                new Material(ore: 0, 0, 0, 0),
                new Material(ore: 1, 0, 0, 0)
            ),
            new Dictionary<State, int>()
        );

    private int MaxGeodes(Blueprint blueprint, State state, Dictionary<State, int> cache) 
    {
        if (state.remaining == 0) {
            return state.available.geode;
        }

        if (!cache.ContainsKey(state)) 
        {
            cache[state] = NextSteps(blueprint, state)
                .Select(newState => newState with
                {
                    remaining = state.remaining - 1,
                    available = newState.available + state.produced
                })
                .Select(newState => MaxGeodes(blueprint, newState, cache))
                .Max();
        }

        return cache[state];
    }

    private static IEnumerable<State> NextSteps(Blueprint bluePrint, State state) 
    {
        var now = state.available;
        var prev = now - state.produced;

        if (!CanBuild(bluePrint.geode, prev) && CanBuild(bluePrint.geode, now)) 
        {
            yield return BuildRobot(state, bluePrint.geode);
            yield break;
        }

        if (!CanBuild(bluePrint.obsidian, prev) && CanBuild(bluePrint.obsidian, now)) 
        {
            yield return BuildRobot(state, bluePrint.obsidian);
        }

        if (!CanBuild(bluePrint.clay, prev) && CanBuild(bluePrint.clay, now)) 
        {
            yield return BuildRobot(state, bluePrint.clay);
        }

        if (!CanBuild(bluePrint.ore, prev) && CanBuild(bluePrint.ore, now)) 
        {
            yield return BuildRobot(state, bluePrint.ore);
        }

        yield return state;
    }

    private static bool CanBuild(Robot robot, Material materials) => materials >= robot.cost;

    private static State BuildRobot(State state, Robot robot) =>
        state with
        {
            available = state.available - robot.cost,
            produced = state.produced + robot.produces
        };

    private static State MineMaterials(State state, Material miners) =>
        state with
        {
            available = state.available + miners
        };

    private static Blueprint ParseBlueprint(string line)
    {
        var numbers = Regex.Matches(line, @"(\d+)").Select(x => int.Parse(x.Value)).ToArray();

        return new Blueprint
        (
            id: numbers[0],
                new Robot(
                    new Material(numbers[1], 0, 0, 0),
                    new Material(1, 0, 0, 0)
                ),
                new Robot(
                    new Material(numbers[2], 0, 0, 0),
                    new Material(0, 1, 0, 0)
                ),
                new Robot(
                    new Material(numbers[3], numbers[4], 0, 0),
                    new Material(0, 0, 1, 0)
                ),
                new Robot(
                    new Material(numbers[5], 0, numbers[6], 0),
                    new Material(0, 0, 0, 1)
                )
        );
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);
        var blueprints = new List<Blueprint>();
        await foreach (var line in lines)
        {
            blueprints.Add(ParseBlueprint(line));
        }

        var sum = 1;

        foreach (var blueprint in blueprints.Take(3))
        {
            sum *= MaxGeodes(blueprint, 32);
        }

        return sum.ToString();
    }
}