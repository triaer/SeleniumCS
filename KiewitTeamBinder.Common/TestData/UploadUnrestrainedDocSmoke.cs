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
        public string ProjectName = "Automation Project 3";
        public string ProjectNumber = "AUTO3";
        public string FileNames = "\"UnrestrainedDocAttachFile.txt\" ";
        public string[] RequiredFields = { "Document No.", "Rev", "Status", "Title", "Category", "Discipline" };
        public Color ColorGrey = Color.FromArgb(207,207,207);
        public int MaxLengthOfDocNo = 40;
        public string SaveMessage = "Document details saved successfully.";
        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("Unrestrained_DocNo").ToUpper(),
            RevStatus = "F",
            Status = "IFR",
            Title = Utils.GetRandomValue("Unrestrained DocTitle"),
            Category = "RE",
            Discipline = "87",
            Type = "SUB",
        };

        //User Story 120278 - 120081 - Upload Unrestrained Document Part 2
        public string DescriptionAdminUser = "Automation Admin1 (Kiewit)";
        public string DescriptionUserVendor2 = "Automation Vendor2 (AUTOVENDOR)";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public string GridViewHoldingAreaData = "_GridViewHoldingArea_GridData";
        public int NumberOfSelectedDocumentRow = 1;    
        public string DocumentDetailWindow(SingleDocumentInfo SingleDocInformation)
        {
            return "AUTO1 - " + SingleDocInformation.DocumentNo.ToUpper() + " - "
                                    + SingleDocInformation.RevStatus.Split('-')[0].Trim().ToUpper() + " - "
                                    + SingleDocInformation.Status.Split('-')[0].Trim().ToUpper();
        }
        public string VendorDocumentSubmissionWindow = "AUTO3 - " + "New Vendor Document Submission";
        public string ToButton = "toBtn";
        public string toTableTo = "To";
        public string toTableCc = "Cc";
        public string CompanyName = "Kiewit";
        public List<string> ListUser = new List<string>() { "Automation Admin1", "Automation Admin2" };
        public List<string> ListUserTo = new List<string>() { "Automation User1", "Automation Admin1" };
        public List<string> ListUserCc = new List<string>() { "Automation User2" };
        public string Subject = "Test - Upload Unrestrained Document";
        public string Message = "This is a test - Upload Unrestrained Document";
        public string TransmittalDetailWindow = "AUTO3 - Transmittal: {0} - {1}";
        public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

        public class ColumnValuesInCondition
        {
            public KeyValuePair<string, string> DocumentNo = new KeyValuePair<string, string>(DocumentDetailHeader.DocumentNo.ToDescription(), new SingleDocumentInfo().DocumentNo);
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

        //User Story 120278 - 120081 - Upload Unrestrained Document Part 3
        public string TransmittalGridViewName = "TransmittalRegisterGrid";
    }
}
