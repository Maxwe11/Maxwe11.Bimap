namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class PropertyHasPrivateSetter
    {
        [Bimap(OrderId: 5)]
        public int Foo { get; private set; }
    }
}