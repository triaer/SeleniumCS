using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class PackagesModuleSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] PackagesPath = { "Packages", "Packages/Inbox", "Packages/Drafts", "Packages/Sent Items" };
        public string[] PackagesSubItemLinks = { "Inbox", "Drafts", "Sent Items" };
        public string DefaultFilter = "All";
        public string PackageModule = "Package";
        public string PackagesNode = "Packages";
        public ColumnName ColumnNames = new ColumnName();
    }
    public class ColumnName
    {
        public KeyValuePair<string, string> TransmittalNo = new KeyValuePair<string, string>(HeaderOfTableOnMainPane.TransmittalNo.ToDescription(), "KIEWIT");
        public KeyValuePair<string, string> Package = new KeyValuePair<string, string>(HeaderOfTableOnMainPane.Package.ToDescription(), "74841");
    }
}
