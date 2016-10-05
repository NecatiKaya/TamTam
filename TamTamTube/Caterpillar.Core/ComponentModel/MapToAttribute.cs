using System;

namespace Caterpillar.Core.ComponentModel
{
    /// <summary>
    /// Maps applied type to another type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class MapToAttribute : Attribute
    {
        private readonly Type[] _typesToMap;

        public MapToAttribute(Type[] typesToMap)
        {
            _typesToMap = typesToMap;
        }

        public Type[] TypesToMap
        {
            get
            {
                return _typesToMap;
            }
        }
    }
}
