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

namespace KiewitTeamBinder.Api.Tests
{
    [TestClass]
    public class MailTest : ApiTestBase
    {
        [TestMethod]
        public void SentMail_API()
        {
            SessionApi sessionRequest = null;
            string sessionKey = "";
            try
            {
                //given
                var sendMaildata = new SendMailSmoke();
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                sessionRequest = new SessionApi(GetServiceUrl(teambinderTestAccount.Url));

                //when
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount.Username, teambinderTestAccount.Company, teambinderTestAccount.Password, sendMaildata.ProjectNumber, sendMaildata.ConnectingProduct);
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                MailApi mailRequest = new MailApi(GetServiceUrl(teambinderTestAccount.Url));
                DataTable response = mailRequest.ListMail(sessionKey, sendMaildata.MailBox, sendMaildata.FilterOptions, null);

                string intKey = mailRequest.getIntKey(response);
                int intKeySavedMail = int.Parse(mailRequest.SaveMail(sessionKey, sendMaildata.MailType, sendMaildata.MailDetailXml("0"), sendMaildata.ComposeMailAcction, sendMaildata.MailBox));
                validations.Add(mailRequest.ValidateIntKeySavedMail(intKey, intKeySavedMail));

                string intKeySentMail = mailRequest.SendMail(sessionKey, intKeySavedMail, sendMaildata.MailType, sendMaildata.MailDetailXml(intKey), sendMaildata.ComposeMailAcction, sendMaildata.MailBox);
                validations.Add(mailRequest.ValidateIntKeySentMail(intKeySentMail));

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

