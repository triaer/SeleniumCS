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
    public class UploadSingleDocSmoke
    {
        public string ProjectName = "Automation Project 2";
        public string FileNames = "\"DocumentAttachFile.txt\" ";
        public string[] RequiredFields = { "Document No.", "Rev", "Status", "Title", "Category", "Discipline" };
        public Color ColorGrey = Color.FromArgb(207,207,207);
        public int MaxLengthOfDocNo = 40;
        public string SaveMessage = "Document details saved successfully.";
        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("DocumentNo"),
            RevStatus = "01",
            Status = "VSUB",
            Title = Utils.GetRandomValue("DocumentTitle"),
            Category = "BM",
            Discipline = "CON",
            Type = "SUB",
        };
    }
}
