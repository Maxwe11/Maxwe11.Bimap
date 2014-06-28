namespace Maxwe11.Bimap.Tests.BitsReaderFixtures
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class ConstructorFixture
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrow()
        {
            new BitsReader(null);
        }
    }
}
