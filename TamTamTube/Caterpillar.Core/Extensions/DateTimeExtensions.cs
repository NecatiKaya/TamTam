using System;

namespace Caterpillar.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsEmptyDate(this DateTime dateTime)
        {
            return dateTime == new DateTime();
        }
    }
}
