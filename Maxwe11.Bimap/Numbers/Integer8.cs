namespace Maxwe11.Bimap.Numbers
{
    class Integer8 : INumber
    {
        public Integer8(int bitsCount = 8)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public sbyte TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
