using System.Collections.Generic;

namespace Caterpillar.Core.Collections
{
    public class StringToStringDictionary : Dictionary<string, string>
    {
        public StringToStringDictionary()
        {

        }
        public StringToStringDictionary(Dictionary<string, string> items)
        {
            foreach (KeyValuePair<string, string> each in items)
            {
                this.Add(each.Key, each.Value);
            }
        }
    }
}
