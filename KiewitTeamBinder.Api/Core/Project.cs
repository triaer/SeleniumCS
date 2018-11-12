using Microsoft.IdentityModel.Clients.ActiveDirectory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Core
{
    public class Project
    {
        public async Task<string> GetProject(string accessToken)
        {
            //  string aadInstance = "https://login.microsoftonline.com/{0}";
            //  string tenant = "mikedouglasdeliveron.onmicrosoft.com";
            //  string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
            //  UserCredential uc = new UserPasswordCredential("test@mikedouglasdeliveron.onmicrosoft.com", "");
            //  AuthenticationContext authContext = new AuthenticationContext(authority, false); ;
            //  AuthenticationResult result;


            //  try
            //  {
            //      ClientCredential clcred =
            //new ClientCredential("3225b2d4-a0cd-4b93-9cdd-51cbb0af6acb", "dmYziaMr81Cud7if/LXoUod2x9a13xeBQqcYr4uwFik=");

            //      // result = await authContext.AcquireTokenSilentAsync("https://mikedouglasdeliveron.onmicrosoft.com/b20bc9a8-f53a-42b5-b585-b572b936c8c3", "3225b2d4-a0cd-4b93-9cdd-51cbb0af6acb");
            //      //result = await authContext.AcquireTokenAsync("https://mikedouglasdeliveron.onmicrosoft.com/b20bc9a8-f53a-42b5-b585-b572b936c8c3", "3225b2d4-a0cd-4b93-9cdd-51cbb0af6acb", uc);
            //      result = await authContext.AcquireTokenAsync("https://mikedouglasdeliveron.onmicrosoft.com/b20bc9a8-f53a-42b5-b585-b572b936c8c3", clcred);

            //  }
            //  catch (Exception ex)
            //  {
            //      throw ex;
            //  }

            //  // when
            //  var client = new RestClient("https://testsecurity.azurewebsites.net");

            //  string accessToken = "Bearer " + result.AccessToken;
            // string accessToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImlCakwxUmNxemhpeTRmcHhJeGRacW9oTTJZayIsImtpZCI6ImlCakwxUmNxemhpeTRmcHhJeGRacW9oTTJZayJ9.eyJhdWQiOiJjZDU2MjU2OS0xODM3LTQ3OWItOWNlNS04MDc5YzdkY2UyOTkiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8wNzQyMGMzZC1jMTQxLTRjNjctYjZmMy1mNDQ4ZTVhZGI2N2IvIiwiaWF0IjoxNTI2ODI0Mjg2LCJuYmYiOjE1MjY4MjQyODYsImV4cCI6MTUyNjgyODE4NiwiYWlvIjoiQVNRQTIvOEhBQUFBK1dYSE5TY29BYzlWOTFvSHZyYTBNZFV4aUZkUDBodVp2L2ZQbVdqZW5hYz0iLCJhbXIiOlsicHdkIl0sImZhbWlseV9uYW1lIjoiRG91Z2xhcyIsImdpdmVuX25hbWUiOiJNaWtlIiwiaXBhZGRyIjoiNjguMTMuMTc1LjE2MCIsIm5hbWUiOiJNaWtlLkRvdWdsYXMiLCJub25jZSI6IjJjNDdhOTdmLTRlMjEtNDU2Yi1hZmEyLWUzNWY4MjcyMDQ4MSIsIm9pZCI6IjRlMmIzMzI0LWYwZmMtNDc5YS04OGM2LTk0NzkyOGQxZTY1YiIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0xNzMxNzczNzY3LTE1ODc3MTg1MC0zODg1OTMzMDA5LTY0NTQyMyIsInN1YiI6IktoVmxaaV94T2pzbUotb210WFRRVkdyRkJKNmtvUFlxVEdfR1RGbjg5a3ciLCJ0aWQiOiIwNzQyMGMzZC1jMTQxLTRjNjctYjZmMy1mNDQ4ZTVhZGI2N2IiLCJ1bmlxdWVfbmFtZSI6Ik1pa2UuRG91Z2xhc0BraWV3aXQuY29tIiwidXBuIjoiTWlrZS5Eb3VnbGFzQGtpZXdpdC5jb20iLCJ1dGkiOiJmTllCcWxNNnZFQ3F1Nk9ncjJNVEFBIiwidmVyIjoiMS4wIn0.apmCH25gjMegmxwPp-CE5kJLonGW1mYJ59yXZ4op52273FjyfclYajFPT8YvLeGJE5isE-r6P-P4J6aoB5BxudtxolTfRjAJ5tnvTN1x0dxeyyUl96mTkvqPd6e764y6_Xw-vtBBV1V3ORAcf-8oAWHv-bCFkSvlu4aO4RHt4wp9FleewAgo3M_Ax26tjKcfGANnrRwTR4kapsdn0kjFOHoT4IXZRH17cEmCdJkiO48tED8o_zy9SgZf2Fk_G4ETHHfaSoWw0igmhQQS3LEowdidXJwLZ-CS7NrUhio1-XXnVuMDUcHQH2TDH_Oa1p4649VMy1w8x2jWVL3HQF4W-g";

           // var request = new RestRequest(new Uri("https://testsecurity.azurewebsites.net/api/HttpTriggerCSharp1?code=NDnjFmbRiK7T8zCPZVlucMUy1SU1gkB0A2kaC3MmUZFaZ3v5A3tCcQ==&name=test"), Method.GET);
           // request.AddHeader("Authorization", accessToken);
           // execute the request
           //var response = client.Execute(request);

            // // given
            // UserCredential uc = new UserPasswordCredential("mike.douglas@kiewit.com", "Hobbyist93$");

            // AuthenticationContext authContext = null;
            // var result = await authContext.AcquireTokenAsync("https://kwt-int-182.hds.ineight.com/CoreWebServices", "cd562569-1837-479b-9ce5-8079c7dce299", uc);


            // // when
            var client = new RestClient("https://kwt-int-182.hds.ineight.com");

            //string accessToken = "Bearer " + result.AccessToken;
            // string accessToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImlCakwxUmNxemhpeTRmcHhJeGRacW9oTTJZayIsImtpZCI6ImlCakwxUmNxemhpeTRmcHhJeGRacW9oTTJZayJ9.eyJhdWQiOiJjZDU2MjU2OS0xODM3LTQ3OWItOWNlNS04MDc5YzdkY2UyOTkiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8wNzQyMGMzZC1jMTQxLTRjNjctYjZmMy1mNDQ4ZTVhZGI2N2IvIiwiaWF0IjoxNTI2ODI0Mjg2LCJuYmYiOjE1MjY4MjQyODYsImV4cCI6MTUyNjgyODE4NiwiYWlvIjoiQVNRQTIvOEhBQUFBK1dYSE5TY29BYzlWOTFvSHZyYTBNZFV4aUZkUDBodVp2L2ZQbVdqZW5hYz0iLCJhbXIiOlsicHdkIl0sImZhbWlseV9uYW1lIjoiRG91Z2xhcyIsImdpdmVuX25hbWUiOiJNaWtlIiwiaXBhZGRyIjoiNjguMTMuMTc1LjE2MCIsIm5hbWUiOiJNaWtlLkRvdWdsYXMiLCJub25jZSI6IjJjNDdhOTdmLTRlMjEtNDU2Yi1hZmEyLWUzNWY4MjcyMDQ4MSIsIm9pZCI6IjRlMmIzMzI0LWYwZmMtNDc5YS04OGM2LTk0NzkyOGQxZTY1YiIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0xNzMxNzczNzY3LTE1ODc3MTg1MC0zODg1OTMzMDA5LTY0NTQyMyIsInN1YiI6IktoVmxaaV94T2pzbUotb210WFRRVkdyRkJKNmtvUFlxVEdfR1RGbjg5a3ciLCJ0aWQiOiIwNzQyMGMzZC1jMTQxLTRjNjctYjZmMy1mNDQ4ZTVhZGI2N2IiLCJ1bmlxdWVfbmFtZSI6Ik1pa2UuRG91Z2xhc0BraWV3aXQuY29tIiwidXBuIjoiTWlrZS5Eb3VnbGFzQGtpZXdpdC5jb20iLCJ1dGkiOiJmTllCcWxNNnZFQ3F1Nk9ncjJNVEFBIiwidmVyIjoiMS4wIn0.apmCH25gjMegmxwPp-CE5kJLonGW1mYJ59yXZ4op52273FjyfclYajFPT8YvLeGJE5isE-r6P-P4J6aoB5BxudtxolTfRjAJ5tnvTN1x0dxeyyUl96mTkvqPd6e764y6_Xw-vtBBV1V3ORAcf-8oAWHv-bCFkSvlu4aO4RHt4wp9FleewAgo3M_Ax26tjKcfGANnrRwTR4kapsdn0kjFOHoT4IXZRH17cEmCdJkiO48tED8o_zy9SgZf2Fk_G4ETHHfaSoWw0igmhQQS3LEowdidXJwLZ-CS7NrUhio1-XXnVuMDUcHQH2TDH_Oa1p4649VMy1w8x2jWVL3HQF4W-g";

            var request = new RestRequest(new Uri("https://kwt-int-182.hds.ineight.com/CoreWebServices/odata/Projects(179)?$select=ProjectId"), Method.GET);
            request.AddHeader("UserConnectionId", "db754da5-77ba-49c9-8a83-42bf96aeb376");
            request.AddHeader("Referer", "https://kwt-int-182.hds.ineight.com/AppControl/Workspace?projectId=179");
            request.AddHeader("Authorization", "Bearer " + accessToken);
            // execute the request
            var response = client.Execute(request);

            return "";
        }
    }
}
