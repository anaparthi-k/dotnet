using Newtonsoft.Json;
using Automation.Utility.Common;
using System.Linq;
using System.Xml;

namespace Automation.Utility.Converters
{
    internal class CustomJsonConverter : IConverter
    {

        public string ConvertTo(string value)
        {
            if (!Validate.IsJson(value))
            {
                var doc = new XmlDocument();
                doc.LoadXml(value);
                return JsonConvert.SerializeXmlNode(doc);
            }

            return value;
        }

        public T Deserialize<T>(string input) where T : class
        {
           return JsonConvert.DeserializeObject<T>(input);
        }

        public T Deserialize<T>(string input, params object[] settings) where T : class
        {
            return (T)JsonConvert.DeserializeObject(input, typeof(T), settings.Cast<JsonConverter>().ToArray());
        }

        public string Serialize<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        
    }
}
