namespace Maxwe11.Bimap.Impl.Properties
{
    class ByteArray : IPropertyValue
    {
        public ByteArray(int length, int bitsCount)
        {
            Length = length;
            BitsCount = bitsCount;
        }

        public int Length { get; internal set; }

        public int BitsCount { get; private set; }

        public byte[] TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
