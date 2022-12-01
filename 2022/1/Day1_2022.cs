namespace AdventOfCode;

internal class Day1_2022 : ISolution
{
    private static readonly Random _random = new();

    public async Task<string> SolvePartOne(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var max = 0;
        var current = 0;        

        await foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                max = Math.Max(max, current);
                current = 0;
            }
            else
            {
                current += int.Parse(line);
            }
        }

        return Math.Max(max, current).ToString();
    }

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var lines = File.ReadLinesAsync(inputFile);

        var elves = new List<int>();
        var current = 0;

        await foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                elves.Add(current);
                current = 0;
            }
            else
            {
                current += int.Parse(line);
            }
        }

        elves.Add(current);

        return QuickSelect(elves, 3).Sum().ToString();
    }

    // https://en.wikipedia.org/wiki/Quickselect provides O(n) average performance compared to O(nlog(n)) with QuickSort
    private IEnumerable<int> QuickSelect(IList<int> items, int k)
    {
        QuickSelectHelper(items, 0, items.Count - 1, k);
        return items.Take(k);
    }

    private int QuickSelectHelper(IList<int> items, int leftIndex, int rightIndex, int k)
    {
        if (leftIndex == rightIndex)
        {
            return items[leftIndex];
        }

        var pivotIndex = leftIndex + _random.Next() % (rightIndex - leftIndex + 1);

        pivotIndex = Partition(items, leftIndex, rightIndex, pivotIndex);

        if (pivotIndex == k)
        {
            return items[k];
        }
        else if (k < pivotIndex)
        {
            return QuickSelectHelper(items, leftIndex, pivotIndex - 1, k);
        }
        else
        {
            return QuickSelectHelper(items, pivotIndex + 1, rightIndex, k);
        }
    }

    private static int Partition(IList<int> items, int leftIndex, int rightIndex, int pivotIndex)
    {
        var pivotValue = items[pivotIndex];

        Swap(items, pivotIndex, rightIndex);

        var storeIndex = leftIndex;

        for (int i = leftIndex; i < rightIndex; i++)
        {
            if (items[i] > pivotValue)
            {
                Swap(items, storeIndex, i);
                storeIndex++;
            }
        }

        Swap(items, rightIndex, storeIndex);

        return storeIndex;
    }

    private static void Swap(IList<int> items, int leftIndex, int rightIndex)
    {
        (items[rightIndex], items[leftIndex]) = (items[leftIndex], items[rightIndex]);
    }
}