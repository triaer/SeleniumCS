using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Breeze.Common.ExcelInterop
{
    class New_ExcelHelper : ExcelHelper
    {
        /*
        This class support for working with Excel 2007 and newer (.XLSX)
        */

        public ExcelWorksheet workSheet;
        private Application excel = new Application();

        public New_ExcelHelper() { }

        public void LoadExcelSheetData(string filePath, string sheetName) //worked
        {
            try
            {
                ExcelPackage package = new ExcelPackage(new FileInfo(filePath));
                if (sheetName.Trim() == "")
                    workSheet = package.Workbook.Worksheets[1];
                else
                    workSheet = package.Workbook.Worksheets[sheetName];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                excel.Application.Quit();
                excel.Quit();
            }
        }

        public string GetAllValue() // worked
        {
            string strCellValue = "";

            ExcelAddressBase oRange = workSheet.Dimension;
            int rowCount = oRange.End.Row;
            int colCount = oRange.End.Column;

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    try
                    {
                        strCellValue += workSheet.Cells[i, j].Value.ToString() + ",";
                    }
                    catch (NullReferenceException)
                    {
                        strCellValue += "" + ",";
                    }

                }
            }
            strCellValue = strCellValue.Remove(strCellValue.Length - 1);
            return strCellValue;
        }

        public string GetAllValuesByRow(int rowIndex) // worked
        {
            string strCellValue = "";
            try
            {
                ExcelAddressBase oRange = workSheet.Dimension;
                int rowCount = oRange.End.Row;
                int colCount = oRange.End.Column;
                if (rowIndex > rowCount)
                    return strCellValue;
                else
                {
                    for (int colIndex = 1; colIndex <= colCount; colIndex++)
                    {
                        try
                        {
                            string value = workSheet.Cells[rowIndex, colIndex].Value.ToString();
                            if (value == "")
                                value = value + " ";
                            strCellValue += value + " ";
                        }
                        catch (NullReferenceException)
                        {
                            strCellValue += " ";
                        }

                    }
                }

                strCellValue = strCellValue.Remove(strCellValue.Length - 1);
                return strCellValue;
            }

            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                return strCellValue;
            }
        }

        public void UpdateCellValue(string filePath, string sheetName, int rowIndex, int colIndex, string cellValue)
        {
            var workBooks = excel.Workbooks;
            Workbook workBook = workBooks.Open(filePath, ReadOnly: false, Editable: true);
            Worksheet workSheet;
            if (sheetName.Trim() == "")
                workSheet = workBook.Worksheets[1];
            else
                workSheet = workBook.Sheets[sheetName];
            try
            {
                if (workSheet == null)
                    return;

                Range cell = workSheet.Rows.Cells[rowIndex, colIndex];
                cell.Value = cellValue;

                excel.Application.ActiveWorkbook.Save();
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
            }
            finally
            {
                if (workSheet != null) Marshal.ReleaseComObject(workSheet);
                if (workBook != null) Marshal.ReleaseComObject(workBook);
                if (workBooks != null) Marshal.ReleaseComObject(workBooks);
            }

        }
        //worked

        public void OpenExcelFileToView(string filePath, string sheetName, int timeout) // worked
        {
            var workBooks = excel.Workbooks;
            Workbook workBook = workBooks.Open(filePath);
            Worksheet workSheet;
            if (sheetName.Trim() == "")
                workSheet = workBook.Worksheets[1];
            else
                workSheet = workBook.Sheets[sheetName];

            try
            {
                workSheet.Activate();
                excel.Visible = true;
                Thread.Sleep(timeout * 1000);
                excel.Visible = false;
                //excel.Application.Quit();
                //excel.Quit();
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
            }
            finally
            {
                if (workSheet != null) Marshal.ReleaseComObject(workSheet);
                if (workBook != null) Marshal.ReleaseComObject(workBook);
                if (workBooks != null) Marshal.ReleaseComObject(workBooks);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public int GetTotalRows() //get number of Rows
        {
            try
            {
                ExcelAddressBase oRange = workSheet.Dimension;
                return oRange.End.Row;
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                return 0;
            }
        }

        public int GetTotalColumns()
        {
            try
            {
                ExcelAddressBase oRange = workSheet.Dimension;
                return oRange.End.Column;
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                return 0;
            }
        } // get number of Columns

        public string GetCellValue(int intRow, int intColumn) // worked
        {
            try
            {
                return workSheet.Cells[intRow, intColumn].Value.ToString();
            }
            catch (Exception e)
            {
                excel.Application.Quit();
                excel.Quit();
                return null;
            }
        }

        // search for the first cell that has exact keyword
        public int[] Search(string strKeyword, bool blnCaseSensitive = true)
        {
            try
            {
                if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();

                ExcelAddressBase oRange = workSheet.Dimension;
                int rowCount = oRange.End.Row;
                int colCount = oRange.End.Column;

                for (int i = 1; i <= rowCount; i++)
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (workSheet.Cells[i, j].Value == null) continue;

                        string strCell = workSheet.Cells[i, j].Value.ToString();
                        if ((blnCaseSensitive && strCell.Equals(strKeyword)) ||
                                (!blnCaseSensitive && strCell.ToLower().Equals(strKeyword)))
                            return new int[2] { i, j };
                    }
                return new int[2] { -1, -1 };
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                return new int[2] { -1, -1 };
            }
        }

        // search for all cells that has exact keyword
        public List<string> SearchAll(string strKeyword, bool blnCaseSensitive = true)
        {
            List<string> results = new List<string>();
            try
            {
                if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();
                ExcelAddressBase oRange = workSheet.Dimension;
                int rowCount = oRange.End.Row;
                int colCount = oRange.End.Column;
                for (int i = 1; i <= rowCount; i++)
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (workSheet.Cells[i, j].Value == null) continue;

                        string strCell = workSheet.Cells[i, j].Value.ToString();
                        if ((blnCaseSensitive && strCell.Equals(strKeyword)) ||
                                (!blnCaseSensitive && strCell.ToLower().Equals(strKeyword)))
                            results.Add(i + ":" + j);
                    }
                if (results == null)
                {
                    results.Add("-1:-1");
                }
                return results;
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                results.Add("-1:-1");
                return results;
            }
        }

        // search for all cells that contain keyword partially.
        public List<string> SearchAllCellsContain(string strKeyword, bool blnCaseSensitive = true)
        {
            List<string> results = new List<string>();
            try
            {
                if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();
                ExcelAddressBase oRange = workSheet.Dimension;
                int rowCount = oRange.End.Row;
                int colCount = oRange.End.Column;
                for (int i = 1; i <= rowCount; i++)
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (workSheet.Cells[i, j].Value == null) continue;

                        string strCell = workSheet.Cells[i, j].Value.ToString();
                        if ((blnCaseSensitive && strCell.Contains(strKeyword)) ||
                                (!blnCaseSensitive && strCell.ToLower().Contains(strKeyword)))
                            results.Add(i + ":" + j);
                    }
                if (results == null)
                {
                    results.Add("-1:-1");
                }
                return results;
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                results.Add("-1:-1");
                return results;
            }
        }

        public void Close() // worked
        {
            excel.Application.Quit();
            excel.Quit();
            Process[] excelProcs = Process.GetProcessesByName("EXCEL");
            foreach (Process proc in excelProcs)
            {
                proc.Kill();
            }
            if (excel != null) Marshal.ReleaseComObject(excel);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void PrintWorksheet() // print workSheet content
        {
            var rows = GetTotalRows();
            var cols = GetTotalColumns();
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    var cell = workSheet.Cells[i, j];
                    Console.Write(cell.Value.ToString().Trim() + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
