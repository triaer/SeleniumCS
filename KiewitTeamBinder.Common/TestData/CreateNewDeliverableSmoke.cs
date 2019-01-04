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
    public class CreateNewDeliverableSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] SubItemMenus = { "Contract", "Item Purchased", "Deliverable Line Item" };
        public string[] SubItemOfMoreFunction = { "Link Items" };
        public string DeliverableWindowTitle = "AUTO1 - New Deliverable Item";
        public string LinkItemsWindowTitle = "Link Items";
        public string[] RequiredField = { "Item ID", "Deliverable Line Item Number", "Description", "Deliverable Type", "Criticality" };
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
        public DeliverableItemInfo DeliverableItemInfo = new DeliverableItemInfo()
        {
            ContractNumber = "2018-12-005",
            ItemID = "005-02",
            LineItemNumber = Utils.GetRandomValue("LineItemNumber"),
            Description = Utils.GetRandomValue("Description"),
            Type = "AR - ARCHITECTURAL (PRE-ENGINEERED METAL BUILDINGS)",
            Criticality = "Normal",
            Status = "COMPLETED - Completed",
        };
    }
}
