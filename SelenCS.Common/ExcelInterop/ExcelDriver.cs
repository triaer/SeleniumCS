using System.IO;

namespace SelenCS.Common.ExcelInterop
{
    public class ExcelDriver
    {
        public static ExcelHelper getExcelHelper(string filePath)
        {
            string fileType = getFileType(filePath);
            if (fileType == ".xlsx")
                return new New_ExcelHelper();
            return new Old_ExcelHelper(fileType);
        }

        private static string getFileType(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Extension.ToLower();
        }
    }
}
