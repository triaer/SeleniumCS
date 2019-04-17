using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Common.ExcelInterop
{
    public interface ExcelHelper
    {
        void LoadExcelSheetData(string filePath, string sheetName);
        string GetAllValue();
        int GetTotalRows();
        int GetTotalColumns();
        string GetAllValuesByRow(int rowIndex);
        void UpdateCellValue(string filePath, string sheetName, int rowIndex, int colIndex, string cellValue);
        void OpenExcelFileToView(string filePath, string sheetName, int timeout);
        string GetCellValue(int intRow, int intColumn);
        int[] Search(string strKeyword, Boolean blnCaseSensitive = true);
        void Close();
    }
}
