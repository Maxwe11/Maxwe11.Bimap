namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures
{
    using Maxwe11.Bimap.Attributes;

    using NUnit.Framework;

    [TestFixture]
    public class ByteArrayFixture
    {
        private readonly byte[] mInput = { 50, 14, 15, 62, 240, 3, 254, 0, 255, 50, 14, 15, 62, 240, 3, 254, 0, 255 };

        private readonly byte[] mSpam = { 240, 3, 254, 0, 255, 50, 14, 15, 62, 240, 3, 254 };

        [Test]
        public void ReadBytesSuccessfully()
        {
            var reader = new ObjectReader<Foo>(mInput);
            var foo = new Foo();

//            reader.ForMemberArray(x => x.Spam, opt => opt.Length = 5);
            reader.Read(foo);
            
            Assert.AreEqual(1041174066, foo.Bar);
            Assert.AreEqual(mSpam, foo.Spam);
            Assert.AreEqual(65280, foo.Crc);
        }
    }

    class Foo
    {
        [Bimap(OrderId: 1)]
        public int Bar { get; set; }

        [BimapArray(OrderId: 2, Length: 12)]
        public byte[] Spam { get; set; }

        [Bimap(OrderId: 3)]
        public ushort Crc { get; set; }
    }
}
