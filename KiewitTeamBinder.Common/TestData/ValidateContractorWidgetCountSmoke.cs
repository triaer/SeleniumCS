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
    public class ValidateContractorWidgetCountSmoke
    {       
        public string ProjectName = "Automation Project 1";
        public string WidgetName = "Contractor View";
        public string RowName = "Deliverables";
        
    }
    public class ValidateDeliverableUnderContractItemSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] RequiredFields = { "Item ID", "Description", "Status" };
        public KeyValuePair<string, string> ItemID = new KeyValuePair<string, string>("Item ID", "ITEMID_20181220060207");
        public KeyValuePair<string, string> Description = new KeyValuePair<string, string>("Description", "Description content");
        public KeyValuePair<string, string> ContractNumber = new KeyValuePair<string, string>("Contract Number", "1234567");
        public KeyValuePair<string, string> Status = new KeyValuePair<string, string>("Status", "OPEN - OPEN");
        public KeyValuePair<string, string> ItemNumber = new KeyValuePair<string, string>("Deliverable Line Item Number", "123456");
        public string SaveMessage = "Saved Successfully";
        public string GridViewName = "GridViewContractVendor";
        public int ExpanButtonIndex = 1;
        public int ExpanSubButtonIndex = 2;
    }
}
