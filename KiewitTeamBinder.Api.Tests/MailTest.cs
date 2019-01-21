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
using System.Xml.Serialization;
using System.IO;

namespace KiewitTeamBinder.Api.Tests
{
    [TestClass]
    public class MailTest : ApiTestBase
    {
        //[TestMethod]
        public void Mail_SentMail_API()
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
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

                MailApi mailRequest = new MailApi(GetServiceUrl(teambinderTestAccount1.Url));
                DataSet dataSetResponse = mailRequest.GetStructureForComposeMail(sessionKey, sendMailData.DraftBox, sendMailData.MailType, sendMailData.ComposeMailAcction);
                string mailDetailsXml = mailRequest.GetMailDetailsXml(dataSetResponse, sendMailData.Subject, sendMailData.MailType, sendMailData.IntKeyForNewMail, sendMailData.RecipientIntKey);
                Console.WriteLine(dataSetResponse);

                //Save email
                int savedMailIntKey = int.Parse(mailRequest.SaveMail(sessionKey, sendMailData.MailType, mailDetailsXml, sendMailData.ComposeMailAcction, sendMailData.DraftBox));
                //Verify email exists in draft view
                DataSet mailDetail = mailRequest.GetMailDetails(sessionKey, sendMailData.DraftBox, savedMailIntKey);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetail, true));

                //Send Email
                string mailDetailsXml2 = mailRequest.GetMailDetailsXml(dataSetResponse, sendMailData.Subject, sendMailData.MailType, savedMailIntKey, sendMailData.RecipientIntKey);
                int sentMailIntKey = int.Parse(mailRequest.SendMail(sessionKey, savedMailIntKey, sendMailData.MailType, mailDetailsXml2, sendMailData.ComposeMailAcction, sendMailData.DraftBox));
                //Verify email was sent
                validations.Add(mailRequest.ValidateIntKeySentMail(sentMailIntKey));
                //Verify draft email has been removed
                mailDetail = mailRequest.GetMailDetails(sessionKey, sendMailData.DraftBox, savedMailIntKey);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetail, false));

                //Log out, 
                string respone = sessionRequest.LogoffStatus(sessionKey);
                validations.Add(sessionRequest.ValidateLogoffStatusSuccessfully(respone));
                //Then log in with a recipient user
                sessionKey = sessionRequest.LogonWithApplication(teambinderTestAccount2.Username, teambinderTestAccount2.Company, teambinderTestAccount2.Password, sendMailData.ProjectNumber, sendMailData.ConnectingProduct);
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
                //Verify email exists
                mailDetail = mailRequest.GetMailDetails(sessionKey, sendMailData.Inbox, sentMailIntKey);
                validations.Add(mailRequest.ValidateEmailsInMailBox(mailDetail, true));
                
                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
                validations.Add(new KeyValuePair<string, bool>("Release " + sessionKey, sessionRequest.ValidateLogoffStatusSuccessfully(sessionRequest.LogoffStatus(sessionKey)).Value));
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

