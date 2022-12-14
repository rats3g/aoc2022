using System.Collections;

namespace AdventOfCode
{
    internal class DataList : IData, IEnumerable<IData>
    {
        private readonly ICollection<IData> _data;

        public DataList()
        {
            _data = new List<IData>();
        }

        public void Add(IData data) => _data.Add(data);

        public IEnumerator<IData> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();

        public override string ToString()
        {
            return $"[{string.Join(',', _data)}]";
        }
    }
}
