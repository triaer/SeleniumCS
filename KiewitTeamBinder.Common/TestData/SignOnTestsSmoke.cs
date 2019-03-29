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
        public string PageName = Utils.GetRandomValue("testPage");
        public string ParentPageOverView = "Overview";
        public string NonParentPage = "Select parent";
        public string NumberCoumn = "2";
        public string DisplayAfter = "Select page";
        public string InputName =  Utils.GetRandomValue("DA_DP_TC1000000");
        public string ItemType = "test modules";
        public string RelatedDataItem = "Related bugs";
        public string Field = "Name";
        public string ValueDescription = "abc";
        public string FillterField = "Recent result";
        public string[] FitllterField = { "Recent result", "Status" };
        public string AndFillter = "and";
        public string[] NameOfCheckbox = {  "name", "location", "description", "run status",
                                            "external id", "revision timestamp", "assigned",
                                            "priority", "status", "update date", "updated by",
                                            "creation date", "created by", "notes", "check out user","url"};
        
    }
}
