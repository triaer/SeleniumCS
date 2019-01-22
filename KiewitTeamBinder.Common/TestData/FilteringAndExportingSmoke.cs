﻿using System;
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
        public string ItemPurchasedHeader = "Item Purchased";
        public string DeliverablesHeader = "Deliverables";
        public string DocumentsHeader = "Documents";
        public string HierarchicalGridViewName = "GridViewContractVendor";
        public string GridGridViewName = "GridViewContractsGrid";
        public string DefaultFilter = "Hierarchical View";
        public string GridViewFilter = "Grid View";
        public string ExpeditingViewFilter = "Expediting View";
        public string RegisterView = "Default View";
        public string RowName = "Contracts";
        public string DownloadFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("Contracts") + ".xlsx";
        public string FilterValue = "CLOSED-OUT - Closed Out";
        public string[] SubItemMenus = { KiewitTeamBinderENums.MainPaneHeaderDropdownItem.Contracts.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.PurchaseItems.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.Deliverables.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.DocumentsAssociated.ToDescription(),
                                         KiewitTeamBinderENums.MainPaneHeaderDropdownItem.ExpeditingView.ToDescription() };

        public string GridView = "Grid View";
        public string GridViewContract = "GridViewContractsGrid";
        public string GridViewDeliverable = "GridViewDeliverablesGrid";
        public string GridViewItem = "GridViewItemsGrid";
        public string GridViewDocument = "GridViewDocumentsGrid";
        public string GridExpeditingContracts = "RadGridExpeditingContracts";


        public string ContractNumber = "1234567";
        public string ItemIDNumber = "123";
        public string DeliverableNumber = "123456";
        public string DocumentNumber = "05218-MM-00001";

        public string ContractNumberDescription = "testing 120793";
        public string ItemDescription = "abc";
        public string DeliverableDescription = "123456";

        public int pageSizeDefault = 100;


    }
}
