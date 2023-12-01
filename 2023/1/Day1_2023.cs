using System.Text.RegularExpressions;

namespace AdventOfCode;

internal class Day1_2023 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var regex = new Regex(@"(?<tens>\d).*(?<ones>\d)|(?<ones>\d)", RegexOptions.Compiled);

        var lines = File.ReadLinesAsync(inputFile);

        var sum = 0;
        // await foreach (var line in lines) {
        //     var match = regex.Match(line);
        //     var tens = match.Groups["tens"].Value;
        //     var ones = match.Groups["ones"].Value;
        //     var digits = $"{(tens == "" ? ones : tens)}{ones}";
        //     var value = int.Parse(digits);
        //     sum += value;
        // }

        return sum.ToString();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var regex = new Regex(@"(?<tens>\d|one|two|three|four|five|six|seven|eight|nine).*(?<ones>\d|one|two|three|four|five|six|seven|eight|nine)|(?<ones>\d|one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Compiled);

        var lines = File.ReadLinesAsync(inputFile);

        var sum = 0;
        await foreach (var line in lines) {
            var match = regex.Match(line);
            var tens = match.Groups["tens"].Value;
            var ones = match.Groups["ones"].Value;
            var digits = $"{(tens == "" ? ones : tens)}{ones}";

            digits = digits.Replace("one", "1");
            digits = digits.Replace("two", "2");
            digits = digits.Replace("three", "3");
            digits = digits.Replace("four", "4");
            digits = digits.Replace("five", "5");
            digits = digits.Replace("six", "6");
            digits = digits.Replace("seven", "7");
            digits = digits.Replace("eight", "8");
            digits = digits.Replace("nine", "9");

            var value = int.Parse(digits);
            sum += value;
        }

        return sum.ToString();
    }
}