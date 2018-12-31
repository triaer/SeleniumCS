using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class RunReportSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string ModuleName = "Vendor Data";
        public string ModuleItemName = "001 - Details – by Contract Number";
        public string[] AvailableReports = { "001 - Details – by Contract Number", "005 - Details – by Item ID" };
        public string ContractNumberDropdownList = "Contract No.";
        public string ContractNumberItem = "1234567";
    }
}
