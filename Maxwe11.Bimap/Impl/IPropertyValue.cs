namespace Maxwe11.Bimap.Impl
{
    interface IPropertyValue
    {
        int BitsCount { get; }

        object Value { get; }

        void Apply(IPropertiesVisitor visitor);
    }
}
