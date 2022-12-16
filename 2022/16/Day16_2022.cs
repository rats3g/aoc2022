using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Security;

namespace AdventOfCode;

internal class Day16_2022 : ISolution
{
    public record ValveInfo(string name, int id, int flow, IEnumerable<string> connections);
    public record Connection(Valve destination, int distance);
    public record Valve(string name, int id, int flow, List<Connection> connections);

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLines(inputFile);

        var valveId = 0;
        var valveInfos = lines.Select(line => ParseValve(line, valveId++)).ToDictionary(valve => valve.name, valve => valve); 

        var shortestPath = CalculateShortestPaths(valveInfos);  

        var valveCount = valveInfos.Values.Count();
        var valves = valveInfos.Values.ToDictionary(valve => valve.name, valve => new Valve(valve.name, valve.id, valve.flow, new List<Connection>()));
        var idToValve = valves.Values.ToDictionary(valve => valve.id, valve => valve);
        foreach (var valve in valves.Values)
        {
            for (var destination = 0; destination < valveCount; destination++)
            {
                if (valve.id != destination && idToValve[destination].flow != 0 && shortestPath[valve.id, destination] != int.MaxValue)
                {
                    valve.connections.Add(new Connection(idToValve[destination], shortestPath[valve.id, destination]));
                }
            }
        }

        var remaining = new HashSet<Valve>(valves.Values.Where(valve => valve.flow != 0));
        return MaxFlow(remaining, idToValve[0], 30).ToString();
    }

    private int MaxFlow(ISet<Valve> remaining, Valve valve, int flow, int time)
    {
        if (time <= 0) return flow;

        var newFlow = flow + (time - 1) * valve.flow;
        var maxFlow = newFlow;

        foreach (var connection in valve.connections)
        {
            if (remaining.Contains(connection.destination))
            {
                remaining.Remove(connection.destination);
                maxFlow = Math.Max(maxFlow, MaxFlow(remaining, connection.destination, newFlow, time - connection.distance - (valve.flow == 0 ? 0 : 1)));
                remaining.Add(connection.destination);
            }
        }

        return maxFlow;
    }

    private static int[,] CalculateShortestPaths(IDictionary<string, ValveInfo> valves)
    {
        var count = valves.Values.Count();

        var distance = new int[count, count];
        for (var i = 0; i < count; i++)
        {
            for (var j = 0; j < count; j++)
            {
                distance[i, j] = int.MaxValue;
            }
        }

        foreach (var valve in valves.Values)
        {
            foreach (var connection in valve.connections)
            {
                distance[valve.id, valves[connection].id] = 1;
            }

            distance[valve.id, valve.id] = 0;
        }

        for (var k = 0; k < count; k++)
        {
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < count; j++)
                {
                    if (distance[i, k] != int.MaxValue && distance[k, j] != int.MaxValue && distance[i, j] > distance[i, k] + distance[k, j])
                    {
                        distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }
        }

        return distance;
    }

    private static ValveInfo ParseValve(string line, int valveId)
    {
        var match = Regex.Match(line, @"Valve (?<name>\w+) has flow rate=(?<rate>\d+); tunnels? leads? to valves? (?<connections>.*)");

        return new ValveInfo(match.Groups["name"].Value, valveId, int.Parse(match.Groups["rate"].Value), match.Groups["connections"].Value.Split(',', StringSplitOptions.TrimEntries));
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLines(inputFile);

        var valveId = 0;
        var valveInfos = lines.Select(line => ParseValve(line, valveId++)).ToDictionary(valve => valve.name, valve => valve); 

        var shortestPath = CalculateShortestPaths(valveInfos);  

        var valveCount = valveInfos.Values.Count();
        var valves = valveInfos.Values.ToDictionary(valve => valve.name, valve => new Valve(valve.name, valve.id, valve.flow, new List<Connection>()));
        var idToValve = valves.Values.ToDictionary(valve => valve.id, valve => valve);
        foreach (var valve in valves.Values)
        {
            for (var destination = 0; destination < valveCount; destination++)
            {
                if (valve.id != destination && idToValve[destination].flow != 0 && shortestPath[valve.id, destination] != int.MaxValue)
                {
                    valve.connections.Add(new Connection(idToValve[destination], shortestPath[valve.id, destination]));
                }
            }
        }

        var remaining = new HashSet<Valve>(valves.Values.Where(valve => valve.flow != 0));
        return MaxFlowTogether(remaining, new Valve[] { idToValve[0], idToValve[0] }, 0, new int[] { 26, 26 }).ToString();
    }

    private static int MaxFlowTogether(ISet<Valve> remaining, Valve[] valve, int flow, int[] time)
    {
        var actor = time[0] > time[1] ? 0 : 1;

        if (time[actor] <= 0) return flow;

        var newFlow = flow + (time[actor] - 1) * valve[actor].flow;
        var maxFlow = newFlow;

        foreach (var connection in valve[actor].connections)
        {
            if (remaining.Contains(connection.destination))
            {
                remaining.Remove(connection.destination);

                var newLocations = new Valve[] 
                {
                    actor == 0 ? connection.destination : valve[0],
                    actor == 0 ? valve[1] : connection.destination
                };

                var newTimes = new int[]
                {
                    actor == 0 ? time[actor] - connection.distance - (valve[actor].flow == 0 ? 0 : 1) : time[0],
                    actor == 0 ? time[1] : time[actor] - connection.distance - (valve[actor].flow == 0 ? 0 : 1)
                };

                maxFlow = Math.Max(maxFlow, MaxFlowTogether(remaining, newLocations, newFlow, newTimes));
                remaining.Add(connection.destination);
            }
        }

        return maxFlow;
    }
}