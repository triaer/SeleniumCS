using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace KiewitTeamBinder.Common
{
    public class ServiceInfoAccess
    {
        public static ServiceInfo GetServiceInfo(string serviceName, string sourceFilePath, string localTempExcelUserTargetPath)
        {
            //Copy File and make sure it was successful before 
            CopyExcelDataFileToTemp(sourceFilePath, localTempExcelUserTargetPath);

            ServiceInfo info = null;
            string SheetName = "";
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
            SheetName = "ServiceInfo$";
            cmdExcel.CommandText = "SELECT SERVICENAME, URL, ENDPOINTNAME From [" + SheetName + "] WHERE SERVICENAME='" + serviceName + "'";
            //Use a DataAdapter and command to populate the DataSet
            dataAdapter.SelectCommand = cmdExcel;
            dataAdapter.Fill(dataSet, "Data");

            //Create another DataTable to hold the final data
            System.Data.DataTable dt = dataSet.Tables["Data"];

            //Assure that there is a row of data and then put that data into a user object
            if (dt.Rows.Count > 0)
            {
                info = new ServiceInfo
                {
                    ServiceName = serviceName,
                    EndpointName = dt.Rows[0]["ENDPOINTNAME"].ToString(),
                    Url = dt.Rows[0]["URL"].ToString()
                };
            }
            
            //Clean up resources to avoid conflicts in copying file if another search is performed
            dataAdapter.Dispose();
            cmdExcel.Dispose();
            connExcel.Close();

            return info;
        }

        private static void CopyExcelDataFileToTemp(string sourceFilePath, string targetFilePath)
        {
            File.Copy(sourceFilePath, targetFilePath, true);
        }
    }

    public class ServiceInfo
    {
        public string ServiceName { get; set; }
        public string EndpointName { get; set; }
        public string Url { get; set; }
    }
}
