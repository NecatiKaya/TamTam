using System.ComponentModel;

namespace Caterpillar.Core.Common
{
    [DefaultValue(0)]
    /// <summary>
    /// Defines types of data.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Shows that type of the data is not set.
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Shows that data is in json format.
        /// </summary>
        Json = 1,
        /// <summary>
        /// Shows that data is in text format.
        /// </summary>
        Text = 2,
        /// <summary>
        /// Shows that data is in xml format.
        /// </summary>
        Xml = 3,
        /// <summary>
        /// Shows that data is in html format.
        /// </summary>
        Html = 4
    }
}
