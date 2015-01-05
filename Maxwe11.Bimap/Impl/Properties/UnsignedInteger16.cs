namespace Maxwe11.Bimap.Impl.Properties
{
    class UnsignedInteger16 : IPropertyValue
    {
        public UnsignedInteger16(int bitsCount = 16)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public ushort TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
