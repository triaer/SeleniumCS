using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Api.Service
{
    public class MailApi
    {
        #region Entities
        private static string ServiceName = "/mail.asmx";
        private static string EndpointName = "MailWebServiceSoap";
        private MailServiceReference.MailWebServiceSoapClient _request;
        //private AddressBookServiceReference.AddressBookSoapClient _addressRequest;
        #endregion

        #region Actions

        public MailApi(string url)
        {
            _request = new MailServiceReference.MailWebServiceSoapClient(EndpointName, url + ServiceName);
        }

 
        public DataSet GetStructureForComposeMail(string sessionKey, int parentMailIntKey, string parentMailBox, string mailType, string composeMailAction)
        {

            DataSet dataSetResponse = _request.GetStructureForComposeMail(sessionKey, parentMailIntKey, parentMailBox, mailType, composeMailAction);
            return dataSetResponse;
        }

        public DataSet GetMailDetails(string sessionKey, string mailBox, int mailIntKey)
        {
            DataSet dataSetResponse = _request.GetMailDetails(sessionKey, mailBox, mailIntKey);
            return dataSetResponse;
        }

        public KeyValuePair<string, bool> ValidateEmailsInMailBox(DataSet dataSetResponse, bool isExisted)
        {
            try
            {
                int expectedNumberOfTable;
                string message;
                if (isExisted)
                {
                    expectedNumberOfTable = 4;
                    message = Validation.Emails_In_Mail_Box;
                }
                else
                {
                    expectedNumberOfTable = 1;
                    message = Validation.Emails_Not_In_Mail_Box;
                }
                    

                int numberOfTable = dataSetResponse.Tables.Count;
                if (numberOfTable == expectedNumberOfTable)
                    return new KeyValuePair<string, bool>(message, true);
                return new KeyValuePair<string, bool>(message, false);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Emails_In_Mail_Box + ", Error: " + e, false);
            }
        }

        public DataTable ListMail(string sessionKey, string mailBox, string[] fieldNamesWithValues = null, string[] dateTimeFieldNamesWithValues = null, int mailAccessLevel = 1)
        {
            string orderBy = "Subject";
            int startRowPosition = 0;
            int noOfRows = 100;

            string mailFilter = "{";

            if (fieldNamesWithValues == null)
            {
                fieldNamesWithValues = new string[0];
            }
            if (fieldNamesWithValues.Length >= 0)
            {
                mailFilter += "'fMailFilter3': [ ";
                foreach (var fieldNameWithValue in fieldNamesWithValues)
                {
                    mailFilter += fieldNameWithValue + ",";
                }
                mailFilter = mailFilter.Remove(mailFilter.Length - 1);
                mailFilter += "],";
            }

            if (dateTimeFieldNamesWithValues == null)
            {
                dateTimeFieldNamesWithValues = new string[0];
            }

            if (dateTimeFieldNamesWithValues.Length >= 0)
            {
                //mailFilter += "'fMailFilter4': [";
                //foreach (var dateTimeFieldNameWithValue in dateTimeFieldNamesWithValues)
                //{
                //    mailFilter += dateTimeFieldNameWithValue + ",";
                //}
                //mailFilter = mailFilter.Remove(mailFilter.Length - 1);
                //mailFilter += "],";
            }
            mailFilter = mailFilter.Remove(mailFilter.Length - 1);
            mailFilter += "}";
            DataTable dataTableResponse = _request.ListMail(sessionKey, mailBox, mailFilter, mailAccessLevel, orderBy, startRowPosition, noOfRows);

            return dataTableResponse;

        }


        public string getValueOfResponseData(DataTable dataTableResponse, int i)
        {
            string value = "";
            foreach (DataRow row in dataTableResponse.Rows)
            {
                value = row.ItemArray[i].ToString();
            }
            return value;
        }

        public string getIntKey (DataTable dataTableResponse)
        {
            string IntKey = getValueOfResponseData(dataTableResponse, 6);
            return IntKey;
        }

        public string ConvertDataTableToString(DataTable dataTable)
        {

            StringBuilder stringBuilder = new StringBuilder();
            dataTable.Rows.Cast<DataRow>().ToList().ForEach(dataRow =>
            {
                dataTable.Columns.Cast<DataColumn>().ToList().ForEach(column =>
                {
                    stringBuilder.AppendFormat("{0}:{1} ", column.ColumnName, dataRow[column]);
                });
                stringBuilder.Append(Environment.NewLine);
            });
            return stringBuilder.ToString();
        }



        public DataSet GetStructureForComposeMail(string sessionKey, string parentMailBox, string mailType, string composeMailAction)
        {
            int parentMailIntKey = 0;
            DataSet dataSetRespone = _request.GetStructureForComposeMail(sessionKey, parentMailIntKey, parentMailBox, mailType, composeMailAction);
            return dataSetRespone;
        }

        public string SaveMail(string sessionKey, string mailType, string mailDetailsXml, string composeMailAction, string parentMailBox)
        {
            int parentMailIntKey = 0;
            string intKeyofSavedMail = _request.SaveMail(sessionKey, mailType, mailDetailsXml, composeMailAction, parentMailIntKey, parentMailBox);

            return intKeyofSavedMail;
        }

        public string SendMail(string sessionKey, int savedMailKey, string mailType, string mailDetailsXml, string composeMailAction, string parentMailBox)
        {
            int parentMailIntKey = 0;
            string intKeyofSentMail = _request.SendMail(sessionKey, savedMailKey, mailType, mailDetailsXml, composeMailAction, parentMailIntKey, parentMailBox);

            return intKeyofSentMail;
        }

        public KeyValuePair<string, bool> ValidateIntKeySavedMail (string intKeySavedMail)
        {
            try
            {
                int temp = int.Parse(intKeySavedMail);
                return new KeyValuePair<string, bool>(Validation.IntKey_Saved_Mail, true);
           
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.IntKey_Saved_Mail + e, false);
            }
        }

        public KeyValuePair<string, bool> ValidateIntKeySentMail(string intKeySentMail)
        {
            try
            {
                int temp = int.Parse(intKeySentMail);
                return new KeyValuePair<string, bool>(Validation.IntKey_Sent_Mail, true);               
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.IntKey_Sent_Mail + e, false);
            }
        }

        private static class Validation
        {
            public static string IntKey_Saved_Mail = "Validate intkey of Saved Mail is correct ";
            public static string IntKey_Sent_Mail = "Validate intkey of Sent Mail is correct ";
            public static string Emails_In_Mail_Box = "Validate Email exists in MailBox ";
            public static string Emails_Not_In_Mail_Box = "Validate Email not exists in MailBox ";
        }

        #endregion
    }
}
