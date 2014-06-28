namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class UInt8Fixture
    {
        private readonly byte[] mResult = { 0, 1, 0, 3, 0, 7, 0, 15, 0, 31, 0, 63, 0, 127, 0, 255 };

        private readonly byte[] mInput = { 50, 14, 15, 62, 240, 3, 254, 0, 255 };

        [TestCase(0)]
        [TestCase(9)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadUInt8(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<byte>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 9; i++)
            {
                result.Add(reader.ReadUInt8(i));
                result.Add(reader.ReadUInt8(i));
            }

            Assert.AreEqual(mResult, result.ToArray());
        }

        [TestCase(new byte[0])]
        [TestCase(new byte[] { 1 })]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadShouldThrow(byte[] input)
        {
            var reader = new BitsReader(input);
            for (int i = 0; i < 2; ++i)
                reader.ReadUInt8(8);
        }
    }
}
