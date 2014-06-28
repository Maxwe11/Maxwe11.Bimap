namespace Maxwe11.Bimap.Numbers
{
    class Double : INumber
    {
        public int BitsCount { get { return 64; } }

        public double TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
