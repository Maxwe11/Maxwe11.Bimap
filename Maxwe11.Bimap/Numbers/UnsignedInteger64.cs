namespace Maxwe11.Bimap.Numbers
{
    class UnsignedInteger64 : INumber
    {
        public UnsignedInteger64(int bitsCount = 64)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public ulong TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}