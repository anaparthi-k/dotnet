using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Automation.Utility.FileUtility
{
    public class FileHandlerBase
    {
        #region Private Variables

        private readonly string[] r_valueSepartor = new string[] { "&", "," };
        protected readonly string[] r_parameterConcatinationSymbol = new string[] { "&" };
        private const string RANDOMFORMAT = "{random}";
        private string m_randomOutPutFormat = null;
        private readonly SortedDictionary<string, string> r_configValues = new SortedDictionary<string, string>();
        private readonly PropertyFile propertyFile = null;

        #endregion

        #region Public Properties

        //protected string ConfigurationFilePath { get; private set; }

        public FormatterProvider Provider { get; set; } = new FormatterProvider();

        #endregion

        protected FileHandlerBase(string configurationFilePath)
        {
            propertyFile = new PropertyFile(configurationFilePath);
        }

        private string GetRandomString()
        {
            if (m_randomOutPutFormat == null)
            {
                m_randomOutPutFormat = propertyFile.GetProperty("RandomOutPutFormat");
            }

            return DateTime.Now.ToString(m_randomOutPutFormat);
        }

        protected List<string> GetFormattedLines(string filePath, FileValueSeparator separator, ParameterConcatinationSymbol symbol)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath should not be null");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("filePath is not found");
            }

            string delimeter = r_valueSepartor[(int)separator];
            string concatWith = r_parameterConcatinationSymbol[(int)symbol];

            var lstParams = new List<string>();

            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimeter);

                string[] headerfields = parser.ReadFields();

                string fieldValue = string.Empty;

                while (!parser.EndOfData)
                {
                    var sb = new StringBuilder();
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        fieldValue = fields[i];

                        if (fieldValue.ToLower().Contains(RANDOMFORMAT))
                        {
                            fieldValue = fieldValue.Replace(RANDOMFORMAT, GetRandomString());
                        }

                        sb.AppendFormat("{0}={1}{2}", headerfields[i], fieldValue, i < fields.Length - 1 ? concatWith : "");
                    }

                    lstParams.Add(sb.ToString());
                }
            }

            return lstParams;
        }

        #region public Methods

       

        #endregion

        #region Private Methods       

        #endregion
    }
}
