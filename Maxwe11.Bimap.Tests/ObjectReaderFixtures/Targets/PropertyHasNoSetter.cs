namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class PropertyHasNoSetter
    {
        [Bimap(OrderId: 5)]
        public int Foo { get { return 5; } }
    }
}