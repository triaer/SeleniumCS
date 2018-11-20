﻿using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace KiewitTeamBinder.Common
{
    public class TestAccountAccess
    {
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

        private static void CopyExcelDataFileToTemp(string sourceFilePath, string targetFilePath)
        {
            File.Copy(sourceFilePath, targetFilePath, true);
        }
    }

    public class TestAccount
    {
        public string kiewitUserName { get; set; }
        public string kiewitPassword { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }
}
