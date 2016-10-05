using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Represents an ms sql extended property field for sql unit (sql database, table, table column etc...)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public abstract class ElementExtendedPropertyAttribute : Attribute
    {
        public ElementExtendedPropertyAttribute(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets extended property field name.
        /// </summary>
        public abstract string Key { get; protected set; }

        /// <summary>
        /// Gets or sets value for the sql unit (sql database, table, table column etc...).
        /// </summary>
        public object Value { get; protected set; }
    }
}
