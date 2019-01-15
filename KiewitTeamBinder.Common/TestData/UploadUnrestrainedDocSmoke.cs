using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class UploadUnrestrainedDocSmoke
    {
        public string ProjectName = "Automation Project 3";
        public string FileNames = "\"UnrestrainedDocAttachFile.txt\" ";
        public string[] RequiredFields = { "Document No.", "Rev", "Status", "Title", "Category", "Discipline" };
        public Color ColorGrey = Color.FromArgb(207,207,207);
        public int MaxLengthOfDocNo = 40;
        public string SaveMessage = "Document details saved successfully.";
        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("Unrestrained_DocNo"),
            RevStatus = "F - Rev F",
            Status = "IFR - Issued for Review",
            Title = Utils.GetRandomValue("Unrestrained DocTitle"),
            Category = "RE - REFERENCE",
            Discipline = "87 - Misc Specialty Work",
            Type = "SUB - Submittal",
        };
    }
}
