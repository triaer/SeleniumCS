using FluentAssertions;
using KiewitTeamBinder.Api.Service;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Api.Tests
{
    [TestClass]
    public class UploadFilesTest : ApiTestBase
    {
        [TestMethod]
        public void UploadFiles_API()
        {
            SessionApi sessionRequest = null;
            string sessionKey = "";
            try
            {
                //given
                var uploadFilesdata = new UploadFilesSmoke();
                var testAccount = GetTestAccount("AdminAccount2", environment, "NonSSO");
                sessionRequest = new SessionApi(GetServiceUrl(testAccount.Url));

                //when
                sessionKey = sessionRequest.LogonWithApplication(testAccount.Username, testAccount.Company, testAccount.Password, uploadFilesdata.ProjectNumber, uploadFilesdata.ConnectingProduct);                
                validations.Add(sessionRequest.ValidateLogonWithApplicationSuccessfully(sessionKey));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

                //Upload Files
                UploadFilesAPI uploadfileRequets = new UploadFilesAPI();
                string filesNameResponse = uploadfileRequets.UploadFiles(sessionKey, uploadFilesdata.FileNames);
                validations.Add(uploadfileRequets.ValidateUploadFiles(filesNameResponse, uploadFilesdata.FileNames));

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
