namespace Maxwe11.Bimap.Numbers
{
    class UnsignedInteger8 : INumber
    {
        public UnsignedInteger8(int bitsCount = 8)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public byte TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
