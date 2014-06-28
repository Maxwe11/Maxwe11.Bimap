namespace Maxwe11.Bimap.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BimapAttribute : Attribute
    {
        #region Fields

        private readonly int mOrderId;

        private readonly int mBitsCount;

        #endregion

        #region Constructors

        // ReSharper disable InconsistentNaming
        public BimapAttribute(int OrderId, int BitsCount = -1)
        // ReSharper restore InconsistentNaming
        {
            mOrderId = OrderId;
            mBitsCount = BitsCount;
        }

        #endregion

        #region Properties

        public int OrderId { get { return mOrderId; } }

        public int BitsCount { get { return mBitsCount; } }

        #endregion
    }
}
