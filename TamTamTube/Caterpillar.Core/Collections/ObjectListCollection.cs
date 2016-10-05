using System.Collections.Generic;

namespace Caterpillar.Core.Collections
{
    /// <summary>
    /// Object list collection
    /// </summary>
    public class ObjectListCollection : List<object>
    {
        public ObjectListCollection()
        {

        }

        public ObjectListCollection(IEnumerable<object> items)
        {
            this.AddRange(items);
        }
    }
}
