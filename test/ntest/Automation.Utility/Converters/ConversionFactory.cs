namespace Automation.Utility.Converters
{
    public class ConversionFactory
    {
        private static readonly IConverter[] sr_converters = new IConverter[] { new CustomJsonConverter(), new XmlConverter() };

        private IConverter GetConverter(ConversionType conversion)
        {
            return sr_converters[(int)conversion];
        }

        public string ConvertTo(string content, ConversionType conversion)
        {
            return GetConverter(conversion).ConvertTo(content);
        }

        public T ConvertTo<T>(string content, ConversionType conversion) where T : class
        {
            return GetConverter(conversion).Deserialize<T>(content);
        }

        public T ConvertTo<T>(string content, ConversionType conversion, params object[] settings) where T : class
        {
            return GetConverter(conversion).Deserialize<T>(content, settings);
        }
    }
}
