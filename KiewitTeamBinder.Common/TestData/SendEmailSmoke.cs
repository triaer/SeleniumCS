using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class SendEmailSmoke
    {
        public string UserID = "ADMIN1";
        public string CompanyID = "KIEWIT";
        public string Password = "kiewit";
        public string ProjectNumber = "AUTO2";
        public string ConnectingProduct = "KiewitApiTest";
        public string MailBox = "Drafts";
        public string Subject = "SAVE";
        public FilterOptions FilterOption = new FilterOptions();
        public class FilterOptions
        {
            public string Type = "{'Field': 'TYPE', 'Condition': 'EQUAL', 'Value': 'PCO'}";
            public string Date = "{'Field': 'Created','Condition': 'BETWEEN','Value1' : '2018-12-10', 'Value2' : '2018-12-21'}";
        }
    }      
}
