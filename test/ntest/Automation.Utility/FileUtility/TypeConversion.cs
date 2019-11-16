using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace Automation.Utility.FileUtility
{
    public class TypeConversion
    {
        public static List<T> ConvertTo<T>(string csvFilePath, string prefix)
        {
            prefix = prefix ?? "";
            const char SEPERATOR = ',';
            string[] linesInFile = File.ReadAllLines(csvFilePath);

            if (linesInFile is null || linesInFile.Length == 0)
            {
                return null;
            }

            var propertiesCollection = TypeDescriptor.GetProperties(typeof(T));
            string[] headers = linesInFile[0].Split(SEPERATOR);
            var targetType = typeof(T);
            var instacnesList = new List<T>(linesInFile.Length - 1);
            var properties = new Dictionary<PropertyDescriptor, int>();

            for (int i = 0; i < headers.Length; i++)
            {
                string propertyName = headers[i];
                int index = propertyName.IndexOf(prefix);
                if (index >= 0)
                {
                    propertyName = propertyName.Substring(index + prefix.Length);
                }

                var prop = propertiesCollection.Find(propertyName, true);

                if (!(prop is null))
                {
                    properties.Add(prop, i);
                }
            }

            for (int linePosition = 1; linePosition < linesInFile.Length; linePosition++)
            {
                var instance = Activator.CreateInstance<T>();
                string[] currentLine = linesInFile[linePosition].Split(SEPERATOR);

                if (currentLine.Length < properties.Count)
                {
                    Array.Resize(ref currentLine, currentLine.Length + Math.Abs(currentLine.Length - properties.Count));
                }

                var propertyEnumerator = properties.GetEnumerator();

                while (propertyEnumerator.MoveNext())
                {
                    var actualProperty = propertyEnumerator.Current.Key;
                    int propertyPosition = propertyEnumerator.Current.Value;
                    if (string.IsNullOrEmpty(currentLine[propertyPosition]))
                    {
                        continue;
                    }

                    if (!actualProperty.IsReadOnly)
                    {
                        if (actualProperty.PropertyType.IsEnum)
                        {
                            actualProperty.SetValue(instance, Enum.Parse(actualProperty.PropertyType, currentLine[propertyPosition]));
                        }
                        else
                        {
                            actualProperty.SetValue(instance, actualProperty.Converter.ConvertFromInvariantString(currentLine[propertyPosition]));
                        }
                    }
                }
                
                instacnesList.Add(instance);
            }

            return instacnesList;
        }

        public static List<T> ConvertTo<T>(string csvFilePath)
        {
            return ConvertTo<T>(csvFilePath, null);
        }

        public static T ConvertTo<T>(TestContext context, string prefix)
        {
            prefix = prefix ?? "";
            var propertiesCollection = TypeDescriptor.GetProperties(typeof(T));
            var headers = new List<string>(context.DataRow.Table.Columns.Count);
            var targetType = typeof(T);
            var properties = new List<PropertyDescriptor>();

            foreach (DataColumn column in context.DataRow.Table.Columns)
            {
                headers.Add(column.ColumnName);
            }

            for (int i = 0; i < headers.Count; i++)
            {
                string propertyName = headers[i];
                int index = propertyName.IndexOf(prefix);
                if (index >= 0)
                {
                    propertyName = propertyName.Substring(index + prefix.Length);
                }

                var prop = propertiesCollection.Find(propertyName, true);
                if (!(prop is null))
                {
                    properties.Add(prop);
                }
            }

            var instance = Activator.CreateInstance<T>();

            for (int i = 0; i < properties.Count; i++)
            {
                var actualProperty = properties[i];
                string columnName = prefix + actualProperty.Name;
                if (string.IsNullOrEmpty($"{context.DataRow[columnName]}"))
                {
                    continue;
                }

                if (!actualProperty.IsReadOnly)
                {
                    if (actualProperty.PropertyType.IsEnum)
                    {
                        actualProperty.SetValue(instance, Enum.Parse(actualProperty.PropertyType, $"{context.DataRow[columnName]}"));
                    }
                    else
                    {
                        actualProperty.SetValue(instance, actualProperty.Converter.ConvertFromInvariantString($"{context.DataRow[columnName]}"));
                    }
                }
            }

            return instance;
        }

        public static T ConvertTo<T>(TestContext context)
        {
            return ConvertTo<T>(context, null);
        }
    }
}
