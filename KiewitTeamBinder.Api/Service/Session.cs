using System;
using System.Collections.Generic;


namespace KiewitTeamBinder.Api.Service
{
    public class SessionApi
    {
        #region Entities
        public SessionServiceReference.SessionWebServiceSoapClient request = new SessionServiceReference.SessionWebServiceSoapClient("SessionWebServiceSoap");
        #endregion

        #region Actions
        public string LogonWithApplication(string userID = "ADMIN1", string companyID = "KIEWIT", string password = "kiewit", string projectNumber = "AUTO1", string connectingProduct = "KiewitApiTest")
        {            
            string sessionKey = request.LogonWithApplication(userID, companyID, password, projectNumber, connectingProduct);
            return sessionKey;
        }

        public string LogoffStatus(string sessionKey)
        {            
            string response = request.LogoffStatus(sessionKey);
            return response;
        }

        public KeyValuePair<string, bool> ValidateLogonWithApplicationSuccessfully(string sessionKey)
        {            
            try
            {
                if (!sessionKey.ToUpper().Contains("ERROR"))
                    return new KeyValuePair<string, bool>(Validation.Logon_With_Application_Successfully + sessionKey, true);
                return new KeyValuePair<string, bool>(Validation.Logon_With_Application_Successfully + ", " + sessionKey, false);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Logon_With_Application_Successfully + ", Error: " + e, false);
            }
        }

        public KeyValuePair<string, bool> ValidateLogoffStatusSuccessfully(string response)
        {
            try
            {
                if (response.ToUpper().Contains("SUCCESS"))
                    return new KeyValuePair<string, bool>(Validation.Logoff_Status_Successfully, true);
                return new KeyValuePair<string, bool>(Validation.Logoff_Status_Successfully + ", " + response, false);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Logoff_Status_Successfully + ", Error: " + e, false);
            }
        }

        private static class Validation
        {
            public static string Logon_With_Application_Successfully = "Validate that logon with application successfully. Returned sessionKey: ";
            public static string Logoff_Status_Successfully = "Validate that logoff status successfully";
        }
        #endregion
    }
}
