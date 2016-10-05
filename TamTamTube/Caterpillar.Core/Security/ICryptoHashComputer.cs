namespace Caterpillar.Core.Security
{
    /// <summary>
    /// Interface types for crypto services to compute hash
    /// </summary>
    public interface ICryptoHashComputer
    {
        /// <summary>
        /// Get hash of string in byte array
        /// </summary>
        /// <param name="value">String value to compute hash.</param>
        /// <returns>Returns hash of specified string value into by array.</returns>
        byte[] GetHashedArray(string value);
    }
}
