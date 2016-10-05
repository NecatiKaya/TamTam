using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Caterpillar.Core.Helpers
{
    public static class SerializationHelper
    {
        public static string Serialize(object obj) 
        {
            string xml = null;
            if (obj != null)
            {
                Type objType = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(objType);
                StringBuilder builder = new StringBuilder();
                using (StringWriter writer = new StringWriter(builder))
                {
                    serializer.Serialize(writer, obj);
                    xml = builder.ToString();
                }
            }
            return xml;
        }

        public static T Deserialize<T>(string xml)
        {
            T obj = default(T);
            if (!string.IsNullOrWhiteSpace(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    obj = (T)serializer.Deserialize(reader);
                }
            }
            return obj;
        }
    }
}
