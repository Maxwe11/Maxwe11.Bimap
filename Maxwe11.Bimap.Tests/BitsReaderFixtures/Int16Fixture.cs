namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class Int16Fixture
    {
        private readonly short[] mResult =
        {
            -1, 0, -2, 1, -4, 3, -8, 7,
            -16, 15, -32, 31, -64, 63, -128, 127,
            -256, 255, -512, 511, -1024, 1023, -2048, 2047,
            -4096, 4095, -8192, 8191, -16384, 16383, -32768, 32767
        };

        private readonly byte[] mInput =
        {
            25, 135, 7, 31, 248, 1, 127, 128, 127,
            0, 255, 1, 248, 31,
            0, 255, 7, 128, 255, 7,
            0, 255, 31,
            0, 248, 255, 1,
            0, 255, 127,
            0, 128, 255, 127
        };

        [TestCase(0)]
        [TestCase(17)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadInt16(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<short>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 17; i++)
            {
                result.Add(reader.ReadInt16(i));
                result.Add(reader.ReadInt16(i));
            }

            Assert.AreEqual(mResult, result.ToArray());
        }

        [TestCase(new byte[0])]
        [TestCase(new byte[] { 1, 255 })]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadShouldThrow(byte[] input)
        {
            var reader = new BitsReader(input);
            for (int i = 0; i < 2; ++i)
                reader.ReadInt16(16);
        }
    }
}
