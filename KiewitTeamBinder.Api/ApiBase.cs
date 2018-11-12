using KiewitTeamBinder.API.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API
{
    public class ApiBase
    {
        protected T SendHttpCall<T>(string accessToken, string userConnectionId, string baseUrl, string operationPath, Method method, int projectId = 0, object postBody = null)
        {
            var response = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, method, projectId, postBody);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
        }

        protected string SendHttpCall(string accessToken, string userConnectionId, string baseUrl, string operationPath, Method method, int projectId = 0, object postBody = null)
        {
            var client = new RestClient(baseUrl);
            string uri = string.Format("{0}/{1}", baseUrl, operationPath);
            //Uri realuri = new Uri(uri, true);
            var request = new RestRequest(new Uri(uri), method);
            request.AddHeader("UserConnectionId", userConnectionId);
            if (projectId > 0)
            {
                request.AddHeader("Referer", string.Format("{0}/AppControl/Workspace?projectId={1}", baseUrl, projectId.ToString()));
            }
            request.AddHeader("Authorization", "Bearer " + accessToken);
            if(postBody != null)
            {
                request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                request.AddJsonBody(postBody);
                //request.AddJsonBody(Newtonsoft.Json.JsonConvert.SerializeObject(postBody));
            }
            var response = client.Execute(request);

            if(!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }

            return response.Content;

        }

    }
}
