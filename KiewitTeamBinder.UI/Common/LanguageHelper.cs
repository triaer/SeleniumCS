using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Common
{
    public static class LanguageHelper
    {
        private static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Data\\LanguageData.json";
        private static JObject data = JsonHelper.GetDataFromJson(path);

        public static CultureInfo GetCulture(string name)
        {
            return new CultureInfo((String)data[name]["culture"]);
        }

        public static string GetMonthYearFormat(string name)
        {
            return (String)data[name]["month-year-format"];
        }
    }
}
