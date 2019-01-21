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
        public string ProjectName = "Automation Project 1";
        public string HierarchicalGridViewName = "GridViewContractVendor";
        public string GridGridViewName = "GridViewContractsGrid";
        public string DefaultFilter = "Hierarchical View";
        public string GridViewFilter = "Grid View";
        public string RegisterView = "Default View";
        public string DownloadFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("Contracts") + ".xlsx";
        public string FilterValue = "CLOSED-OUT - Closed Out";

        public string GridView = "Grid View";
        public string GridViewContract = "GridViewContractsGrid";
        public string GridViewDeliverable = "GridViewDeliverablesGrid";
        public string GridViewItem = "GridViewItemsGrid";
        public string GridViewDocument = "GridViewDocumentsGrid";
        public string ContractViewWidget = "divWidgetContractorView";
        public string ContractsInContractView = "Contracts";

        public string ContractNumber = "1234567";
        public string ItemID = "123";
        public string DeliverableNumber = "123456";
        public string ContractNumberDescription = "testing 120793";
        public string ItemDescription = "abc";
        public string DeliverableDescription = "123456";

        public string DeliverableItemWindow = "AUTO1 - ";
        public string LinkItemsWindow = "Link Items";
        public string OptionAll = "All";
        public string GridViewAddDocName = "GridView_GridData";
        
        public string GridViewLinkItemsName = "LinkedDocumentsGrid";
        public string GridViewLinkItemsData = "LinkedDocumentsGrid_GridData";
        public string SaveMessageOnLinkItem = "Manual Links updated successfully.";

        public string Contracts = "Contracts";
        public string ItemPurchased = "Item Purchased";
        public string Deliverables = "Deliverables";
        public string Documents = "Documents";
        public string DocumentSubmitedPage = "Holding Area - Documents Submitted";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
    }
}
