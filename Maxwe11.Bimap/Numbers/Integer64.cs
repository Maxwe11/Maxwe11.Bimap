namespace Maxwe11.Bimap.Numbers
{
    class Integer64 : INumber
    {
        public Integer64(int bitsCount = 64)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public long TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
