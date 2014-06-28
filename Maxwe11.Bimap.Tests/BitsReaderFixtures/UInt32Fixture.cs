namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class UInt32Fixture
    {
        private readonly uint[] mResult =
        {
            0, 1, 0, 3, 0, 7, 0, 15,
            0, 31, 0, 63, 0, 127, 0, 255,
            0, 511, 0, 1023, 0, 2047, 0, 4095,
            0, 8191, 0, 16383, 0, 32767, 0, 65535,
            0, 131071, 0, 262143, 0, 524287, 0, 1048575,
            0, 2097151, 0, 4194303, 0, 8388607, 0, 16777215,
            0, 33554431, 0, 67108863, 0, 134217727, 0, 268435455,
            0, 536870911, 0, 1073741823, 0, 2147483647, 0, 4294967295
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
            0, 0, 255, 255,
            0, 0, 254, 255, 3,
            0, 240, 255, 63,
            0, 0, 254, 255, 15,
            0, 0, 255, 255, 15,
            0, 0, 254, 255, 63,
            0, 0, 240, 255, 255, 3,
            0, 0, 254, 255, 255,
            0, 0, 0, 255, 255, 255,
            0, 0, 0, 254, 255, 255, 3,
            0, 0, 240, 255, 255, 63,
            0, 0, 0, 254, 255, 255, 15,
            0, 0, 0, 255, 255, 255, 15,
            0, 0, 0, 254, 255, 255, 63,
            0, 0, 0, 240, 255, 255, 255, 3,
            0, 0, 0, 254, 255, 255, 255,
            0, 0, 0, 0, 255, 255, 255, 255
        };

        [TestCase(0)]
        [TestCase(33)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadUInt32(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<uint>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 33; i++)
            {
                result.Add(reader.ReadUInt32(i));
                result.Add(reader.ReadUInt32(i));
            }

            Assert.AreEqual(mResult, result.ToArray());
        }

        [TestCase(new byte[0])]
        [TestCase(new byte[] { 1, 255, 0, 128 })]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadShouldThrow(byte[] input)
        {
            var reader = new BitsReader(input);
            for (int i = 0; i < 2; ++i)
                reader.ReadUInt32(32);
        }
    }
}
