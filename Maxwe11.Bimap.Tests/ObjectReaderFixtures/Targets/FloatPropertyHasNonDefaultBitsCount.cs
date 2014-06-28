namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class FloatPropertyHasNonDefaultBitsCount
    {
        [Bimap(OrderId: 1, BitsCount: 32)]
        public float Foo { get; set; }
    }
}