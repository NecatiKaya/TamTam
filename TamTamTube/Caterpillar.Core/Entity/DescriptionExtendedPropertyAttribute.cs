using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Represents extended property description field for a sql table.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class DescriptionExtendedPropertyAttribute : ElementExtendedPropertyAttribute
    {
        /// <summary>
        /// Creates a new DescriptionDescriptionAttribute instance.
        /// </summary>
        /// <param name="value"></param>
        public DescriptionExtendedPropertyAttribute(object value)
            : base(value)
        {

        }

        /// <summary>
        /// Gets key for extended property.
        /// </summary>
        public override string Key
        {
            get
            {
                return "MS_Description";
            }
            protected set
            {

            }
        }
    }
}
