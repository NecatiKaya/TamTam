using System.Security.Cryptography;
using System.Text;

namespace Caterpillar.Core.Security
{
    /// <summary>
    /// Uses SHA1Managed library to compute hash
    /// </summary>
    public class SHA1ManagedHashComputer : ICryptoHashComputer
    {
        #region ICryptoHashComputer

        /// <summary>
        /// Get hash of string in byte array thanks to SHA1Managed library
        /// </summary>
        /// <param name="value">String value to compute hash.</param>
        /// <returns>Returns hash of specified string value into by array.</returns>
        public byte[] GetHashedArray(string value)
        {
            return SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(value));
        }

        #endregion
    }
}
