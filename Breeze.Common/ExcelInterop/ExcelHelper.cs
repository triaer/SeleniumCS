using System;
using System.Collections.Generic;

namespace Breeze.Common.ExcelInterop
{
    public interface ExcelHelper
    {
        void LoadExcelSheetData(string filePath, string sheetName);
        void OpenExcelFileToView(string filePath, string sheetName, int timeout);
        string GetAllValue();
        int GetTotalRows();
        int GetTotalColumns();
        string GetAllValuesByRow(int rowIndex);
        string GetCellValue(int intRow, int intColumn);
        void UpdateCellValue(string filePath, string sheetName, int rowIndex, int colIndex, string cellValue);
        int[] Search(string strKeyword, Boolean blnCaseSensitive = true);
        List<int[]> SearchAll(string strKeyword, bool caseSensitive = true, bool partialSearch = true);
        void InsertRow(string filePath, string sheetName, int fromRow, int noOfRows = 1);
        void InsertColumn(string filePath, string sheetName, int fromColumn, int noOfColumns = 1);
        //void DeleteRow(string filePath, string sheetName, int fromRow, int noOfRows = 1);
        //void DeleteColumn(string filePath, string sheetName, int fromCol, int noOfColumns = 1);
        void Close();
    }
}
