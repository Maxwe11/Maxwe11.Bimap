namespace Maxwe11.Bimap.Numbers
{
    class Integer16 : INumber
    {
        public Integer16(int bitsCount = 16)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public short TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
