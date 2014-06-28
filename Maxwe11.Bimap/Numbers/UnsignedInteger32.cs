namespace Maxwe11.Bimap.Numbers
{
    class UnsignedInteger32 : INumber
    {
        public UnsignedInteger32(int bitsCount = 32)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public uint TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
