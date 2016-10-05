using System;

namespace Caterpillar.Core.Extensions
{
    /// <summary>
    /// Contains extensions for byte[]
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts byte[] instance to base-64 encoded digits.
        /// </summary>
        /// <param name="buffer">Byte[] instance to convert.</param>
        /// <returns>Returns byte[] instance into base-64 encoded string.</returns>
        public static string ToBase64String(this byte[] buffer) 
        {
            return Convert.ToBase64String(buffer);
        }
    }
}
