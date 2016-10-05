using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Represents author's extended property field for a sql table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AuthorExtendedPropertyAttribute : ElementExtendedPropertyAttribute
    {
        /// <summary>
        /// Creates a new HostedProjectDescriptionAttribute instance.
        /// </summary>
        /// <param name="value"></param>
        public AuthorExtendedPropertyAttribute(object value)
            : base(value)
        {

        }

        /// <summary>
        /// Gets extended property for Author.
        /// </summary>
        public override string Key
        {
            get
            {
                return "Author";
            }
            protected set
            {

            }
        }
    }
}
