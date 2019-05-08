using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Breeze.Common.ExcelInterop
{
    public class New_ExcelHelper : ExcelHelper
    {
        /*
        This class support for working with Excel 2007 and newer (.XLSX)
        */

        public ExcelWorksheet workSheet;
        public Application excel = new Application();
        private Workbook wb = null;
        private Worksheet ws = null;
        object missValue;

        public New_ExcelHelper() { }

        public void LoadExcelSheetData(string filePath, string sheetName) //worked
        {
            // This method uses OfficeOpenXML.ExcelPackage
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

        private void LoadExcelSheet(string filePath, string sheetName)
        {
            // This method uses Microsoft.Office.Interop.Excel;
            try
            {
                if (File.Exists(filePath))
                {
                    missValue = System.Reflection.Missing.Value;
                    wb = excel.Workbooks.Open(filePath,
                        missValue, missValue, missValue, missValue, missValue, missValue, missValue,
                        missValue, missValue, missValue, missValue, missValue, missValue, missValue);
                    if (sheetName.Trim() == "")
                        ws = wb.Worksheets[1];
                    else
                        ws = wb.Worksheets[sheetName];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Close();
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

        public string GetCellValue(int intRow, int intColumn)
        {
            try
            {
                return workSheet.Cells[intRow, intColumn].Value.ToString();
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                return null;
            }
        }

        
        public int[] Search(string strKeyword, bool blnCaseSensitive = true)
        {
            // search for the first cell that has exact keyword
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
        
        public List<int[]> SearchAll(string strKeyword, bool caseSensitive = true, bool partialSearch = true)
        {
            // caseSensitive = TRUE: search for all cells that has exact keyword
            // partialSearch = TRUE: search for all cells that contain keyword partially.
            List<int[]> results = new List<int[]>();
            try
            {
                if (!caseSensitive) strKeyword = strKeyword.ToLower();
                ExcelAddressBase oRange = workSheet.Dimension;
                int rowCount = oRange.End.Row;
                int colCount = oRange.End.Column;

                if (partialSearch == false)
                {
                    for (int i = 1; i <= rowCount; i++)
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (workSheet.Cells[i, j].Value == null) continue;

                            string strCell = workSheet.Cells[i, j].Value.ToString();
                            if ((caseSensitive && strCell.Equals(strKeyword)) ||
                                    (!caseSensitive && strCell.ToLower().Equals(strKeyword)))
                                results.Add(new int[2] { i, j });
                        }
                }
                else
                {
                    for (int i = 1; i <= rowCount; i++)
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (workSheet.Cells[i, j].Value == null) continue;

                            string strCell = workSheet.Cells[i, j].Value.ToString();
                            if ((caseSensitive && strCell.Contains(strKeyword)) ||
                                    (!caseSensitive && strCell.ToLower().Contains(strKeyword)))
                                results.Add(new int[2] { i, j });
                        }
                }
                if (results == null)
                {
                    results.Add(new int[2] { -1, -1 });
                }
                return results;
            }
            catch (Exception)
            {
                excel.Application.Quit();
                excel.Quit();
                results.Add(new int[2] { -1, -1 });
                return results;
            }
        }
        
        public void Close()
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

        public void PrintWorksheet() 
        {
            // print workSheet content
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
        
        public void InsertRow(string filePath, string sheetName, int fromRow = 1, int noRowsToInsert = 1)
        {   
            Range line = (Range)ws.Rows[fromRow];
            for (int i = 1; i <= noRowsToInsert; i++)
                line.Insert();
            wb.Save();
            this.Close();
        }
        
        public void InsertColumn(string filePath, string sheetName, int fromColumn = 1, int noColumnsToInsert = 1)
        {
            Range col = (Range)ws.Columns[fromColumn];
            for (int i = 1; i <= noColumnsToInsert; i++)
                col.Insert();
            wb.Save();
            this.Close();
        }

        /*public void DeleteRow(string filePath, string sheetName, int fromRow = 1, int noRowsToDelete = 1)
        {
            Range line = (Range)GetWorkSheet(filePath, sheetName).Rows[fromRow];
            for (int i = 1; i <= noRowsToDelete; i++)
                line.Delete(XlDeleteShiftDirection.xlShiftUp);
            wb.Save();
            this.Close();
        }*/

        /*public void DeleteColumn(string filePath, string sheetName, int fromColumn = 1, int noColumnsToDelete = 1)
        {
            Range col = (Range)GetWorkSheet(filePath, sheetName).Columns[fromColumn];
            for (int i = 1; i <= noColumnsToDelete; i++)
            {
                //col.Delete(Type.Missing);
                col.EntireColumn.Delete();
            }
            wb.Save();
            this.Close();
        }*/

        private Range GetRange(string rangeType, object rangeDefine)
        {
            /*
             * From cell to cell ---
             * Range range = ws.Range[ws.Cells[2, 2], ws.Cells[5, 6]];
             * 
             * For Rows ---
             * Range range = ws.Rows[6];
             * Range range = ws.Rows["2:4"];
             * 
             * For Columns ---
             * Range range = ws.Columns["B"];
             * Range range = ws.Columns[3];
             * Range range = ws.Columns["F:I"];
             * 
             * For Multiple range
             * Range range = ws.Range["7:9,12:12,14:14"];
             * Range range = ws.Range["B:C,E:E,G:G"];
            */

            Range range = ws.Cells[1, 1]; // default range sets to cell A1

            try
            {
                switch (rangeType) {
                    case "Range": range = ws.Range[rangeDefine]; break;
                    case "Columns":  range = ws.Columns[rangeDefine]; break;
                    case "Rows":  range = ws.Rows[rangeDefine]; break;
                    default: Console.WriteLine("Invalid input!");  break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return range;
        }
        
        private void FormatRange (Range range)
        {
            //range.NoteText("This is a Formatting test", Type.Missing, Type.Missing);
            //range.Font.Name = "Verdana";
            //range.VerticalAlignment = XlVAlign.xlVAlignCenter;
            //range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //range.BorderAround(Type.Missing, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, Type.Missing);
            //range.AutoFormat(XlRangeAutoFormat.xlRangeAutoFormat3DEffects1, true, false, true, false, true, true);
            //range.Value = "test";
            //range.Font.ThemeColor = ColorTranslator.ToOle(Color.White);
            //range.Font.Color = ColorTranslator.ToOle(Color.Black);
            //range.Font.Bold = true;
            //range.Font.Italic = true;
            //range.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
        }
    }
}
