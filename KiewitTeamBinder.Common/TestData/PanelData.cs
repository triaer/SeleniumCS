using KiewitTeamBinder.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiewitTeamBinder.Common.TestData
{
    public class PanelData
    {

        public User user1 = new User()
        {
            Repository = null,
            Username = "administrator",
            Password = "",
        };

        public string[] chartInfo = {"Action Implementation By Status",
               "Test Module Implementation By Priority",
                "Test Module Implementation By Status",
                "Test Module Implementation Progress",
                "Test Module Status per Assigned Users"};

        public string[] indicatorsInfo = {"Test Case Execution", "Test Module Execution", "Test Objective Execution"};

        public TAPage taPage1 = new TAPage()
        {
            PageName = "Test_panel",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = false,
        };

        public string ChartSettings = "Chart Settings";
        public string IndicatorSettings = "Indicator Settings";
        public string HeatMapSettings = "Heat Map Settings";

        public ChartPanel chartPanel = new ChartPanel()
        {
           Type = "Chart",
           DisplayName = "Chart Panel",
           Series = "Name",
        };
    }
}
