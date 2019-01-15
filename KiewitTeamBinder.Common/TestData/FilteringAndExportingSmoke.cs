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
        public string HierarchicalGridViewName = "GridViewContractVendor";
        public string GridGridViewName = "GridViewContractsGrid";
        public string DefaultFilter = "Hierarchical View";
        public string GridViewFilter = "Grid View";
        public string RegisterView = "Default View";
        public string RowName = "Contracts";
        public string DownloadFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("Contracts") + ".xlsx";
        public string FilterValue = "CLOSED-OUT - Closed Out";
    }
}
