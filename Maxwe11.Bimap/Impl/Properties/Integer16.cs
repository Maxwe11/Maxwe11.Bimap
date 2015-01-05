namespace Maxwe11.Bimap.Impl.Properties
{
    class Integer16 : IPropertyValue
    {
        public Integer16(int bitsCount = 16)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public short TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
