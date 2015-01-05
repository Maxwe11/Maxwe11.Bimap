namespace Maxwe11.Bimap.Impl.Properties
{
    class UnsignedInteger32 : IPropertyValue
    {
        public UnsignedInteger32(int bitsCount = 32)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public uint TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
