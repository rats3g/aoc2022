namespace AdventOfCode
{
    internal class Monkey
    {
        private readonly IList<ulong> _items;
        private readonly Func<ulong, ulong> _operation;
        private readonly Func<ulong, (ulong item, int target)> _test;
        private readonly Part _part;

        public Monkey(IEnumerable<ulong> startingItems, Func<ulong, ulong> operation, Func<ulong, (ulong item, int target)> test, Part part)
        {
            _items = startingItems.ToList();
            _operation = operation;
            _test = test;
            _part = part;
        }

        public ulong Inspections { get; private set; }

        public void AddItem(ulong item)
        {
            _items.Add(item);
        }

        public IEnumerable<(ulong item, int target)> InspectAndTest()
        {
            var response = _part switch
            {
                Part.One => _items.Select(_operation).Select(item => item / 3).Select(_test).ToList(),
                Part.Two => _items.Select(_operation).Select(item => item % 9699690).Select(_test).ToList()
            };
            Inspections += (uint) _items.Count;
            _items.Clear();
            return response;
        }
    }
}
