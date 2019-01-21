using KiewitTeamBinder.Common.TestData;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Api.Service
{
    public class UploadFilesAPI
    {
        //UploadFilesSmoke uploadFilesData = new UploadFilesSmoke();
        public string UploadFiles(string sessionKey, string[] fileNames)
        {
            string url = $"https://kiewittest.teambinder.com/TBWS/UploadFile.aspx?sessionKey={sessionKey}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            try
            {
                string[] filePaths = new string[fileNames.Length];
                for (int i = 0; i < filePaths.Length; i++)
                {
                    filePaths[i] = Utils.GetInputFilesLocalPath() + "\\" + fileNames[i];
                    request.AddFile("File" + (i + 1).ToString(), filePaths[i]);
                }
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public KeyValuePair<string, bool> ValidateUploadFiles(string uploadFilesResponse, string[] expectedFileName)
        {
            try
            {
                string[] uploadFilenames = uploadFilesResponse.Split(',');
                if (uploadFilenames.Length != expectedFileName.Length)
                    return new KeyValuePair<string, bool>(Validation.Files_Are_Uploaded + "number of upload response files is incorrect. Response: " + uploadFilesResponse, false);

                for (int i = 0; i < uploadFilenames.Length; i++)                
                    uploadFilenames[i] = uploadFilenames[i].Substring(0, uploadFilenames[i].IndexOf('(')) + uploadFilenames[i].Substring(uploadFilenames[i].IndexOf(')') + 1);
                
                bool match;
                foreach (var uploadFilename in uploadFilenames)
                {
                    match = false;
                    for (int i = 0; i < expectedFileName.Length; i++)
                    {
                        if (uploadFilename == expectedFileName[i])
                        {
                            match = true;
                            break;
                        }
                    }
                    if (match == false)                    
                        return new KeyValuePair<string, bool>(Validation.Files_Are_Uploaded + "Actual files: " + uploadFilesResponse , false);
                                        
                }
                return new KeyValuePair<string, bool>(Validation.Files_Are_Uploaded + uploadFilesResponse, true);
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, bool>(Validation.Files_Are_Uploaded + e, false);
            }           
        }

        private static class Validation
        {
            public static string Files_Are_Uploaded = "Validate files are uploaded: ";
        }
    }
}
