namespace Maxwe11.Bimap.Impl.Properties
{
    class Integer64 : IPropertyValue
    {
        public Integer64(int bitsCount = 64)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public long TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
