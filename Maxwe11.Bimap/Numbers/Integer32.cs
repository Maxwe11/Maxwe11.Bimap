namespace Maxwe11.Bimap.Numbers
{
    class Integer32 : INumber
    {
        public Integer32(int bitsCount = 32)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public int TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
