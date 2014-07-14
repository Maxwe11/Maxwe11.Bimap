namespace Maxwe11.Bimap
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class BimapException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BimapException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BimapException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
