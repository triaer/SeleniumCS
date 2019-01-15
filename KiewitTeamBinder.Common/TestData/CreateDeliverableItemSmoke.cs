using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.Common.TestData
{
    public class CreateDeliverableItemSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] SubItemMenus = { "Contract", "Item Purchased", "Deliverable Line Item" };
        public string[] SubItemOfMoreFunction = { "Link Items" };
        public string DeliverableWindowTitle = "AUTO1 - New Deliverable Item";
        public string LinkItemsWindowTitle = "Link Items";
        public string[] RequiredFields = { "Item ID", "Deliverable Line Item Number", "Description", "Deliverable Type", "Criticality" };
        public string SaveMessage = "Saved Successfully";
        public string[] SubItemOfAddFunction = {
            ModuleSubMenuInAddFunction.Mail.ToDescription(),
            ModuleSubMenuInAddFunction.Documents.ToDescription(),
            ModuleSubMenuInAddFunction.Transmittals.ToDescription(),
            ModuleSubMenuInAddFunction.Packages.ToDescription(),
            ModuleSubMenuInAddFunction.Forms.ToDescription(),
            ModuleSubMenuInAddFunction.Gallery.ToDescription(),
            ModuleSubMenuInAddFunction.Deliverables.ToDescription(),
            ModuleSubMenuInAddFunction.HoldingArea.ToDescription()
        };
        public string DocumentNo = "AUTO1";
        public string GridViewAddDocName = "GridView_GridData";
        public string GridViewLinkItemsName = "LinkedDocumentsGrid_GridData";
        public string SaveMessageOnLinkItem = "Manual Links updated successfully.";
        public DeliverableLine DeliverableInfo = new DeliverableLine
        {
            ContractNumber = "2018-12-005",
            ItemID = "005-01",
            LineItemNumber = Utils.GetRandomValue("LineItemNumber"),
            Description = Utils.GetRandomValue("Description Deliverable"),
            DeliverableType = "AR - ARCHITECTURAL (PRE-ENGINEERED METAL BUILDINGS)",
            Criticality = "Normal",
            Status = "OPEN - OPEN",
        };
        
        public string RowName = "Deliverables";

        public List<KeyValuePair<string, string>> ExpectedContractValuesInColumnList(DeliverableLine DeliverableInfo)
        {
            return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Contract Number", DeliverableInfo.ContractNumber) };
        }

        public List<KeyValuePair<string, string>> ExpectedPurchasedValuesInColumnList(DeliverableLine DeliverableInfo)
        {
            return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Item ID", DeliverableInfo.ItemID) };
        }

        public List<KeyValuePair<string, string>> ExpectedDeliverableValuesInColumnList(DeliverableLine DeliverableInfo)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Deliverable Line Item Number", DeliverableInfo.LineItemNumber),
                new KeyValuePair<string, string>("Description", DeliverableInfo.Description),
                new KeyValuePair<string, string>("Status", DeliverableInfo.Status)
            };
        }
        public string GridViewName = "GridViewContractVendor";
        public int ExpanButtonIndex = 1;
        public int ExpanSubButtonIndex = 2;
    }
    
}
