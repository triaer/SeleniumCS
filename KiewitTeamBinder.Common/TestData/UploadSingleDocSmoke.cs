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
        public string ProjectName = "Automation Project 1";
        public string FileNames = "\"DocumentAttachFile.txt\" ";
        public string[] RequiredFields = { "Document No.", "Rev", "Status", "Title", "Category", "Discipline", "Type" };
        public Color ColorGrey = Color.FromArgb(207,207,207);
        public int MaxLengthOfDocNo = 40;

        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("LGVN_DocumentNo"),
            RevStatus = "01 - Rev 01",
            Status = "VSUB - Vendor Submission",
            Title = Utils.GetRandomValue("LGVN_Title"),
            Category = "BM - BILL OF MATERIAL",
            Discipline = "CON - Contruction",
            Type = "SUB - Submittal",
        };
    }
}
