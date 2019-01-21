using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using System.Reflection;

namespace KiewitTeamBinder.Common.Helper
{
    public static class Utils
    {
        public static bool DictEquals(Dictionary<string, string> dict1, Dictionary<string, string> dict2)
        {
            bool result = true;
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var key in dict1.Keys)
            {
                if (!dict1[key].Equals(dict2[key]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static int RefactorIndex(int index)
        {
            if (index <= 0)
                return 0;
            return index - 1;
        }

        public static string GetRandomValue(string value)
        {
            value = string.Format("{0}_{1}", value.Replace(' ', '_'), DateTime.Now.ToString("yyyyMMddhhmmss"));
            return value;
        }

        public static int GetRandomNumber(int min, int max)
        {
            Thread.Sleep(100);
            Random rnum = new Random();
            return rnum.Next(min, max);
        }

        public static List<KeyValuePair<string, bool>> AddCollectionToCollection(List<KeyValuePair<string, bool>> collectionContain, List<KeyValuePair<string, bool>> collectionAdded)
        {
            foreach (var item in collectionAdded)
            {
                collectionContain.Add(item);
            }

            collectionAdded = new List<KeyValuePair<string, bool>>();
            return collectionContain;
        }

        public static string GetLatestFileName(string directory)
        {
            var files = new DirectoryInfo(directory).GetFiles("*.*");
            string latestFile = "";

            DateTime lastModified = DateTime.MinValue;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastModified)
                {
                    lastModified = file.LastWriteTime;
                    latestFile = file.Name;
                }
            }
            return latestFile;
        }

        public static string[] SplitArrayofString(string[] arrayValue, char separator = '-')
        {
            string[] outItem = new string[arrayValue.Length];
            for (int i = 0; i < arrayValue.Length; i++)
            {
                string[] item = new string[2];
                item = arrayValue[i].Split(separator);
                outItem[i] = item[0].Trim();
            }
            return outItem;
        }

        public static string ReportFailureOfValidationPoints(string verifiedPoint, string expectedValue, string actualValue)
        {
            string outMessage = verifiedPoint + " - Expected Value: " + expectedValue + " ,Actual Value: " + actualValue;
            return outMessage;
        }

        public static string ReportExceptionInValidation(string verifiedPoint, Exception e)
        {
            string outMessage = verifiedPoint + " Failed With Exception - " + e.Message + " " + e.StackTrace;
            return outMessage;
        }

        public static string GetProjectPath(bool includeBinFolder = false)
        {
            //string path_old = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            string path = Directory.GetCurrentDirectory() + "\\";
            string actualPath = path;
            if (!includeBinFolder)
            {
                actualPath = path.Substring(0, path.LastIndexOf("bin"));
            }
            string projectPath = new Uri(actualPath).LocalPath; // project path of your solution
            return projectPath;
        }

        public static string ImageToBase64(string imagePath)
        {
            string base64String;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static string GetUserDefaultDownloadsPath()
        {
            if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();
            IntPtr pathPtr = IntPtr.Zero;
            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }

        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]

        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        public static DateTime ToDateTime(this GetDateTime option, int days = 0)
        {
            {
                switch (option)
                {
                    case GetDateTime.TODAY: return DateTime.Now;
                    case GetDateTime.YESTERDAY: return DateTime.Now.AddDays(-1);
                    case GetDateTime.N_DAYS_AGO: return DateTime.Now.AddDays(-days);
                    case GetDateTime.TOMORROW: return DateTime.Now.AddDays(1);
                    default: return DateTime.Now;
                }

            }
        }

        public static string ToDescription<T>(this T val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }

        public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount = 1)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
        }

        public static void CreateSampleTextFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                using (TextWriter tw = new StreamWriter(filePath))
                {
                    tw.WriteLine("This is a sample text file for testing!");
                    tw.Close();
                }
            }
        }

        public static void DeleteSampleTextFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
            
        }

        public static string GetInputFilesLocalPath([CallerMemberName]string memberName = "")
        {
            var path = GetProjectPath(true) + string.Format(@"ScenariosInputFiles\{0}", memberName);
            return path;
        }
    }
}
