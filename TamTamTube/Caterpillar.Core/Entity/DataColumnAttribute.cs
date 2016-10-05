using System;
using System.Data;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Can be applied on properties. Shows that properties marked with this attributes correspond database table columns.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DataColumnAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets whether the column which is represented by property marked by this attribute is primary key.
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets description of sql table column represented by property which is marked by this type of attribute.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the column which is represented by property marked by this attribute is an idendity column.
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Gets or sets whether sql table column represented by property which is marked by this type of attribute is unique or not.
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// Gets or sets whether sql table column represented by property which is marked by this type of attribute is auto increment.
        /// </summary>
        public bool IsAutoIncrement { get; set; }

        /// <summary>
        /// Gets or sets seed of sql table column represented by property which is marked by this type of attribute. Only has value when represented table column is auto incremental.
        /// </summary>
        public int? Seed { get; set; }

        /// <summary>
        /// Gets or sets auto incremental step of sql table column represented by property which is marked by this type of attribute. Only has value when represented table column is auto incremental.
        /// </summary>
        public int? AutoIncrementStep { get; set; }

        /// <summary>
        /// Gets or sets column name of sql table column represented by property which is marked by this type of attribute.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets DbType of sql table column represented by property which is marked by this type of attribute.
        /// </summary>
        public DbType DataType { get; set; }

        /// <summary>
        /// Gets or sets SqlDbType of sql table column represented by property which is marked by this type of attribute.
        /// </summary>
        public SqlDbType SqlDbType { get; set; }
    }
}
