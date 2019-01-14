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
                var sendMailData = new SendMailSmoke();
                var teambinderTestAccount1 = GetTestAccount("AdminAccount1", environment, "NonSSO");
                var teambinderTestAccount2 = GetTestAccount("AdminAccount2", environment, "NonSSO");
                sessionRequest = new SessionApi(GetServiceUrl(teambinderTestAccount1.Url));

                //when
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount1.Username, teambinderTestAccount1.Company, teambinderTestAccount1.Password, sendMailData.ProjectNumber, sendMailData.ConnectingProduct);
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));

                MailApi mailRequest = new MailApi(GetServiceUrl(teambinderTestAccount1.Url));
                int savedMailIntKey = int.Parse(mailRequest.SaveMail(sessionKey, sendMailData.MailType, sendMailData.MailDetailXml(sendMailData.IntKeyForNewMail, sendMailData.RecipientIntKey), sendMailData.ComposeMailAcction, sendMailData.MailBox));

                DataSet mailDetail = mailRequest.GetMailDetails(sessionKey, sendMailData.MailBox, savedMailIntKey);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetail, true));

                string intKeySentMail = mailRequest.SendMail(sessionKey, savedMailIntKey, sendMailData.MailType, sendMailData.MailDetailXml(savedMailIntKey, sendMailData.RecipientIntKey), sendMailData.ComposeMailAcction, sendMailData.MailBox);
                validations.Add(mailRequest.ValidateIntKeySentMail(intKeySentMail));

                string respone = sessionRequest.LogoffStatus(sessionKey);
                validations.Add(sessionRequest.ValidateLogoffStatusSuccessfully(respone));

                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount2.Username, teambinderTestAccount2.Company, teambinderTestAccount2.Password, sendMailData.ProjectNumber, sendMailData.ConnectingProduct);
                mailDetail = mailRequest.GetMailDetails(sessionKey, sendMailData.MailBox, savedMailIntKey);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetail, false));

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

