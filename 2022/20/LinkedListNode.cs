namespace AdventOfCode;

internal class LinkedListNode
{
    public LinkedListNode(long value)
    {
        Value = value;
    }

    public LinkedListNode? Previous { get; set; }

    public LinkedListNode? Next { get; set; }

    public long Value { get; }
}