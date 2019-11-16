using System.IO;

namespace Automation.Utility.FileUtility
{
    public class FileManager
    {

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            Copy(sourceDirectory, targetDirectory, string.Empty);
        }

        public static void Copy(string sourceDirectory, string targetDirectory, string prefix)
        {
            if (Directory.Exists(sourceDirectory))
            {
                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
                CopyAll(diSource, diTarget,prefix);
            }
        }      

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target, string prefix)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (var fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, prefix + fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, prefix);
            }
        }

    }
}
