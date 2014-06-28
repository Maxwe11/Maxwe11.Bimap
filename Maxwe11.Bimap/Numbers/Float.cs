namespace Maxwe11.Bimap.Numbers
{
    class Float : INumber
    {
        public int BitsCount { get { return 32; } }

        public float TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(INumbersVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
