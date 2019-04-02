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
    public static class CurrencyHelper
    {
        private static string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Data\\CurrencyData.json";
        private static JObject data = JsonHelper.GetDataFromJson(path);

        public static string GetCurrencyCode(string name)
        {
            return (String)data[name]["code"];
        }

        public static string GetCurrencyIcon(string name)
        {
            return (String)data[name]["icon"];
        }
    }
}
