using System.Net.Sockets;

namespace AdventOfCode;

internal class Day13_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile).GetAsyncEnumerator();

        await lines.MoveNextAsync();

        var pairs = 0;
        var sum = 0;

        while (lines.Current != null)
        {
            var packet1 = ParsePacket(lines.Current);
            await lines.MoveNextAsync();
            var packet2 = ParsePacket(lines.Current);

            pairs++;
            if (ComparePackets(packet1, packet2) == Order.Correct)
            {
                sum += pairs;
            }

            await lines.MoveNextAsync();
            await lines.MoveNextAsync();
        }

        return sum.ToString();
    }

    private Order ComparePackets(IData packet1, IData packet2)
    {
        if (packet1 is DataInteger integer1 && packet2 is DataInteger integer2)
        {
            if (integer1.Value < integer2.Value) return Order.Correct;
            if (integer1.Value == integer2.Value) return Order.Continue;
            if (integer1.Value > integer2.Value) return Order.Incorrect;
        }

        if (packet1 is DataList list1 && packet2 is DataList list2)
        {
            foreach (var (first, second) in list1.Zip(list2))
            {
                var result = ComparePackets(first, second);
                if (result != Order.Continue)
                {
                    return result;
                }
            }

            var count1 = list1.Count();
            var count2 = list2.Count();

            return count1 == count2 ? Order.Continue : count1 < count2 ? Order.Correct : Order.Incorrect;
        }

        if (packet1 is DataInteger && packet2 is DataList)
        {
            return ComparePackets(new DataList { packet1 }, packet2);
        }

        if (packet1 is DataList && packet2 is DataInteger)
        {
            return ComparePackets(packet1, new DataList { packet2 });
        }

        return Order.Continue;
    }

    private static IData ParsePacket(string line)
    {
        var stack = new Stack<IData>();
        var value = 0;
        var hasValue = false;

        IData data = new DataList();
        foreach (var character in line)
        {
            switch (character)
            {
                case '[':
                    var list = new DataList();

                    if (stack.TryPeek(out var peek1) && peek1 is DataList peek2)
                    {
                        peek2.Add(list);
                    }

                    stack.Push(list);
                    break;

                case ']':
                    if (hasValue && stack.Peek() is DataList list1)
                    {
                        list1.Add(new DataInteger(value));
                        value = 0;
                        hasValue = false;
                    }
                    data = stack.Pop();
                    break;

                case ',':
                    if (hasValue && stack.Peek() is DataList list2)
                    {
                        list2.Add(new DataInteger(value));
                        value = 0;
                        hasValue = false;
                    }
                    break;

                default:
                    value *= 10;
                    value += int.Parse(character.ToString());
                    hasValue = true;
                    break;
            }
        }

        return data;
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLines(inputFile);

        var packets = lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(ParsePacket).ToList();

        var divider2 = ParsePacket("[[2]]");
        packets.Add(divider2);

        var divider6 = ParsePacket("[[6]]");
        packets.Add(divider6);

        packets.Sort((x, y) => ComparePackets(x, y) == Order.Correct ? -1 : 1);

        var divider2Index = 0;
        var divider6Index = 0;
        for (var i = 0; i < packets.Count; i++)
        {
            if (packets[i] == divider2) divider2Index = i + 1;
            if (packets[i] == divider6) divider6Index = i + 1;
        }

        return (divider2Index * divider6Index).ToString();
    }
}