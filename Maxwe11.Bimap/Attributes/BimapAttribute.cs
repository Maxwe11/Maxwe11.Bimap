namespace Maxwe11.Bimap.Attributes
{
    using System;
    using System.Reflection;
    using Maxwe11.Bimap.Impl;

    /// <summary>
    /// Specifies order id and amount of bits for a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BimapAttribute : Attribute
    {
        #region Fields

        private readonly int mOrderId;

        private readonly int mBitsCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BimapAttribute"/> class
        /// </summary>
        /// <param name="OrderId">Used for reading properties in specified order</param>
        /// <param name="BitsCount">Used for specifying amount of bits for properties of integer types</param>
        // ReSharper disable InconsistentNaming
        public BimapAttribute(int OrderId, int BitsCount = -1)
        // ReSharper restore InconsistentNaming
        {
            mOrderId = OrderId;
            mBitsCount = BitsCount;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets property order id
        /// </summary>
        public int OrderId { get { return mOrderId; } }

        /// <summary>
        /// Gets amount of bits for property
        /// </summary>
        public int BitsCount { get { return mBitsCount; } }

        #endregion

        #region Methods

        internal virtual Property MakeProperty(MethodInfo creator, string propertyName, MethodInfo propertySetter)
        {
            return (Property) creator.Invoke(null, new object[] {propertyName, propertySetter, BitsCount, null});
        }

        #endregion
    }
}
