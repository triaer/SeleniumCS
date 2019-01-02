using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;


namespace KiewitTeamBinder.Common.TestData
{
    public class CreatePurchaseItemSmoke
    {       
        public string ProjectName = "Automation Project 1";
        public string GridViewName = "GridViewContractVendor";
        public string GridViewItemsName = "GridViewItemsVendor";
        public string[] RequiredFields = {"Item ID", "Description", "Status"};
        public KeyValuePair<string, string> ItemID = new KeyValuePair<string, string>("Item ID", Utils.GetRandomValue("ITEMID"));
        public KeyValuePair<string, string> Description = new KeyValuePair<string, string>("Description", "Description content");
        public KeyValuePair<string, string> ContractNumber = new KeyValuePair<string, string>("Contract Number", "1234567");
        public KeyValuePair<string, string> Status = new KeyValuePair<string, string>("Status", "OPEN - OPEN");
        public string SaveMessage = "Saved Successfully";        
        public int expandButtonIndex = 1;
    }
}
