using Newtonsoft.Json;
using Automation.Utility.Common;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Automation.Utility.Converters
{
    public class XmlConverter : IConverter
    {
        public string ConvertTo(string value)
        {
            if (Validate.IsJson(value))
            {
                return JsonConvert.DeserializeXNode(value).ToString();
            }

            return value;
        }

        public T Deserialize<T>(string input) where T : class
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public T Deserialize<T>(string input, params object[] settings) where T : class
        {
            throw new NotImplementedException();
        }

        public string Serialize<T>(T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, objectToSerialize);
                return textWriter.ToString();
            }
        }
    }
}
