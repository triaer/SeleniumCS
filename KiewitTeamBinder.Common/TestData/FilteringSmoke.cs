using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;


namespace KiewitTeamBinder.Common.TestData
{
    public class FilteringSmoke
    {
        public string ProjectName = "Automation Project 2";
        public string ProjectNumber = "AUTO2";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public static string FilterValue1 = "BLUEPRINT1";
        public static string FilterValue2 = "BLUEPRINT8";
        public string ValueInHoldingProcessStatusColumn = "New";
        public string FromUser = "Automation Admin1 (Kiewit)";

        public List<KeyValuePair<string, string>> ValueInDocumentNoColumn1 = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), FilterValue1)
        };
        public List<KeyValuePair<string, string>> ValueInDocumentNoColumn2 = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), FilterValue2)
        };

    }

}
