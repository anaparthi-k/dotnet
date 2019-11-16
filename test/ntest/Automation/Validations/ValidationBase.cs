using Automation.Helpers.Facade;
using Automation.Utility.FileUtility;
using System;
using System.Collections.Generic;

namespace Automation.Validations
{
    public abstract class ValidationBase
    {
        private static bool s_validationStatus = true;
        private static Dictionary<string, object> s_validationParameters = new Dictionary<string, object>();
        private readonly SetupHelperBaseLine m_setup;

        private PropertyFile TestData { get; }

        protected internal ValidationBase(SetupHelperBaseLine setup)
        {
            if (setup == null)
                throw new ArgumentNullException("setup");
            this.m_setup = setup;
        }

        /// <summary>
        /// This is used to get completed validation status thourgh out the validations.
        /// </summary>
        public bool ValidationStatus => s_validationStatus;

        protected bool IsValid(bool action)
        {
            s_validationStatus &= action;
            return action;
        }


        public void Reset()
        {
            s_validationStatus = true;
            s_validationParameters.Clear();
        }

        public void AddValidationParameter(string key, string value)
        {
            AddOrUpdateValue(key, value);
        }

        public void AddValidationParameter<T>(string key, T value)
        {
            AddOrUpdateValue(key, value);
        }

        private static void AddOrUpdateValue<T>(string key, T value)
        {
            if (s_validationParameters.ContainsKey(key))
            {
                s_validationParameters[key] = value;
            }
            else
            {
                s_validationParameters.Add(key, value);
            }
        }

        protected string GetTestDataFromFile(string key)
        {
            return TestData.GetProperty(key);
        }

        protected string GetTestDataFromFileThrowIfNull(string key)
        {
            string val = TestData.GetProperty(key);

            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(key);
            }

            return val;
        }

        protected string GetTestCaseData(string key)
        {
            return GetCustomTestCaseData<string>(key) ?? "";
        }

        protected string GetTestCaseDataThrowIfNull(string key)
        {
            string val = GetCustomTestCaseData<string>(key) ?? "";

            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(key);
            }

            return val;
        }

        protected T GetCustomTestCaseData<T>(string key)
        {
            if (s_validationParameters.ContainsKey(key))
            {
                return (T)s_validationParameters[key];
            }

            return default(T);
        }

        protected T GetCustomTestCaseDataThrowIfNull<T>(string key)
        {
            var val = (T)s_validationParameters[key];

            if (default(T).Equals(val))
            {
                throw new ArgumentNullException(key);
            }

            return val;
        }
    }
}
