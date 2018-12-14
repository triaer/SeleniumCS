using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;


namespace KiewitTeamBinder.Common.TestData
{
    public class TransmitDocumentSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string ProjectNumber = "AUTO1";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public string GridViewTransmitDocName = "GridViewDocuments";
        public string ToButton = "toBtn";
        public int NumberOfSelectedDocumentRow = 2;        
        public string Subject = "Test Transmittal";
        public string Message = "This is a test transmittal";
        public User KiewitUser = new User();

        public class User
        {
            public string UserName = "Automation Admin1";
            public string CompanyName = "Kiewit";
            public string Description = "Automation Admin1 (Kiewit)";
        }
    }

}
