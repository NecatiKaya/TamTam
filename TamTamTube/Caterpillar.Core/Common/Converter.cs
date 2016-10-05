using System;

namespace Caterpillar.Core.Common
{
    /// <summary>
    /// Contains main type converter or mapper methods
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Converts data type in string format(e.g. json or xml ) to enum DataType
        /// </summary>
        /// <param name="dataType">Data type in string format</param>
        /// <returns>Returns DataType enum.</returns>
        public static DataType ToDataType(string dataType)
        {
            return (DataType)Enum.Parse(typeof(DataType), dataType, true);
        }
    }
}
