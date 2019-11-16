using Automation.Utility.FileUtility;
using System;
using System.Collections.Generic;

namespace Automation.Configurations
{
    public class TestConfigurationManager
    {
        private readonly PropertyFile testDataFile;
        private readonly SortedDictionary<string, string> configValues = new SortedDictionary<string, string>();

        public TestConfigurationManager(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException();
            }
            testDataFile = new PropertyFile(filePath);
        }

        protected virtual string GetData(string key)
        {
            if (!configValues.ContainsKey(key))
            {
                configValues.Add(key, testDataFile.GetProperty(key));
            }

            return configValues[key];
        }



    }
}
