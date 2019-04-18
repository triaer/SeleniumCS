using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.Common;

namespace Breeze.Common.Helper
{
    public static class ExcelUtils
    {
        public static void CopyDataFromFileToFile(string excelFileContainPath, string excelFileToBeCopiedPath, string excelFileContainSheetName, string excelFileToBeCopiedSheetName, int rowToBeCopied, int rowContain)
        {
            var excelDriver1 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileToBeCopiedPath);
            excelDriver1.LoadExcelSheetData(excelFileToBeCopiedPath, excelFileToBeCopiedSheetName);
            excelDriver1.GetAllValuesByRow(rowToBeCopied);
            string[] cellValue = excelDriver1.GetAllValuesByRow(rowToBeCopied).Split(',');
            var excelDriver2 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileContainPath);

            for (int colIndex = 1; colIndex <= cellValue.Length; colIndex++)
            {
                excelDriver2.UpdateCellValue(excelFileContainPath, excelFileContainSheetName, rowContain, colIndex, cellValue[colIndex - 1]);
            }
            //excelDriver2.OpenExcelfileToView(excelFileContainPath, excelFileContainSheetName, 5);
            excelDriver2.Close();
        }

        public static void EditCellValueInFile(string filePath, string sheetName, string cellValueBeforeEdit, string cellValueAfterEdit)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            excelDriver.Search(cellValueBeforeEdit);
            excelDriver.UpdateCellValue(filePath, sheetName, excelDriver.Search(cellValueBeforeEdit)[0], excelDriver.Search(cellValueBeforeEdit)[1], cellValueAfterEdit);
            excelDriver.Close();
        }

        public static void OpenFiletoView(string filePath, string sheetName, int timeout)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.OpenExcelFileToView(filePath, sheetName, timeout);
            excelDriver.Close();
        }               

        public static int GetNumberOfRows(string filePath, string sheetName)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            int numberOfRows = excelDriver.GetTotalRows() - 1;
            excelDriver.Close();
            return numberOfRows;
        }

        public static string GetExcelRowValue(string filePath, string sheetName, int rowIndex) {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            string rowValue = excelDriver.GetAllValuesByRow(rowIndex);
            excelDriver.Close();
            return rowValue;
        }

    }
}
