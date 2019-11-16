using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Automation.Utility.FileUtility
{
    public class LogFileUtility
    {
        private static readonly SortedList<string, int> r_sequenceCount = new SortedList<string, int>();

        public static string GetNewFilePath(string folderPath, string fileNameStarts, char seperator, string filePathExtension, string categoryName)
        {
            if (!r_sequenceCount.ContainsKey(categoryName))
            {
                r_sequenceCount.Add(categoryName, 1);
            }

            string fileName = Path.GetFullPath($"{folderPath}{Path.DirectorySeparatorChar}{fileNameStarts}{seperator}{r_sequenceCount[categoryName]++}.{filePathExtension}");
            try
            {
                //Verifying if the file already exists, if so append the numbers 1,2  so on to the fine name.			

                var myPngImage = new FileInfo(fileName);
                if (!myPngImage.Directory.Exists)
                {
                    myPngImage.Directory.Create();
                }

                if (myPngImage.Exists)
                {
                    r_sequenceCount[categoryName] = myPngImage.Directory.GetFiles().Max(x => int.Parse(x.Name.Substring(0, x.Name.IndexOf('_')))) + 1;
                    fileName = Path.GetFullPath($"{folderPath}{Path.DirectorySeparatorChar}{fileNameStarts}{seperator}{r_sequenceCount[categoryName]++}.{filePathExtension}");
                    myPngImage = new FileInfo(fileName);
                }

                return fileName;
            }
            catch (Exception)
            {
               
            } 
            
            return fileName;
        }

        public static string GetOldFilePath(string folderPath, string fileNameStarts, char seperator, string filePathExtension, string categoryName)
        {
            if (!r_sequenceCount.ContainsKey(categoryName))
            {
                r_sequenceCount.Add(categoryName, 1);
            }

            var fileName = Path.GetFullPath($"{folderPath}{Path.DirectorySeparatorChar}{fileNameStarts}{seperator}{r_sequenceCount[categoryName]}.{filePathExtension}");

            var myPngImage = new FileInfo(fileName);

            if (myPngImage.Directory.Exists)
            {
                var lastCount= myPngImage.Directory.GetFiles().Max(x => int.Parse(x.Name.Substring(0, x.Name.IndexOf('_'))));
                return Path.GetFullPath($"{folderPath}{Path.DirectorySeparatorChar}{fileNameStarts}{seperator}{lastCount}.{filePathExtension}");
            }
            else
            {
                return null;
            }
        }
    }
}
