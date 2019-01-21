using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class ReportSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string ModuleName = "Vendor Data";
        public string ModuleItemName = "001 - Details – by Contract Number";
        public string[] AvailableReports = { "001 - Details – by Contract Number", "005 - Details – by Item ID" };
        public string ContractNumberDropdownList = "Contract No.";
        public string ContractNumberItem = "1234567";
        public string contractNumberKey = "Contract Number";
        public string[] contractNumberValueArray = { "1234567" };
        public string ReportLeftPanel = "ReportTypesPanelBar";
        public string FavLeftPanel = "FavReportTypePanelBar";
        public string[] favoriteItems = { "Myself", "My company", "My project" };
        public string radioButton = "Schedule";
        public string myselfFavReport = "Myself";
        public string mycompanyFavReport = "My company";
        public string myprojectFavReport = "My project";
        public string reporterHeader = "Report Title:";
        public string contractUserName = "Automation";
        public string availableMessage = "Your report request is being processed and you will be notified via email once the report becomes available.";
        public string favSuccessfullyMsg = "Selected report successfully added to Favorites.";

        public KeyValuePair<string, string> Title = new KeyValuePair<string, string>("Title", "Vendor Data Details – by Contract Number");
    }
}
