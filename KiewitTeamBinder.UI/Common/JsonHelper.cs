using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Common
{
    public static class JsonHelper
    {
        public static JObject GetDataFromJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JObject.Parse(json);
            }
        }
    }
}
