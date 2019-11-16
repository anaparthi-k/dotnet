using System;
using System.ComponentModel;
using System.Linq;

namespace Automation.Utility.Conversion
{
    public class FormatType
    {
        private FormatType()
        {

        }

        public static string GetDescription(Enum type)
        {
            var genericEnumType = type.GetType();
            var memberInfo = genericEnumType.GetMember(type.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                object[] attribute = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((attribute != null && attribute.Count() > 0))
                {
                    return ((DescriptionAttribute)attribute.ElementAt(0)).Description;
                }
            }
            return type.ToString().Replace("_", " ");
        }

        public static T ToEnum<T>(string text) where T : struct
        {
            var result = default(T);
            var genericEnumType = typeof(T);
            var memberInfo = genericEnumType.GetMembers();

            var strDescription = memberInfo.Where(x =>
                    {
                        object[] attribute = x.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if ((attribute != null && attribute.Length > 0))
                        {
                            return attribute.Cast<DescriptionAttribute>().FirstOrDefault(a => string.Compare(a.Description, text, true) == 0) != null;
                        }
                        return false;
                    }).FirstOrDefault();

            if (strDescription == null)
            {
                Enum.TryParse(text, out result);
            }
            else
            {
                result = (T)Enum.Parse(genericEnumType, strDescription.Name);
            }

            return result;
        }
    }
}
