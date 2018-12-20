using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Threading;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Api.Service;


namespace KiewitTeamBinder.Api.Tests.ApiTest
{
    [TestClass]
    public class ApiTest
    {
        private List<KeyValuePair<string, bool>> validations = new List<KeyValuePair<string, bool>>();
        private List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();        

        [TestMethod]
        public void SimpleLogon()
        {
            SessionApi sessionRequest = new SessionApi();
            string sessionKey = "";
            try
            {
                //given
                sessionKey = sessionRequest.LogonWithApplication();
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                string respone = sessionRequest.LogoffStatus(sessionKey);
                validations.Add(sessionRequest.ValidateLogoffStatusSuccessfully(respone));

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
            finally
            {
                Console.WriteLine("Logoff: " + sessionKey + ", " + sessionRequest.LogoffStatus(sessionKey));
            }
        }
        
        [TestMethod]
        public void VerifyDocumentCount()
        {
            SessionApi sessionRequest = new SessionApi();
            string sessionKey = "";
            try
            {
                var verifyDocumentCountData = new VerifyDocumentCountSmoke();
                string[] fieldNamesWithValues = new string[] { verifyDocumentCountData.FilterOption.DocumentNo };

                // given
                sessionKey = sessionRequest.LogonWithApplication();
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                DocumentApi documentRequest = new DocumentApi();
                DataTable respone = documentRequest.ListDocumentsAll(sessionKey, fieldNamesWithValues);
                validations.Add(documentRequest.ValidateRecordCountIsCorrect(respone, verifyDocumentCountData.expectedRecordCount));
                
                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
            finally
            {
                Console.WriteLine("Logoff: " + sessionKey + ", " + sessionRequest.LogoffStatus(sessionKey));
            }
        }

        [TestMethod]
        public void SendEmail()
        {
            SessionApi sessionRequest = new SessionApi();
            string sessionKey = "";
            try
            {
                var sendEmailData = new SendEmailSmoke();
                string[] dateTimeFieldNamesWithValues = new string[] {sendEmailData.FilterOption.Date};
                // given
                sessionKey = sessionRequest.LogonWithApplication();
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                MailApi mailRequest = new MailApi();
                var mailDetailReponse = mailRequest.GetMailDetails(sessionKey, sendEmailData.MailBox, sendEmailData.Subject, null, dateTimeFieldNamesWithValues);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetailReponse, true));

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
            finally
            {
                Console.WriteLine("Logoff: " + sessionKey + ", "+ sessionRequest.LogoffStatus(sessionKey));
            }
        }
    }
}
