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
    public class CreateContractItemSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] RequiredFields = { "Contract Number", "Description", "Vendor Company", "Expediting Contract", "Status" };
        public string[] SubItemMenus = { "Vendor Data Register", "Holding Area" };
        public string DefaultFilter = "Hierarchical View";
        public string VendorDataRegisterPaneName = "Vendor Data";
        public string TableVendorDataRegister = "GridViewContractVendor";
        public string SaveMessage = "Saved Successfully";
        public string WidgetUniqueName = KiewitTeamBinderENums.WidgetUniqueName.CONTRACTORVIEW.ToDescription();
        public string RowName = "Contracts";

        public Contract ContractInfo = new Contract()
        {
            ContractNumber = Utils.GetRandomValue("CONTRACT_NO"),
            Description = Utils.GetRandomValue("Description Contract"),
            VendorCompany = "Kiewit",
            ExpeditingContract = "No",
            Status = "STARTED - STARTED"
        };

    }

}
