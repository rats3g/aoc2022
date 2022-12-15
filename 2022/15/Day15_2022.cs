using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day15_2022 : ISolution
{
    public record struct Position(int x, int y);
    public record struct Sensor(Position position, int distance);

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);
        var targetRow = 2000000;

        var coverage = new HashSet<Position>();
        var beacons = new List<Position>();
        await foreach (var line in lines) 
        {
            var (sensor, beacon) = ParseSensorBeacon(line);

            beacons.Add(beacon);

            var distance = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
            var remainder = distance - Math.Abs(sensor.y - targetRow);

            for (var i = -remainder; i <= remainder; i++) 
            {
                coverage.Add(new Position(sensor.x + i, targetRow));
            }
        }

        foreach (var beacon in beacons)
        {
            coverage.Remove(beacon);
        }

        return coverage.Count().ToString();
    }

    private (Position sensor, Position beacon) ParseSensorBeacon(string line)
    {
        var match = Regex.Match(line, @"Sensor at x=(?<sensorX>-?\d+), y=(?<sensorY>-?\d+): closest beacon is at x=(?<beaconX>-?\d+), y=(?<beaconY>-?\d+)");

        var sensor = new Position(int.Parse(match.Groups["sensorX"].Value), int.Parse(match.Groups["sensorY"].Value));
        var beacon = new Position(int.Parse(match.Groups["beaconX"].Value), int.Parse(match.Groups["beaconY"].Value));

        return (sensor, beacon);
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);
        var max = 4000000;

        var options = new HashSet<Position>();
        var sensors = new List<Sensor>();
        await foreach (var line in lines) 
        {
            var (sensor, beacon) = ParseSensorBeacon(line);

            var distance = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);

            sensors.Add(new Sensor(sensor, distance));

            for (var offsetY = -(distance + 1); offsetY <= distance + 1; offsetY++)
            {
                var offsetX = distance + 1 - Math.Abs(offsetY);

                var newY = sensor.y + offsetY;
                if (newY >= 0 && newY <= max) 
                {
                    var leftX = sensor.x - offsetX;
                    var rightX = sensor.x + offsetX;

                    if (leftX >= 0 && leftX <= max) 
                    {
                        options.Add(new Position(leftX, newY));
                    }

                    if (rightX >= 0 && rightX <= max) 
                    {
                        options.Add(new Position(rightX, newY));
                    }
                }
            }        
        }

        var result = options
            .First(option => 
                sensors.All(sensor => Math.Abs(sensor.position.x - option.x) + Math.Abs(sensor.position.y - option.y) > sensor.distance));

        return (4000000ul * (ulong) result.x + (ulong) result.y).ToString();
    }
}