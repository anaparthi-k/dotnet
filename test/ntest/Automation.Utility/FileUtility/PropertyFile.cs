using System;
using System.IO;
using System.Linq;

namespace Automation.Utility.FileUtility
{
    public class PropertyFile
    {
        private readonly string filePath;

        public PropertyFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException($"Path is empty");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"'{Path.GetFullPath(filePath)}' is not found in the given location.");
            }

            this.filePath = filePath;
           
        }

        /// <summary>
        /// Returns an empty value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetProperty(string key)
        {
            var lines = File.ReadLines(filePath);
            key = key.Trim();
            string line = lines.FirstOrDefault(x =>
            {
                if (x.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                {
                    int index = x.IndexOf('=');
                    return key.Equals(x.Substring(0, index).Trim(),StringComparison.OrdinalIgnoreCase);
                }

                return false;
            });

            if (!string.IsNullOrEmpty(line))
            {
                int index = line.IndexOf('=');
                return line.Substring(index + 1).Trim();
            }

            return null;
        }
    }
}
