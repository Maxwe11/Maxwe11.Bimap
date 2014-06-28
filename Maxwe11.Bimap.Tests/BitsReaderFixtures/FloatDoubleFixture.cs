namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class FloatDoubleFixture
    {
        [Test]
        public void ReadDoubleSuccess()
        {
            var bytes = BitConverter.GetBytes(Math.PI);
            var result = new BitsReader(bytes).ReadDouble();

            Assert.AreEqual(Math.PI, result);
        }

        [Test]
        public void ReadFloatSuccess()
        {
            const float Pi = 3.1415926f;
            var bytes = BitConverter.GetBytes(Pi);
            var result = new BitsReader(bytes).ReadFloat();

            Assert.AreEqual(Pi, result);
        }
    }
}
