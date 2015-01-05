namespace Maxwe11.Bimap.Impl
{
    using System;
    using System.Reflection;
    using Properties;
    using Double = Properties.Double;

    class Property
    {
        #region Fields

        private readonly string mName;

        private readonly Action<object, object> mSetter;

        private readonly IPropertyValue mPropertyValue;

        #endregion

        #region Constructor

        private Property(string name, Action<object, object> setter, IPropertyValue propertyValue)
        {
            mName = name;
            mSetter = setter;
            mPropertyValue = propertyValue;
        }

        #endregion

        #region Properties

        public string Name { get { return mName; } }

        #endregion

        #region Methods

        public static Property Create<TDeclared, TTarget>(string name, MethodInfo info, int bitsCount, int? arrayLength)
        {
            var type = typeof(TTarget);
            IPropertyValue propertyValue = null;

            try
            {
                propertyValue = arrayLength.HasValue ? CreatePropertyValueForArray(type, bitsCount, arrayLength.Value)
                                                     : CreatePropertyValueForType(type, bitsCount);
            }
            catch (ArgumentException e)
            {
                var msg = "Failed to map property \'" + name + "\'. See inner exception for details";
                throw new BimapException(msg, e);
            }
            catch (NotSupportedException e)
            {
                var msg = "Failed to map property \'" + name + "\'. See inner exception for details";
                throw new BimapException(msg, e);
            }

            var setter = (Action<TDeclared, TTarget>)Delegate.CreateDelegate(typeof(Action<TDeclared, TTarget>), info);
            var setter2 = (Action<object, object>)((declared, target) => setter((TDeclared)declared, (TTarget)target));

            return new Property(name, setter2, propertyValue);
        }

        private static IPropertyValue CreatePropertyValueForType(Type type, int bitsCount)
        {
            var typeCode = Type.GetTypeCode(type);

            if (typeCode == TypeCode.Byte || typeCode == TypeCode.SByte)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Helpers.BitsCount.BitsPerByte;
                }
                else
                {
                    Helpers.BitsCount.EnsureBitsCount(bitsCount, Helpers.BitsCount.BitsPerByte);
                }

                return typeCode == TypeCode.Byte ? new UnsignedInteger8(bitsCount) : (IPropertyValue)new Integer8(bitsCount);
            }

            if (typeCode == TypeCode.UInt16 || typeCode == TypeCode.Int16)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Helpers.BitsCount.BitsPerWord;
                }
                else
                {
                    Helpers.BitsCount.EnsureBitsCount(bitsCount, Helpers.BitsCount.BitsPerWord);
                }

                return typeCode == TypeCode.UInt16
                    ? new UnsignedInteger16(bitsCount)
                    : (IPropertyValue)new Integer16(bitsCount);
            }

            if (typeCode == TypeCode.UInt32 || typeCode == TypeCode.Int32)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Helpers.BitsCount.BitsPerDWord;
                }
                else
                {
                    Helpers.BitsCount.EnsureBitsCount(bitsCount, Helpers.BitsCount.BitsPerDWord);
                }

                return typeCode == TypeCode.UInt32
                    ? new UnsignedInteger32(bitsCount)
                    : (IPropertyValue)new Integer32(bitsCount);
            }

            if (typeCode == TypeCode.UInt64 || typeCode == TypeCode.Int64)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Helpers.BitsCount.BitsPerQWord;
                }
                else
                {
                    Helpers.BitsCount.EnsureBitsCount(bitsCount, Helpers.BitsCount.BitsPerQWord);
                }

                return typeCode == TypeCode.UInt64
                    ? new UnsignedInteger64(bitsCount)
                    : (IPropertyValue)new Integer64(bitsCount);
            }

            if (typeCode == TypeCode.Single)
            {
                if (bitsCount != -1)
                    throw new ArgumentException("For properties of type Single default bits count value should be used");

                return new Float();
            }

            if (typeCode == TypeCode.Double)
            {
                if (bitsCount != -1)
                    throw new ArgumentException("For properties of type Double default bits count value should be used");

                return new Double();
            }

            throw new NotSupportedException("Not supported type: \'" + type.Name + "\'");
        }

        private static IPropertyValue CreatePropertyValueForArray(Type type, int bitsCount, int length)
        {
            if (!type.IsArray)
                throw new NotSupportedException("Not supported type: \'" + type.Name + "\'");
            
            var elemType = type.GetElementType();
            if (Type.GetTypeCode(elemType) != TypeCode.Byte)
                throw new NotSupportedException("Not supported type: \'" + type.Name + "\'");
            
            if (bitsCount == -1)
            {
                bitsCount = Helpers.BitsCount.BitsPerByte;
            }
            else
            {
                Helpers.BitsCount.EnsureBitsCount(bitsCount, Helpers.BitsCount.BitsPerByte);
            }

            return new ByteArray(length, bitsCount);
        }

        public void Apply(object target, IPropertiesVisitor visitor)
        {
            try
            {
                mPropertyValue.Apply(visitor);
            }
            catch (InvalidOperationException e)
            {
                var msg = "Failed to read property \'" + mName + "\'. See inner exception for details";
                throw new BimapException(msg, e);
            }

            mSetter(target, mPropertyValue.Value);
        }

        #endregion
    }
}
