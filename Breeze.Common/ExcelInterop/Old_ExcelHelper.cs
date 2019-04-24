using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Breeze.Common.ExcelInterop
{
    public class Old_ExcelHelper : ExcelDriver
    {
        /*
         This class support for working with Excel 97-2003 (.XLS) and CSV format 
        */
        public string fileType;
        private Application excel;
        public System.Data.DataTable table;

        public Old_ExcelHelper() { }

        public Old_ExcelHelper(string fileType)
        {
            this.fileType = fileType;
        }

        public void LoadExcelSheetData(string filePath, string sheetName) // load all data from a sheet specified by sheetName
        {
            if (fileType == ".csv")
                table = ConvertCSVtoDataTable(filePath);
            else
                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet();
                            if (sheetName.Trim() == "")
                                table = result.Tables[0];
                            else
                                table = result.Tables[sheetName.Trim()];
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
        }

        public string GetAllValue() // worked
        {
            string strCellValue = "";


            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    strCellValue += table.Rows[i][j].ToString() + ",";

            strCellValue = strCellValue.Remove(strCellValue.Length - 1);

            return strCellValue;
        }

        public string GetAllValuesByRow(int rowIndex) // get all values by Row index
        {
            string strCellValue = "";

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            if (rowIndex > rowCount)
                return strCellValue;
            else
            {
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                    strCellValue += table.Rows[rowIndex][colIndex].ToString() + " ";

                strCellValue = strCellValue.Remove(strCellValue.Length - 1);

                return strCellValue;
            }
        }

        public void UpdateCellValue(string filePath, string sheetName, int rowIndex, int colIndex, string cellValue)
        {
            //Not implement for .csv file
        }

        public void OpenExcelFileToView(string filePath, string sheetName, int timeout) // worked
        {
            excel = new Application();
            var workBooks = excel.Workbooks;
            Workbook workBook = workBooks.Open(filePath, ReadOnly: false, Editable: true);
            Worksheet workSheet;
            if (sheetName.Trim() == "")
                workSheet = workBook.Worksheets[1];
            else
                workSheet = workBook.Sheets[sheetName];

            try
            {
                excel.Visible = true;
                workSheet.Activate();

                Thread.Sleep(timeout * 1000);
                excel.Visible = false;
                // workBook.Close();
                //excel.Application.Quit();
                // excel.Quit();
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

        public void Close() //worked
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

        public int GetTotalRows() // get total number of Rows
        {
            return table.Rows.Count;
        }

        public int GetTotalColumns()  // get total number of Columns
        {
            return table.Columns.Count;
        }

        public string GetCellValue(int intRow, int intColumn) // worked
        {
            try
            {
                string cellValue = null;
                cellValue = table.Rows[intRow - 1][intColumn - 1].ToString();
                return cellValue;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        // search for the first cell that has exact keyword
        public int[] Search(string strKeyword, bool blnCaseSensitive = true)
        {
            if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    string strCell = table.Rows[i][j].ToString();
                    if ((blnCaseSensitive && strCell.Equals(strKeyword)) ||
                            (!blnCaseSensitive && strCell.ToLower().Equals(strKeyword)))
                        return new int[2] { i + 1, j + 1 };

                }
            return new int[2] { -1, -1 };
        }

        // search for the first cell that has exact keyword
        public List<string> SearchAll(string strKeyword, bool blnCaseSensitive = true)
        {
            List<string> results = new List<string>();
            if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    string strCell = table.Rows[i][j].ToString();
                    if ((blnCaseSensitive && strCell.Equals(strKeyword)) ||
                            (!blnCaseSensitive && strCell.ToLower().Equals(strKeyword)))
                        results.Add((i + 1) + ":" + (j + 1));

                }
            if (results == null)
            {
                results.Add("-1:-1");
            }
            return results;
        }

        // search for all cells that contain keyword partially.
        public List<string> SearchAllCellsContain(string strKeyword, bool blnCaseSensitive = true)
        {
            List<string> results = new List<string>();
            if (!blnCaseSensitive) strKeyword = strKeyword.ToLower();

            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    string strCell = table.Rows[i][j].ToString();
                    if ((blnCaseSensitive && strCell.Contains(strKeyword)) ||
                            (!blnCaseSensitive && strCell.ToLower().Contains(strKeyword)))
                        results.Add((i + 1) + ":" + (j + 1));
                }
            if (results == null)
            {
                results.Add("-1:-1");
            }
            return results;
        }

        private System.Data.DataTable ConvertCSVtoDataTable(string filePath) // worked: with CSV format "a,b,c,d"
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Get first row - header
                string[] headers = sr.ReadLine().Split(',');
                DataRow firstRow = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dt.Columns.Add("Header " + i.ToString());
                    firstRow[i] = headers[i].Trim();
                }
                dt.Rows.Add(firstRow);

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                            dr[i] = rows[i].Trim();
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        public void PrintDataTable()
        {
            for (int i = 0; i <= table.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= table.Columns.Count - 1; j++)
                {
                    var cell = table.Rows[i][j];
                    Console.Write(cell.ToString() + "\t");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
