namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class Int8Fixture
    {
        private readonly sbyte[] mResult = { -1, 0, -2, 1, -4, 3, -8, 7, -16, 15, -32, 31, -64, 63, -128, 127 };

        private readonly byte[] mInput = { 25, 135, 7, 31, 248, 1, 127, 128, 127 };

        [TestCase(0)]
        [TestCase(9)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadInt8(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<sbyte>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 9; i++)
            {
                result.Add(reader.ReadInt8(i));
                result.Add(reader.ReadInt8(i));
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
                reader.ReadInt8(8);
        }
    }
}
