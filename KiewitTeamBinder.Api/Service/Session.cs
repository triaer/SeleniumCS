using System;
using System.Collections.Generic;


namespace KiewitTeamBinder.Api.Service
{
    public class SessionApi
    {
        #region Entities
        private static string ServiceName = "/Session.asmx";
        private static string EndpointName = "SessionWebServiceSoap";
        private SessionServiceReference.SessionWebServiceSoapClient _request;
        #endregion

        #region Actions
        public SessionApi(string url)
        {
            _request = new SessionServiceReference.SessionWebServiceSoapClient(EndpointName, url + ServiceName);
        }

        public string LogonWithApplication(string userID, string companyID, string password, string projectNumber, string connectingProduct)
        {
            string sessionKey = _request.LogonWithApplication(userID, companyID, password, projectNumber, connectingProduct);
            return sessionKey;
        }

        public string LogoffStatus(string sessionKey)
        {
            string response = _request.LogoffStatus(sessionKey);
            return response;
        }

        public KeyValuePair<string, bool> ValidateLogonWithApplicationSuccessfully(string sessionKey)
        {
            try
            {
                if (!sessionKey.ToUpper().Contains("ERROR"))
                    return new KeyValuePair<string, bool>(Validation.Logon_With_Application_Successfully, true);
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
            public static string Logon_With_Application_Successfully = "Validate that logon with application successfully";
            public static string Logoff_Status_Successfully = "Validate that logoff status successfully";
        }
        #endregion
    }
}
