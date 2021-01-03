using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Serializer
{
    internal static class GenericXmlSerializer
    {
        public static void WriteObject<T>(T outputObject, string outputFile)
        {
            using (FileStream writer = new FileStream(outputFile, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(writer, outputObject);
            }
        }

        public static T ReadObject<T>(string objectData)
        {
            T deserializedObject = default(T);
            try
            {
                using (FileStream reader = new FileStream(objectData, FileMode.OpenOrCreate))
                {
                    XmlTextReader xmlReader = new XmlTextReader(reader);
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    deserializedObject = (T)ser.Deserialize(xmlReader);
                    xmlReader.Close();
                }
            }
            catch
            {
                deserializedObject = default(T);
                return deserializedObject;
            }

            return deserializedObject;
        }
    }
}
