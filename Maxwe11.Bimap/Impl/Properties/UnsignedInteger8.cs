namespace Maxwe11.Bimap.Impl.Properties
{
    class UnsignedInteger8 : IPropertyValue
    {
        public UnsignedInteger8(int bitsCount = 8)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public byte TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
