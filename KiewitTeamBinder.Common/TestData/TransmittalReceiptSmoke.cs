using KiewitTeamBinder.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.Models.LoggedUserInfo;

namespace KiewitTeamBinder.Common.TestData
{
    public class TransmittalReceiptSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string DefaultFilter = "Personal";
        public string[] SubItemMenus = { "Inbox", "Drafts", "Sent Items", "Pending" };
        public string GridViewName = "TransmittalRegisterGrid";
        public string DefaultFilterAtPendingPane = "By Recipient";
        public string GridViewDocumentName = "GridViewDocuments";
        public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();
        public User SelectedUserWithCompany = new User()
        {
            UserName = "Automation Admin1",
            CompanyName = "Kiewit",
            Description = "Automation Admin1 (Kiewit)"
        };
        public class ColumnValuesInCondition
        {
            public KeyValuePair<string, string> Subject = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Subject.ToDescription(), "test subjest");
            public KeyValuePair<string, string> DocumentNo = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), "AUTO 13");
        }

        public TransmittalMailInformation transmittalMailInformation = new TransmittalMailInformation()
        {
            FromUser = "Automation Admin1",
            ProjectNumber = "AUTO1",
            ProjectTitle = "Automation Project 1",
            TransmittalDate = "12/06/2018",
            TransmittalNo = "TRN-SMOKE-00039",
            DocumentNo = "AUTO 13",
            AttachedDocumentInfor = new string[] { "AUTO 13"},
        };
    }
    public class TransmittalMailInformation
    {
        public string FromUser { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectTitle { get; set; }
        public string TransmittalDate { get; set; }
        public string TransmittalNo { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentTitle { get; set; }
        public string[] AttachedDocumentInfor { get; set; }
    }


}
