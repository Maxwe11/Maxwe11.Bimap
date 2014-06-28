namespace Maxwe11.Bimap.Numbers
{
    class UnsignedInteger16 : INumber
    {
        public UnsignedInteger16(int bitsCount = 16)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public ushort TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
