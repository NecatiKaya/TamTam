using System.Collections.Generic;

namespace Caterpillar.Core.Collections
{
    /// <summary>
    /// Generic List&lt;string&gt; derived type.
    /// </summary>
    public class StringListCollection : List<string>
    {
        public StringListCollection()
        {

        }

        /// <summary>
        /// Initialize StringListCollection type with items.
        /// </summary>
        /// <param name="items"></param>
        public StringListCollection(IEnumerable<string> items)
        {
            this.AddRange(items);
        }
    }
}
