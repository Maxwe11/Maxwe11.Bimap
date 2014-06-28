namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class UInt16Fixture
    {
        private readonly ushort[] mResult =
        {
            0, 1, 0, 3, 0, 7, 0, 15,
            0, 31, 0, 63, 0, 127, 0, 255,
            0, 511, 0, 1023, 0, 2047, 0, 4095,
            0, 8191, 0, 16383, 0, 32767, 0, 65535
        };

        private readonly byte[] mInput =
        {
            50, 14, 15, 62, 240, 3, 254,
            0, 255,
            0, 254, 3, 240, 63,
            0, 254, 15,
            0, 255, 15,
            0, 254, 63,
            0, 240, 255, 3,
            0, 254, 255,
            0, 0, 255, 255
        };

        [TestCase(0)]
        [TestCase(17)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadUInt16(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<ushort>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 17; i++)
            {
                result.Add(reader.ReadUInt16(i));
                result.Add(reader.ReadUInt16(i));
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
                reader.ReadUInt16(16);
        }
    }
}
