namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class DoublePropertyHasNonDefaultBitsCount
    {
        [Bimap(OrderId: 1, BitsCount: 32)]
        public double Foo { get; set; }
    }
}