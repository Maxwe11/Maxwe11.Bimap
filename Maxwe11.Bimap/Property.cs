namespace Maxwe11.Bimap
{
    using System;
    using System.Reflection;

    using Maxwe11.Bimap.Numbers;

    using Double = Maxwe11.Bimap.Numbers.Double;

    class Property
    {
        #region Fields

        private readonly string mName;

        private readonly Action<object, object> mSetter;

        private readonly INumber mNumber;

        #endregion

        #region Constructor

        private Property(string name, Action<object, object> setter, INumber number)
        {
            mName = name;
            mSetter = setter;
            mNumber = number;
        }

        #endregion

        #region Properties

        public int BitsCount { get { return mNumber.BitsCount; } }

        public string Name { get { return mName; } }

        #endregion

        #region Methods

        public static Property Create<TDeclared, TTarget>(string name, MethodInfo info, int bitsCount)
        {
            var type = typeof(TTarget);
            INumber number = null;

            try
            {
                number = CreateNumberForType(type, bitsCount);
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

            return new Property(name, setter2, number);
        }

        private static INumber CreateNumberForType(Type type, int bitsCount)
        {
            var typeCode = Type.GetTypeCode(type);

            if (typeCode == TypeCode.Byte || typeCode == TypeCode.SByte)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Bimap.BitsCount.BitsPerByte;
                }
                else
                {
                    Bimap.BitsCount.EnsureBitsCount(bitsCount, Bimap.BitsCount.BitsPerByte);
                }

                return typeCode == TypeCode.Byte ? new UnsignedInteger8(bitsCount) : (INumber)new Integer8(bitsCount);
            }

            if (typeCode == TypeCode.UInt16 || typeCode == TypeCode.Int16)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Bimap.BitsCount.BitsPerWord;
                }
                else
                {
                    Bimap.BitsCount.EnsureBitsCount(bitsCount, Bimap.BitsCount.BitsPerWord);
                }

                return typeCode == TypeCode.UInt16
                    ? new UnsignedInteger16(bitsCount)
                    : (INumber)new Integer16(bitsCount);
            }

            if (typeCode == TypeCode.UInt32 || typeCode == TypeCode.Int32)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Bimap.BitsCount.BitsPerDWord;
                }
                else
                {
                    Bimap.BitsCount.EnsureBitsCount(bitsCount, Bimap.BitsCount.BitsPerDWord);
                }

                return typeCode == TypeCode.UInt32
                    ? new UnsignedInteger32(bitsCount)
                    : (INumber)new Integer32(bitsCount);
            }

            if (typeCode == TypeCode.UInt64 || typeCode == TypeCode.Int64)
            {
                if (bitsCount == -1)
                {
                    bitsCount = Bimap.BitsCount.BitsPerQWord;
                }
                else
                {
                    Bimap.BitsCount.EnsureBitsCount(bitsCount, Bimap.BitsCount.BitsPerQWord);
                }

                return typeCode == TypeCode.UInt64
                    ? new UnsignedInteger64(bitsCount)
                    : (INumber)new Integer64(bitsCount);
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

        public void Apply(object target, INumbersVisitor visitor)
        {
            try
            {
                mNumber.Apply(visitor);
            }
            catch (InvalidOperationException e)
            {
                var msg = "Failed to read property \'" + mName + "\'. See inner exception for details";
                throw new BimapException(msg, e);
            }

            mSetter(target, mNumber.Value);
        }

        #endregion
    }
}
