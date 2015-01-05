namespace Maxwe11.Bimap.Impl.Properties
{
    class Integer8 : IPropertyValue
    {
        public Integer8(int bitsCount = 8)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public sbyte TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
