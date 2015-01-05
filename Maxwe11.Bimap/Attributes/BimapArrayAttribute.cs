namespace Maxwe11.Bimap.Attributes
{
    using System.Reflection;
    using Impl;

    /// <summary>
    /// 
    /// </summary>
    public class BimapArrayAttribute : BimapAttribute
    {
        private readonly int mLength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="Length"></param>
        /// <param name="BitsCount"></param>
        // ReSharper disable InconsistentNaming
        public BimapArrayAttribute(int OrderId, int Length, int BitsCount = -1) : base(OrderId, BitsCount)
        // ReSharper restore InconsistentNaming
        {
            mLength = Length;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length { get { return mLength; } }

        internal override Property MakeProperty(MethodInfo creator, string propertyName, MethodInfo propertySetter)
        {
            return (Property)creator.Invoke(null, new object[] { propertyName, propertySetter, BitsCount, Length });
        }
    }
}
