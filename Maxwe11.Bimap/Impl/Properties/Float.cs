namespace Maxwe11.Bimap.Impl.Properties
{
    class Float : IPropertyValue
    {
        public int BitsCount { get { return 32; } }

        public float TypedValue { get; set; }

        public object Value { get { return TypedValue; } }

        public void Apply(IPropertiesVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
