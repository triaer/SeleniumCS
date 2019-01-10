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
        public string VendorDataRegisterPaneName = "Vendor Data";
        public string GridViewVendorDataRegisterName = "GridViewContractVendor";
        public string DefaultFilterAtVendorDataRegisterPane = "Hierarchical View";
        public string RegisterView = "Default View";
        public string DownloadFilePath = Utils.GetDownloadFilesLocalPath() + "\\" + Utils.GetRandomValue("Contracts") + ".xlsx";
    }
}
