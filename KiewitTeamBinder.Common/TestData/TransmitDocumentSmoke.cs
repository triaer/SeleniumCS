using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using static KiewitTeamBinder.Common.Models.LoggedUserInfo;

namespace KiewitTeamBinder.Common.TestData
{
    public class TransmitDocumentSmoke
    {
        public string ProjectName = "Automation Project 2";
        public string ProjectNumber = "AUTO2";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public string GridViewTransmitDocName = "GridViewDocuments";
        public string ToButton = "toBtn";
        public int NumberOfSelectedDocumentRow = 2;        
        public string Subject = "Test Transmittal";
        public string Message = "This is a test transmittal";
        public User KiewitUser = new User()
        {
            UserName = "Automation Admin1",
            CompanyName = "Kiewit",
            Description = "Automation Admin1 (Kiewit)"
        };
    }

}
