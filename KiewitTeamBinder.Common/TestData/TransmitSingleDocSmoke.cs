using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class TransmitSingleDocSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string ProjectNumber = "AUTO1";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public string GridViewTransmitDocName = "GridViewDocuments";
        public string ToButton = "toBtn";
        public int NumberOfSelectedDocumentRow = 1;
        public string Subject = "Test Transmittal - Single Doc";
        public string Message = "This is a test transmittal - Single Doc";
        public string ReasonForIssue = "For Information";
        public string RespondByMessage = "Response required by";
        public string RespondByDate = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
        public User KiewitUser = new User();

        public class User
        {
            public string UserName = "Automation Admin1";
            public string CompanyName = "Kiewit";
            public string Description = "Automation Admin1 (Kiewit)";
        }
    }
}
