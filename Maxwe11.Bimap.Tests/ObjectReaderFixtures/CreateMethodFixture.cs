namespace Maxwe11.Bimap.Tests.ObjectReaderFixtures
{
    using System;
    using System.Reflection;

    using Maxwe11.Bimap.Tests.ObjectReaderFixtures.Targets;

    using NUnit.Framework;

    [TestFixture]
    public class CreateMethodFixture
    {
        [TestCase(typeof(Empty), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Target type \'Empty\' has no public instance properties")]
        [TestCase(typeof(HasNoPublicInstanceProperty), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Target type \'HasNoPublicInstanceProperty\' has no public instance properties")]
        [TestCase(typeof(HasNoMappedProperty), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Target type \'HasNoMappedProperty\' has no properties with \'BimapAttribute\' attribute")]
        [TestCase(typeof(HasSameOrderId), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Properties \'Foo\' and \'Bar\' has same order id")]
        [TestCase(typeof(PropertyHasNoSetter), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Property \'Foo\' that you want to map has no accessible setter")]
        [TestCase(typeof(PropertyHasPrivateSetter), ExpectedException = typeof(ArgumentException), ExpectedMessage = "Property \'Foo\' that you want to map has no accessible setter")]
        public void CreateShouldThrow(Type target)
        {
            var type = typeof(ObjectReader);
            var method = type.GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
            method = method.MakeGenericMethod(target);

            var func = (Func<byte[], bool, ObjectReader>)
                    Delegate.CreateDelegate(typeof(Func<byte[], bool, ObjectReader>), method);
            func(new byte[0], true);
        }

        [TestCase(typeof(PropertyTypeNotSupport), ExpectedException = typeof(NotSupportedException), ExpectedMessage = "Not supported type: \'TestValueType\'")]
        [TestCase(typeof(FloatPropertyHasNonDefaultBitsCount), ExpectedException = typeof(ArgumentException), ExpectedMessage = "For properties of type Single use default bits count value")]
        [TestCase(typeof(DoublePropertyHasNonDefaultBitsCount), ExpectedException = typeof(ArgumentException), ExpectedMessage = "For properties of type Double use default bits count value")]
        public void CreateShouldThrowWithInnerExceptionDetails(Type target)
        {
            try
            {
                var type = typeof(ObjectReader);
                var method = type.GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
                method = method.MakeGenericMethod(target);

                var func = (Func<byte[], bool, ObjectReader>)
                        Delegate.CreateDelegate(typeof(Func<byte[], bool, ObjectReader>), method);
                func(new byte[0], true);
            }
            catch (ArgumentException e)
            {
                throw e.InnerException;
            }
        }
    }
}