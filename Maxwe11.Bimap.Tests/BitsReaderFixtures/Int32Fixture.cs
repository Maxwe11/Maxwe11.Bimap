namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class Int32Fixture
    {
        private readonly int[] mResult =
        {
            -1, 0, -2, 1, -4, 3, -8, 7,
            -16, 15, -32, 31, -64, 63, -128, 127,
            -256, 255, -512, 511, -1024, 1023, -2048, 2047,
            -4096, 4095, -8192, 8191, -16384, 16383, -32768, 32767,
            -65536, 65535, -131072, 131071, -262144, 262143, -524288, 524287,
            -1048576, 1048575, -2097152, 2097151, -4194304, 4194303, -8388608, 8388607,
            -16777216, 16777215, -33554432, 33554431, -67108864, 67108863, -134217728, 134217727,
            -268435456, 268435455, -536870912, 536870911, -1073741824, 1073741823, -2147483648, 2147483647
        };

        private readonly byte[] mInput =
        {
            25, 135, 7, 31, 248, 1, 127, 128, 127,
            0, 255, 1, 248, 31,
            0, 255, 7, 128, 255, 7,
            0, 255, 31,
            0, 248, 255, 1,
            0, 255, 127,
            0, 128, 255, 127,
            0, 0, 255, 255, 1,
            0, 248, 255, 31,
            0, 0, 255, 255, 7,
            0, 128, 255, 255, 7,
            0, 0, 255, 255, 31,
            0, 0, 248, 255, 255, 1,
            0, 0, 255, 255, 127,
            0, 0, 128, 255, 255, 127,
            0, 0, 0, 255, 255, 255, 1,
            0, 0, 248, 255, 255, 31,
            0, 0, 0, 255, 255, 255, 7,
            0, 0, 128, 255, 255, 255, 7,
            0, 0, 0, 255, 255, 255, 31,
            0, 0, 0, 248, 255, 255, 255, 1,
            0, 0, 0, 255, 255, 255, 127,
            0, 0, 0, 128, 255, 255, 255, 127
        };

        [TestCase(0)]
        [TestCase(33)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void EnsureBitsCountShouldThrow(int bitsCount)
        {
            new BitsReader(mInput).ReadInt32(bitsCount);
        }

        [Test]
        public void ReadBytesSuccessfully()
        {
            var result = new List<int>(mResult.Length);
            var reader = new BitsReader(mInput);

            for (int i = 1; i < 33; i++)
            {
                result.Add(reader.ReadInt32(i));
                result.Add(reader.ReadInt32(i));
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
                reader.ReadInt32(32);
        }
    }
}
