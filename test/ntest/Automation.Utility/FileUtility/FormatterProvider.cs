using Automation.Utility.Converters;
using Newtonsoft.Json.Converters;
using System;

namespace Automation.Utility.FileUtility
{
    public class FormatterProvider
    {
        internal FormatterProvider()
        {

        }
        public object GetDateFormatter(ConversionType conversion, string dateFormat)
        {
            switch (conversion)
            {
                case ConversionType.Json:
                    return new IsoDateTimeConverter() { DateTimeFormat = dateFormat };
                case ConversionType.Xml:
                    throw new NotSupportedException();
            }

            return null;
        }
    }
}
