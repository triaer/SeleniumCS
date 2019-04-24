using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Breeze.Common
{
    public class TestAccountAccess
    {
        // Keep old method with 6 arguments for reference
        public static TestAccount GetTestAccount(string role, string environment, string type, string kwUserRole, string sourceFilePath, string localTempExcelUserTargetPath)
        {
            //Copy File and make sure it was successful before 
            CopyExcelDataFileToTemp(sourceFilePath, localTempExcelUserTargetPath);

            TestAccount user = null;
            string SheetName = "";
            string KWSheetName = "";
            //Create connection string to the excel document
            String strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;"
            + "Data Source='" + localTempExcelUserTargetPath + "';"
            + "Extended Properties='Excel 8.0;HDR=Yes'";

            //Declare the necessary Data Structures for the operation
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();
            System.Data.DataTable dtExcelSchema; //Must be specified as System.Data.DataTable because there is another DataTable in an imported class
            DataSet dataSet = new DataSet();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();

            //Set the connection for the command
            cmdExcel.Connection = connExcel;

            //Open the connection
            connExcel.Open();

            //Get the Tables from the excel file and put them into the DataTable
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();

            //Set up SQL like command
            SheetName = "TeamBinderAccounts$";
            cmdExcel.CommandText = "SELECT ROLE, ENVIRONMENT, DESCRIPTION, URL, USERNAME, COMPANY, PASSWORD, KIEWITUSER, NOTES From [" + SheetName + "] WHERE ROLE='" + role + "' AND ENVIRONMENT='" + environment + "'";
            //Use a DataAdapter and command to populate the DataSet
            dataAdapter.SelectCommand = cmdExcel;
            dataAdapter.Fill(dataSet, "Data");

            //Create another DataTable to hold the final data
            System.Data.DataTable dt = dataSet.Tables["Data"];

            //Assure that there is a row of data and then put that data into a user object
            if (dt.Rows.Count > 0)
            {
                user = new TestAccount
                {
                    Role = role,
                    Username = dt.Rows[0]["USERNAME"].ToString(),
                    Company = dt.Rows[0]["COMPANY"].ToString(),
                    Password = dt.Rows[0]["PASSWORD"].ToString(),
                    Url = dt.Rows[0]["URL"].ToString()
                };
            }
            //get the user info of Kiewit Account
            if (type == "KWUser")
            {
                KWSheetName = "In8Accounts$";
                cmdExcel.CommandText = "SELECT ROLE, USERNAME, PASSWORD From [" + KWSheetName + "] WHERE ROLE='" + kwUserRole + "'";
                dataAdapter.SelectCommand = cmdExcel;
                DataSet dataSet1 = new DataSet();
                dataAdapter.Fill(dataSet1, "Data");

                //Create another DataTable to hold the final data
                dt = dataSet1.Tables["Data"];
                if (dt.Rows.Count > 0)
                {
                    user.kiewitUserName = dt.Rows[0]["USERNAME"].ToString();
                    user.kiewitPassword = dt.Rows[0]["PASSWORD"].ToString();
                }
            }
            //Clean up resources to avoid conflicts in copying file if another search is performed
            dataAdapter.Dispose();
            cmdExcel.Dispose();
            connExcel.Close();


            return user;
        }

        public static TestAccount GetTestAccount(string filePath, string sheetName)
        {
            TestAccount user = null;
            
            // Create connection string to the excel document
            String strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;"
                                + @"Data Source='" + filePath + "';"
                                + @"Extended Properties='Excel 8.0; HDR=Yes'";

            /* The connection string below uses for Excel 2007 or later: 2010, 2013, 2016
             * However, it requires Microsoft Access Database Engine 2010
             * which can be downloaded at: https://www.microsoft.com/en-us/download/details.aspx?id=13255
             * Actually, for this specific requirement (getting test account stored in excel file), it's
             * fine to use old version excel file *.xls
            
             String strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;"
                                + @"Data Source='" + filePath + "';"
                                + @"Extended Properties='Excel 8.0; HDR=Yes'";
            */

            // Declare the necessary Data Structures for the operation
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();
            DataTable dtExcelSchema; 
            DataSet dataSet = new DataSet();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();

            // Set the connection for the command
            cmdExcel.Connection = connExcel;

            // Open the connection
            connExcel.Open();

            // Get the Tables from the excel file and put them into the DataTable
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            
            // Set up SQL like command
            var sheet = sheetName + "$";
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";

            /* It's able to filter the queried table, using WHERE
             * Example:
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "] WHERE ROLE='" + role + "'";
            */

            // Use a DataAdapter and command to populate the DataSet
            dataAdapter.SelectCommand = cmdExcel;
            dataAdapter.Fill(dataSet, "Data"); // name the queried dataset as 'Data'

            // Create another DataTable to hold the final data
            System.Data.DataTable dt = dataSet.Tables["Data"];

            // Assure that there is a row of data and then put that data into a user object
            if (dt.Rows.Count > 0)
            {
                user = new TestAccount
                {
                    Username = dt.Rows[0]["USERNAME"].ToString(),
                    Password = dt.Rows[0]["PASSWORD"].ToString(),
                    Role = dt.Rows[0]["ROLE"].ToString(),
                };
            }
            
            // Clean up resources to avoid conflicts in copying file if another search is performed
            dataAdapter.Dispose();
            cmdExcel.Dispose();
            connExcel.Close();
            
            return user;
        }

        private static void CopyExcelDataFileToTemp(string sourceFilePath, string targetFilePath)
        {
            File.Copy(sourceFilePath, targetFilePath, true);
        }
    }

    
    public class TestAccount
    {
        /* This class also describes the template of target excel file. 
         * Each property should be mapped to a header column within the file */
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }

        public string Company { get; set; }
        public string kiewitUserName { get; set; }
        public string kiewitPassword { get; set; }
        public string Url { get; set; }


    }
}
