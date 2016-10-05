using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Represents creation date extended property field for sql unit (sql database, table, table column etc...).
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class CreateDateExtendedPropertyAttribute : ElementExtendedPropertyAttribute
    {
        /// <summary>
        /// Creates a new CreateDateAttribute instance.
        /// </summary>
        /// <param name="value"></param>
        public CreateDateExtendedPropertyAttribute(object value)
            : base(value)
        {

        }

        /// <summary>
        /// Gets CreataDate's extended property key for sql unit.
        /// </summary>
        public override string Key
        {
            get
            {
                return "CreateDate";
            }
            protected set
            {
                
            }
        }
    }
}
