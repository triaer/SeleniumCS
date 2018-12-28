using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DeliverableItemInfo DeliverableItemInfo = new DeliverableItemInfo()
        {
            ContractNumber = "2018-12-005",
            ItemID = "005-02",
            LineItemNumber = Utils.GetRandomValue("LGVN_LineItemNumber"),
            Description = Utils.GetRandomValue("Description"),
            Type = "AR - ARCHITECTURAL (PRE-ENGINEERED METAL BUILDINGS)",
            Criticality = "High",
            Status = "COMPLETED - Completed",
        };
    }
}
