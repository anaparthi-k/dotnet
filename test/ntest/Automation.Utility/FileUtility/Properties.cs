using System.Collections.Generic;
using System.Linq;

namespace Automation.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class Properties
    {
        private Dictionary<string, string> list;
        private string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="Properties"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public Properties(string file)
        {
            reload(file);
        }

        /// <summary>
        /// Gets the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="defValue">The definition value.</param>
        /// <returns></returns>
        public string get(string field, string defValue)
        {
            return (get(field) == null) ? (defValue) : (get(field));
        }

        /// <summary>
        /// Gets the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public string get(string field)
        {
            return (list.ContainsKey(field)) ? (list[field]) : (null);
        }

        /// <summary>
        /// Sets the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void set(string field, object value)
        {
            if (!list.ContainsKey(field))
                list.Add(field, value.ToString());
            else
                list[field] = value.ToString();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(filename);
        }

        /// <summary>
        /// Saves the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void Save(string filename)
        {
            this.filename = filename;

            if (!System.IO.File.Exists(filename))
                System.IO.File.Create(filename);

            var file = new System.IO.StreamWriter(filename);

            foreach (string prop in list.Keys.ToArray())
                if (!string.IsNullOrWhiteSpace(list[prop]))
                    file.WriteLine(prop + "=" + list[prop]);

            file.Close();
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void reload()
        {
            reload(filename);
        }

        /// <summary>
        /// Reloads the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void reload(string filename)
        {
            this.filename = filename;
            list = new Dictionary<string, string>();

            if (System.IO.File.Exists(filename))
                loadFromFile(filename);
            else
                System.IO.File.Create(filename);
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="file">The file.</param>
        private void loadFromFile(string file)
        {
            foreach (string line in System.IO.File.ReadAllLines(file))
            {
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    string key = line.Substring(0, index).Trim();
                    string value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore dublicates
                        list.Add(key, value);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}