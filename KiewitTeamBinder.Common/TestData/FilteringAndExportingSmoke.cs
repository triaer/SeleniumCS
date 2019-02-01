using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common.Helper;


namespace KiewitTeamBinder.Common.TestData
{
    public class FilteringAndExportingSmoke
    {
        public string ProjectName = "Automation Project 2";
        public string ProjectID = "AUTO2";
        public string Contracts = "Contracts";
        public string ItemPurchased = "Item Purchased";
        public string Deliverables = "Deliverables";
        public string Documents = "Documents";

        public class GridViewNameModules
        {
            public string ContractsGrid = "GridViewContractsGrid";
            public string DeliverableGrid = "GridViewDeliverablesGrid";
            public string ItemPurchasedGrid = "GridViewItemsGrid";
            public string DocumentsGrid = "GridViewDocumentsGrid";
            public string ContractVendorPannel = "GridViewContractVendorPanel";
            public string HierarchicalView = "GridViewContractVendor";
            public string ExpeditingContracts = "RadGridExpeditingContracts";
            public string AddDocument = "GridView_GridData";
            public string LinkItems = "LinkedDocumentsGrid";
            public string LinkItemsData = "LinkedDocumentsGrid_GridData";
            public string HoldingArea = "GridViewHoldingArea";
            public string DocumentRegister = "GridViewDocReg";
        }

        public string[] SubItemMenus = { KiewitTeamBinderENums.MainPaneHeaderDropdownItem.Contracts.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.PurchaseItems.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.Deliverables.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.DocumentsAssociated.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.ExpeditingView.ToDescription() };

        public string DefaultFilter = "Hierarchical View";
        public string GridViewFilter = "Grid View";
        public string RegisterViewFilter = "Default View";
        public string ExpeditingViewFilter = "Expediting View";
        public string FilterValue = "CLOSED-OUT - Closed Out";

        public string DownloadContractsFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("Contracts") + ".xlsx";
        public string DownloadExpeditingFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("ExpeditingContracts") + ".xlsx";
        public string DefaultDownloadedFolderPath = Utils.GetDownloadFilesLocalPath();

        public string ContractNumber = "1234567";
        public string ContractNumberDescription = "testing 120793";
        public string ItemID = "123";
        public string ItemDescription = "abc";
        public string DeliverableNumber = "123456";
        public string DeliverableDescription = "123456";
               
        public string LinkItemsWindow = "Link Items";
        public string DocumentSubmitedPage = "Holding Area - Documents Submitted";
        public string NewDocumentSubmitedPage = "Holding Area - Documents Submitted - New";
        public string AcceptedDocumentSubmitedPage = "Holding Area - Documents Submitted - Accepted";
        public string ReturnedToVendorDocumentsPage = "Documents - To be returned to Vendor";
        public string ContractsVendorDataPage = "Vendor Data - Contracts";

        public string OptionAll = "All";
        public string SaveMessageOnLinkItem = "Manual Links updated successfully.";
        public int PageSizeDefault = 100;
    }
}
