using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;


namespace KiewitTeamBinder.Api.Service
{
    public class DocumentApi
    {
        #region Entities
        private static string ServiceName = "/Document.asmx";
        private static string EndpointName = "DocumentWebServiceSoap";
        private DocumentServiceReference.DocumentWebServiceSoapClient _request;
        #endregion

        #region Actions 
        public DocumentApi(string url)
        {
            _request = new DocumentServiceReference.DocumentWebServiceSoapClient(EndpointName, url + ServiceName);
        }
               
        public DataTable ListDocumentsAll(string sessionKey, string[] fieldNamesWithValues = null, string[] dateTimeFieldNamesWithValues = null, string registerView = "ALL")
        {
            string orderBy = "DocumentNo";
            int startRowPosition = 0;
            int noOfRows = 100;
            string documentFilter = "{";
            if (fieldNamesWithValues != null && fieldNamesWithValues.Length > 0)
            {
                documentFilter += "'fDocFilter3': [";
                foreach (var fieldNameWithValue in fieldNamesWithValues)
                {
                    documentFilter += fieldNameWithValue + ",";
                }
                documentFilter = documentFilter.Remove(documentFilter.Length - 1);
                documentFilter += "],";
            }

            if (dateTimeFieldNamesWithValues != null && dateTimeFieldNamesWithValues.Length > 0)
            {
                documentFilter += "'fDocFilter4': [";
                foreach (var dateTimeFieldNameWithValue in dateTimeFieldNamesWithValues)
                {
                    documentFilter += dateTimeFieldNameWithValue + ",";
                }
                documentFilter = documentFilter.Remove(documentFilter.Length - 1);
                documentFilter += "],";
            }
            documentFilter = documentFilter.Remove(documentFilter.Length - 1);
            documentFilter += "}";

            DataTable dataTableResponse = _request.ListDocumentsAll(sessionKey, documentFilter, registerView, orderBy, startRowPosition, noOfRows);            
            return dataTableResponse;
        }
        
        public KeyValuePair<string, bool> ValidateRecordCountIsCorrect(DataTable dataTableResponse, int expectedRecordCount)
        {            
            try
            {
                int numberOfRecord = dataTableResponse.Rows.Count;
                if (numberOfRecord == expectedRecordCount)
                    return new KeyValuePair<string, bool>(Validation.Record_Count_Is_Correct, true);
                return new KeyValuePair<string, bool>(Validation.Record_Count_Is_Correct + ", " + expectedRecordCount + ", " + numberOfRecord, false);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Record_Count_Is_Correct + ", Error: " + e, false);
            }
        }

        private static class Validation
        {
            public static string Record_Count_Is_Correct = "Validate that record count is correct";
        }
        #endregion
    }
}
