using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class VerifyDocumentCountSmoke
    {
        public string userID = "ADMIN1";
        public string companyID = "KIEWIT";
        public string password = "kiewit";
        public string projectNumber = "AUTO2";
        public string connectingProduct = "KiewitApiTest";
        public int expectedRecordCount = 3;

        public FilterOptions FilterOption = new FilterOptions();        
        public class FilterOptions
        {
            public string DocumentNo = "{'Field': 'DocumentNo', 'Condition': 'LIKE', 'Value': 'TEST_API'}";
            public string Sts = "{'Field': 'Sts', 'Condition': 'EQUAL', 'Value': 'UNC'}";            
        }
        
    }      

}
