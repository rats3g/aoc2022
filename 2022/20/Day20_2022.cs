namespace AdventOfCode;

internal class Day20_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var nodes = new List<LinkedListNode>();

        LinkedListNode? zero = null;
        LinkedListNode? head = null;
        LinkedListNode? previous = null;

        await foreach (var line in lines)
        {
            var current = new LinkedListNode(long.Parse(line));

            nodes.Add(current);

            if (current.Value == 0) zero = current;

            if (head == null) head = current;

            if (previous != null)
            {
                previous.Next = current;
                current.Previous = previous;
            }

            previous = current;
        }

        previous!.Next = head;
        head!.Previous = previous;

        foreach (var node in nodes)
        {
            RemoveNode(node);

            if (node.Value < 0)
            {
                var before = node.Next;
                for (var i = 0; i < Math.Abs(node.Value) % (nodes.Count - 1); i++)
                {
                    before = before!.Previous;
                }
                InsertBefore(node, before!);
            }
            else
            {
                var after = node.Previous;
                for (var i = 0; i < node.Value % (nodes.Count - 1); i++)
                {
                    after = after!.Next;
                }
                InsertAfter(node, after!);
            }
        }

        var output = zero;
        var sum = 0L;
        for (var i = 1; i <= 3000; i++)
        {
            output = output!.Next;
            if (i % 1000 == 0) sum += output!.Value;
        }

        return sum.ToString();
    }

    private void RemoveNode(LinkedListNode node)
    {
        node.Previous!.Next = node.Next;
        node.Next!.Previous = node.Previous;
    }

    private void InsertNode(LinkedListNode node, LinkedListNode previous, LinkedListNode next)
    {
        previous.Next = node;
        next.Previous = node;
        node.Previous = previous;
        node.Next = next;
    }

    private void InsertAfter(LinkedListNode node, LinkedListNode after)
    {
        InsertNode(node, after, after.Next!);
    }

    private void InsertBefore(LinkedListNode node, LinkedListNode before)
    {
        InsertNode(node, before.Previous!, before);
    }

    private static void PrintList(LinkedListNode head)
    {
        var output = head;
        do
        {
            Console.Write($"{output!.Value}, ");
            output = output.Next;
        }
        while (output != head);

        Console.WriteLine();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var nodes = new List<LinkedListNode>();

        LinkedListNode? zero = null;
        LinkedListNode? head = null;
        LinkedListNode? previous = null;

        await foreach (var line in lines)
        {
            var current = new LinkedListNode(long.Parse(line) * 811589153);

            nodes.Add(current);

            if (current.Value == 0) zero = current;

            if (head == null) head = current;

            if (previous != null)
            {
                previous.Next = current;
                current.Previous = previous;
            }

            previous = current;
        }

        previous!.Next = head;
        head!.Previous = previous;

        for (var iteration = 0; iteration < 10; iteration++)
        {
            foreach (var node in nodes)
            {
                RemoveNode(node);

                if (node.Value < 0)
                {
                    var before = node.Next;
                    for (var i = 0; i < Math.Abs(node.Value) % (nodes.Count - 1); i++)
                    {
                        before = before!.Previous;
                    }
                    InsertBefore(node, before!);
                }
                else
                {
                    var after = node.Previous;
                    for (var i = 0; i < node.Value % (nodes.Count - 1); i++)
                    {
                        after = after!.Next;
                    }
                    InsertAfter(node, after!);
                }
            }
        }

        var output = zero;
        var sum = 0L;
        for (var i = 1; i <= 3000; i++)
        {
            output = output!.Next;
            if (i % 1000 == 0) sum += output!.Value;
        }

        return sum.ToString();
    }
}