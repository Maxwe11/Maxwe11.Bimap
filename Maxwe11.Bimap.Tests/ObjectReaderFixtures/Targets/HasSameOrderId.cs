namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets
{
    using Maxwe11.Bimap.Attributes;

    class HasSameOrderId
    {
        [Bimap(OrderId: 5)]
        public int Foo { get; set; }

        [Bimap(OrderId: 5)]
        public int Bar { get; set; }
    }
}