using System;

namespace Caterpillar.Core.Entity
{
    /// <summary>
    /// Represents hosted project extended property field for a sql table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class HostedProjectExtendedPropertyAttribute : ElementExtendedPropertyAttribute
    {
        /// <summary>
        /// Creates a new HostedProjectDescriptionAttribute instance.
        /// </summary>
        /// <param name="value"></param>
        public HostedProjectExtendedPropertyAttribute(object value)
            : base(value)
        {

        }

        /// <summary>
        /// Gets extended property key for Hosted Project description
        /// </summary>
        public override string Key
        {
            get
            {
                return "HostedProject";
            }
            protected set
            {
                
            }
        }
    }
}
