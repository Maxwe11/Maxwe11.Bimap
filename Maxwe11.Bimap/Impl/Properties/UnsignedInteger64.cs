namespace Maxwe11.Bimap.Impl.Properties
{
    class UnsignedInteger64 : IPropertyValue
    {
        public UnsignedInteger64(int bitsCount = 64)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public ulong TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}