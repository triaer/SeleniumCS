using Breeze.Common.ExcelInterop;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Breeze.Common.Helper
{
    public static class ExcelUtils
    {
        public static void CopyDataFromFileToFile(string excelFileContainPath, string excelFileToBeCopiedPath, string excelFileContainSheetName, string excelFileToBeCopiedSheetName, int rowToBeCopied, int rowContain)
        {
            var excelDriver1 = ExcelDriver.getExcelHelper(excelFileToBeCopiedPath);
            
            excelDriver1.LoadExcelSheetData(excelFileToBeCopiedPath, excelFileToBeCopiedSheetName);
            excelDriver1.GetAllValuesByRow(rowToBeCopied);
            string[] cellValue = excelDriver1.GetAllValuesByRow(rowToBeCopied).Split(',');
            var excelDriver2 = ExcelDriver.getExcelHelper(excelFileContainPath);

            for (int colIndex = 1; colIndex <= cellValue.Length; colIndex++)
            {
                excelDriver2.UpdateCellValue(excelFileContainPath, excelFileContainSheetName, rowContain, colIndex, cellValue[colIndex - 1]);
            }
            //excelDriver2.OpenExcelfileToView(excelFileContainPath, excelFileContainSheetName, 5);
            excelDriver2.Close();
        }

        public static void EditCellValueInFile(string filePath, string sheetName, string cellValueBeforeEdit, string cellValueAfterEdit)
        {
            var excelDriver = ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            excelDriver.Search(cellValueBeforeEdit);
            excelDriver.UpdateCellValue(filePath, sheetName, excelDriver.Search(cellValueBeforeEdit)[0], excelDriver.Search(cellValueBeforeEdit)[1], cellValueAfterEdit);
            excelDriver.Close();
        }

        public static void OpenFiletoView(string filePath, string sheetName, int timeout)
        {
            var excelDriver = ExcelDriver.getExcelHelper(filePath);
            excelDriver.OpenExcelFileToView(filePath, sheetName, timeout);
            excelDriver.Close();
        }               

        public static int GetNumberOfRows(string filePath, string sheetName)
        {
            var excelDriver = ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            int numberOfRows = excelDriver.GetTotalRows() - 1;
            excelDriver.Close();
            return numberOfRows;
        }

        public static string GetExcelRowValue(string filePath, string sheetName, int rowIndex) {
            var excelDriver = ExcelDriver.getExcelHelper(filePath);
            excelDriver.LoadExcelSheetData(filePath, sheetName);
            string rowValue = excelDriver.GetAllValuesByRow(rowIndex);
            excelDriver.Close();
            return rowValue;
        }

        public static string GetColumnNameByNumber(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = System.String.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = System.Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        public static int GetColumnNumberByName(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new System.ArgumentNullException("columnName");
            columnName = columnName.ToUpperInvariant();
            int sum = 0;
            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }
            return sum;
        }

        public static void InsertNewExcelReport(string filePath, string sheetName)
        {
            Process[] excelProcs = Process.GetProcessesByName("EXCEL");
            foreach (Process proc in excelProcs)
            {
                proc.Kill();
            }

            Application excelApp = new Application();
            object missValue = Type.Missing;
            Workbook wb = excelApp.Workbooks.Open(filePath,
                        missValue, missValue, missValue, missValue, missValue, missValue, missValue,
                        missValue, missValue, missValue, missValue, missValue, missValue, missValue);
            Worksheet ws = null;
            if (sheetName.Trim() == "")
                ws = wb.Worksheets[1];
            else
                ws = wb.Worksheets[sheetName];

            // Insert copied Range.
            Range range = ws.Columns["F:I"];
            range.Copy();
            range.Insert();

            // Update Date value.
            range = ws.Cells[1, 6];
            range.Value = String.Format("{0:m}", DateTime.Today);

            range = ws.Cells[7, 6];
            range.Value = String.Format("{0:m}", DateTime.Today);

            // Clear old data. Values below are fixed to Excel template.
            var startCellRow = 10;
            var startCellCol = 6;
            var endCellRow = 56;
            var endCellCol = 7;
            range = ws.Range[ws.Cells[startCellRow, startCellCol], ws.Cells[endCellRow, endCellCol]];

            for (int i = startCellCol; i < range.Columns.Count + startCellCol; i++)
            {
                for (int j = startCellRow; j < range.Rows.Count + startCellRow; j++)
                {
                    Range cell = ws.Cells[j, i];
                    if (i == 6)
                    {
                        cell.ClearContents();
                    }
                    else
                    {
                        // use for merged cells in template
                        cell.MergeArea.ClearContents();
                    }
                }
            }

            // Save changes then quit.
            wb.Save();

            excelApp.Quit();
            excelApp.Application.Quit();
            excelProcs = Process.GetProcessesByName("EXCEL");
            foreach (Process proc in excelProcs)
            {
                proc.Kill();
            }
        }
    }




}
