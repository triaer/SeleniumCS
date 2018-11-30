using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class NavigateToModulesFromTheLeftNavSmoke
    {        
        public string ProjectName = "Automation Project 1";

        public class DashboardModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string[] ListWidgits = { DashboardWidgit.MAIL.ToDescription(), DashboardWidgit.PACKAGES.ToDescription() };
            public string DefaultFilter = "Latest Unrestrained";
            public string GridViewName = "GridViewDocReg";
            public string DocumentNo = "77777";
        }
        public class TransmittalsModules : NavigateToModulesFromTheLeftNavSmoke
        {
            
            public string[] SubItemLinks = { "Inbox", "Drafts", "Sent Items", "Pending" };
            public string SubPendingTitle = "Pending Transmittals";
            public string DefaultFilter = "Personal";
            public string DefaultFilterAtPendingPane = "By Recipient";
            public string TransmittalsModule = "Transmittals";
            public string GridViewName = "TransmittalRegisterGrid";
            public string GridViewPendingName = "RadGridRecipients";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> TransmittalNo = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.TransmittalNo.ToDescription(), "KIEWIT");
                public KeyValuePair<string, string> Package = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Package.ToDescription(), "74841");
                public KeyValuePair<string, string> Subject = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Subject.ToDescription(), "test subjest");
            }
        }
        public class PackagesModules : NavigateToModulesFromTheLeftNavSmoke
        { 
            public string[] SubItemLinks = { "Inbox", "Drafts", "Sent Items" };
            public string DefaultFilter = "All";
            public string PackageModule = "Package";
            public string PackagesNode = "Packages";
            public string GridViewName = "GridViewPackage";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> PackageID = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.PackageID.ToDescription(), "74841");
                public KeyValuePair<string, string> PackageType = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.PackageType.ToDescription(), "Standard");
            }
        }      

    }

}
