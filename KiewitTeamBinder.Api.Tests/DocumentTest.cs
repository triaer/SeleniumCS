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
    public class DocumentTest : ApiTestBase
    {
        [TestMethod]
        public void Documents_VerifyDocumentCount_API()
        {
            SessionApi sessionRequest = null;
            string sessionKey = "";
            try
            {
                //given
                var verifyDocumentCountData = new VerifyDocumentCountSmoke();
                string[] fieldNamesWithValues = new string[] { verifyDocumentCountData.FilterOption.DocumentNo };
                var teambinderTestAccount = GetTestAccount("AdminAccount2", environment, "NonSSO");
                sessionRequest = new SessionApi(GetServiceUrl(teambinderTestAccount.Url));

                //when
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount.Username, teambinderTestAccount.Company, teambinderTestAccount.Password, verifyDocumentCountData.ProjectNumber, verifyDocumentCountData.ConnectingProduct);
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));
                
                DocumentApi documentRequest = new DocumentApi(GetServiceUrl(teambinderTestAccount.Url));
                DataTable respone = documentRequest.ListDocumentsAll(sessionKey, fieldNamesWithValues);
                validations.Add(documentRequest.ValidateRecordCountIsCorrect(respone, verifyDocumentCountData.ExpectedRecordCount));

                validations.Add(new KeyValuePair<string, bool>("Release " + sessionKey, sessionRequest.ValidateLogoffStatusSuccessfully(sessionRequest.LogoffStatus(sessionKey)).Value));

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                validations.Add(new KeyValuePair<string, bool>("Release " + sessionKey, sessionRequest.ValidateLogoffStatusSuccessfully(sessionRequest.LogoffStatus(sessionKey)).Value));
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
    }
}
