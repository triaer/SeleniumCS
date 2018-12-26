﻿using System;
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
    public class SessionLogon : ApiTestBase
    {
        [TestMethod]
        public void SimpleLogon()
        {
            SessionApi sessionRequest = new SessionApi();
            string sessionKey = "";
            try
            {
                //given
                var simpleLogonData = new SimpleLogonSmoke();
                var teambinderTestAccount = GetTestAccount("AdminAccount2", environment, "NonSSO");

                //when
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount.Username, teambinderTestAccount.Company, teambinderTestAccount.Password, simpleLogonData.ProjectNumber, simpleLogonData.ConnectingProduct);
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
                validations.Add(new KeyValuePair<string, bool>("Release " + sessionKey, sessionRequest.ValidateLogoffStatusSuccessfully(sessionRequest.LogoffStatus(sessionKey)).Value));
                methodValidations.Add(new KeyValuePair<string, bool>("Error: " + e, false));
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
        
        
    }
}