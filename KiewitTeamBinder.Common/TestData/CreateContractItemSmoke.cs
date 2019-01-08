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
        public string ProjectName = "Automation Project 5";
        public string[] RequiredFields = { "Contract Number", "Description", "Vendor Company", "Expediting Contract", "Status" };
        public string[] SubItemMenus = { "Vendor Data Register", "Holding Area" };
        public string DefaultFilter = "Hierarchical View";
        public string VendorDataRegisterPaneName = "Vendor Data";
        public string TableVendorDataRegister = "GridViewContractVendor";
        public string SaveMessage = "Saved Successfully";

        public Contract ContractInfo = new Contract()
        {
            ContractNumber = Utils.GetRandomValue("CONTRACT_NO"),
            Description = Utils.GetRandomValue("Description Contract"),
            VendorCompany = Utils.GetRandomValue("Kiewit"),
            ExpeditingContract = "No",
            Status = "STARTED - STARTED"
        };
        

        public string[] AllWidgitsInDashboardSection =
            {
                    DashboardWidgit.MAIL.ToDescription(),
                    DashboardWidgit.DOCUMENTS.ToDescription(),
                    DashboardWidgit.UNREGISTEREDMAIL.ToDescription(),
                    DashboardWidgit.WORKFLOW.ToDescription(),
                    DashboardWidgit.PACKAGES.ToDescription(),
                    DashboardWidgit.TRANSMITTALS.ToDescription(),
                    DashboardWidgit.TASKS.ToDescription(),
                    DashboardWidgit.MYSTATISTICS.ToDescription(),
                    DashboardWidgit.CONTRACTORVIEW.ToDescription(),
                    DashboardWidgit.FORMS.ToDescription(),
                    DashboardWidgit.GALLERY.ToDescription(),
                    DashboardWidgit.SUBMISSIONVIEW.ToDescription(),
                    DashboardWidgit.PROJECTANNOUNCEMENTS.ToDescription(),
                    DashboardWidgit.PROJECTDESCRIPTION.ToDescription(),
                    DashboardWidgit.USEFULCONTACTS.ToDescription(),
                    DashboardWidgit.RESPONSESOUTSTANDING.ToDescription(),
                    DashboardWidgit.DOCMUENTBARCHART.ToDescription()
                };

    }

}
