namespace Maxwe11.Bimap.Impl.Properties
{
    class Double : IPropertyValue
    {
        public int BitsCount { get { return 64; } }

        public double TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
