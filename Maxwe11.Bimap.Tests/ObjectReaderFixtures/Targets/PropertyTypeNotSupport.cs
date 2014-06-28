namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    struct TestValueType
    {

    }

    class PropertyTypeNotSupport
    {
        [Bimap(OrderId: 5)]
        public TestValueType Foo { get; set; }
    }
}