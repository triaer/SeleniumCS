using KiewitTeamBinder.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class SignOnTestsSmoke
    {
        public string PageName = "testPage";
        public string ParentPage = "Overview";
        public string NumberCoumn = "2";
        public string DisplayAfter = "Select page";
        public string InputName =  Utils.GetRandomValue("DA_DP_TC1000000");
        public string ItemType = "test modules";
        public string RelatedDataItem = "Related bugs";
        public string Field = "Name";
        public string ValueDescription = "abc";
        public string FillterField = "Recent result";
        public string[] FitllterField = { "Recent result", "Status"};
        public string AndFillter = "and";
    }
}
