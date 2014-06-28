namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class ResetFixture
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResetShouldThrow()
        {
            new BitsReader(new byte[]{}).Reset(null);
        }

        [Test]
        public void ResetSuccess()
        {
            var reader = new BitsReader(new byte[] { 86, 8 });
            reader.ReadInt16(12);

            Assert.AreNotEqual(0, reader.BitsRead);

            reader.Reset(new byte[] { 240 });

            Assert.AreEqual(0, reader.BitsRead);

            reader.ReadInt8(5);
        }
    }
}
