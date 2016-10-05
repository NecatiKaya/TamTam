using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Can be applied on classes. Shows that classes marked with this attributes correspond database tables.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets schema name of table.
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets name of table.
        /// </summary>
        public string TableName { get; set; }
    }
}
