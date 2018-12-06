﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class NavigateToModulesFromTheLeftNavSmoke
    {
        public string ProjectName = "Automation Project 1";

        public class DashboardModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public WidgitsList ListOfWidgits = new WidgitsList();

            public class WidgitsList
            {
                public string Mail = DashboardWidgit.MAIL.ToDescription();
                public string Documents = DashboardWidgit.DOCUMENTS.ToDescription();
                public string UnregisteredEmail = DashboardWidgit.UNREGISTEREDMAIL.ToDescription();
                public string Workllow = DashboardWidgit.WORKFLOW.ToDescription();
                public string Packages = DashboardWidgit.PACKAGES.ToDescription();
                public string Transmittals = DashboardWidgit.TRANSMITTALS.ToDescription();
                public string Tasks = DashboardWidgit.TASKS.ToDescription();
                public string MyStatistics = DashboardWidgit.MYSTATISTICS.ToDescription();
                public string ContractorView = DashboardWidgit.CONTRACTORVIEW.ToDescription();
                public string Forms = DashboardWidgit.FORMS.ToDescription();
                public string Gallery = DashboardWidgit.GALLERY.ToDescription();
                public string SubmissionView = DashboardWidgit.SUBMISSIONVIEW.ToDescription();
                public string ProjectAnnouncements = DashboardWidgit.PROJECTANNOUNCEMENTS.ToDescription();
                public string ProjectDescription = DashboardWidgit.PROJECTDESCRIPTION.ToDescription();
                public string UsefulContacts = DashboardWidgit.USEFULCONTACTS.ToDescription();
                public string ResponsesOutstanding = DashboardWidgit.RESPONSESOUTSTANDING.ToDescription();
                public string DocumentBarChart = DashboardWidgit.DOCMUENTBARCHART.ToDescription();
            }
        }

        public class DocumentsModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string DefaultFilter = "Latest Unrestrained";
            public string GridViewName = "GridViewDocReg";
            public string DocumentNo = "77777";
        }

        public class TransmittalsModules : NavigateToModulesFromTheLeftNavSmoke
        {

            public string[] SubItemMenus = { "Inbox", "Drafts", "Sent Items", "Pending" };
            public string SubPendingTitle = "Pending Transmittals";
            public string DefaultFilter = "Personal";
            public string DefaultFilterAtPendingPane = "By Recipient";
            public string TransmittalsModule = "Transmittals";
            public string GridViewName = "TransmittalRegisterGrid";
            public string GridViewPendingName = "RadGridRecipients";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> TransmittalNo = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.TransmittalNo.ToDescription(), "KIEWIT");
                public KeyValuePair<string, string> Package = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Package.ToDescription(), "74841");
                public KeyValuePair<string, string> Subject = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Subject.ToDescription(), "test subjest");
            }
        }
        public class PackagesModules : NavigateToModulesFromTheLeftNavSmoke
        {

            public string[] SubItemMenus = { "Inbox", "Drafts", "Sent Items" };
            public string DefaultFilter = "All";
            public string PackageModule = "Package";
            public string PackagesNode = "Packages";
            public string GridViewName = "GridViewPackage";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> PackageID = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.PackageID.ToDescription(), "74841");
                public KeyValuePair<string, string> PackageType = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.PackageType.ToDescription(), "Standard");
            }
        }

        public class TasksModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string[] SubItemMenus = { "Inbox", "Drafts", "Sent Items" };
            public string DefaultFilter = "Personal";
            public string GridViewName = "GridViewTask";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> Subject = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.Subject.ToDescription(), "Task");
            }
        }

        public class FormsModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string DefaultFilter = "Personal";
            public string GridViewName = "GridViewForms";
            public string FormsModule = "Forms";
        }

        public class GalleryModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string DefaultFilter = "All";
            public string GridViewName = "GridViewThumbnail";
            public string SortByValue = "Date Uploaded";
        }

        public class VendorDataModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string[] SubItemMenus = { "Vendor Data Register", "Holding Area" };
            public string VendorDataRegisterPaneName = "Vendor Data";
            public string HoldingAreaPaneName = "Holding Area -";
            public string DefaultFilterAtVendorDataRegisterPane = "Hierarchical View";
            public string DefaultFilterAtHoldingAreaPane = "New Documents";
            public string GridViewVendorDataRegisterName = "GridViewContractVendor";
            public string GridViewHoldingAreaName = "GridViewHoldingArea";
            public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

            public class ColumnValuesInCondition
            {
                public KeyValuePair<string, string> ContractNumber = new KeyValuePair<string, string>(MainPaneTableHeaderLabel.ContractNumber.ToDescription(), "");
            }
        }
    }

}
