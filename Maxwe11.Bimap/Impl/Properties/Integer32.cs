namespace Maxwe11.Bimap.Impl.Properties
{
    class Integer32 : IPropertyValue
    {
        public Integer32(int bitsCount = 32)
        {
            BitsCount = bitsCount;
        }

        public int BitsCount { get; private set; }

        public int TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
