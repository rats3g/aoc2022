namespace AdventOfCode
{
    internal class DataInteger : IData
    {
        public DataInteger(int value)
        {
            Value = value;
        }

        public int Value { get;}

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
