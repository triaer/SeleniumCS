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
    public class SessionTest : ApiTestBase
    {
        //[TestMethod]
        public void General_NonSSOValidUserSignon_API()
        {
            SessionApi sessionRequest = null;
            string sessionKey = "";
            try
            {
                //given
                var simpleLogonData = new SimpleLogonSmoke();
                var teambinderTestAccount = GetTestAccount("AdminAccount2", environment, "NonSSO");
                sessionRequest = new SessionApi(GetServiceUrl(teambinderTestAccount.Url));

                //when
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount.Username, teambinderTestAccount.Company, teambinderTestAccount.Password, simpleLogonData.ProjectNumber, simpleLogonData.ConnectingProduct);
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                string respone = sessionRequest.LogoffStatus(sessionKey);
                validations.Add(sessionRequest.ValidateLogoffStatusSuccessfully(respone));

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);                
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
            }
            catch (Exception e)
            {
                validations.Add(new KeyValuePair<string, bool>("Release " + sessionKey, sessionRequest.ValidateLogoffStatusSuccessfully(sessionRequest.LogoffStatus(sessionKey)).Value));
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                throw;
            }
        }
    }
}
