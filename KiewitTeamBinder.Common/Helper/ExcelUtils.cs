using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;

namespace KiewitTeamBinder.Common.Helper
{
    public static class ExcelUtils
    {
        public static void CopyDataFromExcelFileToExcelFile(string excelFileContainPath, string excelFileToBeCopiedPath, string excelFileContainSheetName, string excelFileToBeCopiedSheetName, int rowToBeCopied, int rowContain)
        {
            var excelDriver1 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileToBeCopiedPath);
            excelDriver1.Open(excelFileToBeCopiedPath, excelFileToBeCopiedSheetName);
            excelDriver1.GetAllExcelRowsValue(rowToBeCopied);
            string[] cellValue = excelDriver1.GetAllExcelRowsValue(rowToBeCopied).Split(',');
            var excelDriver2 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileContainPath);

            for (int colIndex = 1; colIndex <= cellValue.Length; colIndex++)
            {
                excelDriver2.WriteDataToExcelFile(excelFileContainPath, excelFileContainSheetName, rowContain, colIndex, cellValue[colIndex - 1]);
            }
            //excelDriver2.OpenExcelfileToView(excelFileContainPath, excelFileContainSheetName, 5);
            excelDriver2.Close();
        }

        public static void EditCellValueInExcelFile(string filePath, string sheetName, string cellValueBeforeEdit, string cellValueAfterEdit)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.Open(filePath, sheetName);
            excelDriver.Search(cellValueBeforeEdit);
            excelDriver.WriteDataToExcelFile(filePath, sheetName, excelDriver.Search(cellValueBeforeEdit)[0], excelDriver.Search(cellValueBeforeEdit)[1], cellValueAfterEdit);
            excelDriver.Close();
        }

        public static void OpenExcelFiletoView(string filePath, string sheetName, int timeout)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.OpenExcelfileToView(filePath, sheetName, timeout);
            excelDriver.Close();
        }

        public static void OpenExcelFile(string filePath, string sheetName)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.Open(filePath, sheetName);
        }

        public static void CloseExcelFile(string filePath)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.Close();
        }

        public static int GetNumberOfRows(string filePath, string sheetName)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.Open(filePath, sheetName);
            return excelDriver.GetExcelTotalRows();
        }
    }
}
