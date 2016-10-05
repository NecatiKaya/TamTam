using Caterpillar.Core.Exception;
using System;

namespace Caterpillar.Core.Helpers
{
    /// <summary>
    /// Contains helper methods fo datetime.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns new datetime instance by producing datetime value type from  string which has a date in sql 112 format. For example, 20121213
        /// </summary>
        /// <param name="sql112Date">Date in Sql 112 date format</param>
        /// <returns>Returns new datetime instance</returns>
        public static DateTime ToDateTimeFromSql112DateFormat(string sql112Date) 
        {
            if (string.IsNullOrWhiteSpace(sql112Date))
            {
                throw new CoreLevelException("'sql112Date' parameter can not be null, empty or whitespace.", new ArgumentNullException("sql112Date"));
            }

            try
            {
                int year = Convert.ToInt32(sql112Date.Substring(0, 4));
                int month = Convert.ToInt32(sql112Date.Substring(4, 2));
                int day = Convert.ToInt32(sql112Date.Substring(6, 2));

                return new DateTime(year, month, day);
            }
            catch (System.Exception ex)
            {
                throw new FormatException("'sql112Date' parameter is not in valid Sql 112 date format. For example, 20121231 is valid sql 112 format.", ex);
            }
        }

        /// <summary>
        /// If specified sql112Date is not null, then returns new datetime instance by producing datetime value type from  string which has a date in sql 112 format. For example, 20121213. If the sql112Date is null, returns null
        /// </summary>
        /// <param name="sql112Date">Date in Sql 112 date format.</param>
        /// <returns>Returns new datetime instance</returns>
        public static DateTime? ToNullableDateTimeFromSql112DateFormat(string sql112Date)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(sql112Date))
                {
                    int year = Convert.ToInt32(sql112Date.Substring(0, 4));
                    int month = Convert.ToInt32(sql112Date.Substring(4, 2));
                    int day = Convert.ToInt32(sql112Date.Substring(6, 2));

                    return new DateTime(year, month, day);
                }
            }
            catch (System.Exception ex)
            {
                throw new FormatException("'sql112Date' parameter is not in valid Sql 112 date format. For example, 20121231 is valid sql 112 format.", ex);
            }

            return null;
        }
    }
}
