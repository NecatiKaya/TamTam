using System;

namespace Caterpillar.Core.Extensions
{
    /// <summary>
    /// Contains extension methods ofr System.Array type.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Check an array instance for not being null and containing element.
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Returns true if array instance is not null and contains at least 1 element. Othwerwise, false.</returns>
        public static bool IsNotNullAndContainsAnyElements(this Array array) 
        {
            return array.IsNotNull() && array.Length > 0;
        }
    }
}
