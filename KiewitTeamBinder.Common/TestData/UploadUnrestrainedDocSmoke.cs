using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.Models.LoggedUserInfo;

namespace KiewitTeamBinder.Common.TestData
{
    public class UploadUnrestrainedDocSmoke
    {
        //User Story 120278 - 120081 - Upload Unrestrained Document Part 1
        public string ProjectName = "Automation Project 1";
        public string FileNames = "\"UnrestrainedDocAttachFile.txt\" ";
        public string[] RequiredFields = { "Document No.", "Rev", "Status", "Title", "Category", "Discipline" };
        public Color ColorGrey = Color.FromArgb(207,207,207);
        public int MaxLengthOfDocNo = 40;
        public string SaveMessage = "Document details saved successfully.";
        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("Unrestrained_DocumentNo"),
            RevStatus = "F - Rev F",
            Status = "IFR - Issued for Review",
            Title = Utils.GetRandomValue("Unrestrained DocumentTitle"),
            Category = "RE - REFERENCE",
            Discipline = "87 - Misc Specialty Work",
            Type = "SUB - Submittal",
        };
        //User Story 120278 - 120081 - Upload Unrestrained Document Part 2
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public int NumberOfSelectedDocumentRow = 1;
        public string DocumentDetailWindow(SingleDocumentInfo SingleDocInformation)
        {
            return "AUTO1 - " + SingleDocInformation.DocumentNo + " - "
                                    + SingleDocInformation.RevStatus.Split('-')[0].Trim() + " - "
                                    + SingleDocInformation.Status.Split('-')[0].Trim();
        }
        
        public string VendorDocumentSubmissionWindow = "AUTO1 - " + "New Vendor Document Submission";
        public string ToButton = "toBtn";
        public User KiewitUser = new User()
        {
            UserName = "Automation Admin1",
            CompanyName = "Kiewit",
            Description = "Automation Admin1 (Kiewit)"
        };
        public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();
        public class ColumnValuesInCondition
        {
            public KeyValuePair<string, string> DocumentNo = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), new SingleDocumentInfo().DocumentNo);
        }
        public List<KeyValuePair<string, string>> ExpectedDocumentInforInColumnList(SingleDocumentInfo SingleDocInformation)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Document No.", SingleDocInformation.DocumentNo),
                new KeyValuePair<string, string>("Rev", SingleDocInformation.RevStatus),
                new KeyValuePair<string, string>("Status", SingleDocInformation.Status),
                new KeyValuePair<string, string>("Title", SingleDocInformation.Title),
                new KeyValuePair<string, string>("Category", SingleDocInformation.Category),
                new KeyValuePair<string, string>("Discipline", SingleDocInformation.Discipline),
                new KeyValuePair<string, string>("Type", SingleDocInformation.Type)
            };
        }
    }
}
