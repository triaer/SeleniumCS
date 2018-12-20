using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;

namespace KiewitTeamBinder.Api.Service
{
    public class MailApi
    {
        #region Entities
        public MailServiceReference.MailWebServiceSoapClient request = new MailServiceReference.MailWebServiceSoapClient("MailWebServiceSoap");
        #endregion

        #region Actions        
        public DataSet GetStructureForComposeMail(string sessionKey, int parentMailIntKey, string parentMailBox, string mailType, string composeMailAction)
        {
            DataSet dataSetResponse = request.GetStructureForComposeMail(sessionKey, parentMailIntKey, parentMailBox, mailType, composeMailAction);            
            return dataSetResponse;
        }
        
        public DataSet GetMailDetails(string sessionKey, string mailBox, string subject, string[] fieldNamesWithValues = null, string[] dateTimeFieldNamesWithValues = null)
        {
            DataTable mailList = ListMail(sessionKey, mailBox, fieldNamesWithValues, dateTimeFieldNamesWithValues);
            int mailIntKey = 0; 
            foreach (DataRow mail in mailList.Rows)
            {                
                if (mail.ItemArray[14].ToString() == subject)
                {
                    mailIntKey = int.Parse(mail.ItemArray[6].ToString());
                    break;
                }
            }
            DataSet dataSetResponse = request.GetMailDetails(sessionKey, mailBox, mailIntKey);                       
            return dataSetResponse;
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
                mailFilter += "'fMailFilter4': [";
                foreach (var dateTimeFieldNameWithValue in dateTimeFieldNamesWithValues)
                {
                    mailFilter += dateTimeFieldNameWithValue + ",";
                }
                mailFilter = mailFilter.Remove(mailFilter.Length - 1);
                mailFilter += "],";
            }
            mailFilter = mailFilter.Remove(mailFilter.Length - 1);
            mailFilter += "}";
            DataTable dataTableResponse = request.ListMail(sessionKey, mailBox, mailFilter, mailAccessLevel, orderBy, startRowPosition, noOfRows);
            
            return dataTableResponse;
        }

        public KeyValuePair<string, bool> ValidateEmailsInMailBox(DataSet dataSetResponse, bool isExisted)
        {
            try
            {
                int expectedNumberOfTable;
                if (isExisted)
                    expectedNumberOfTable = 4;
                else
                    expectedNumberOfTable = 1;

                int numberOfTable = dataSetResponse.Tables.Count;
                if (numberOfTable == expectedNumberOfTable)
                    return new KeyValuePair<string, bool>(Validation.Emails_In_Mail_Box, true);
                return new KeyValuePair<string, bool>(Validation.Emails_In_Mail_Box, false);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Emails_In_Mail_Box + ", Error: " + e, false);
            }
        }

        private static class Validation
        {
            public static string Emails_In_Mail_Box = "Validate that email exists in draft mail box";
        }
        #endregion
    }
}
