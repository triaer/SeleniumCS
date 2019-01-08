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
        public DeliverableLine DeliverableInfo = new DeliverableLine()
        {
            ContractNumber = "2018-12-005",
            ItemID = "005-02",
            LineItemNumber = Utils.GetRandomValue("LineItemNumber"),
            Description = Utils.GetRandomValue("Description"),
            DeliverableType = "AR - ARCHITECTURAL (PRE-ENGINEERED METAL BUILDINGS)",
            Criticality = "Normal",
            Status = "COMPLETED - Completed",
        };
    }
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
