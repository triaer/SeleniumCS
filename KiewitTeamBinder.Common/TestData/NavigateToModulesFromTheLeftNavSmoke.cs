using System;
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
            public string[] AllWidgitsInDashboardSection =
                {
                    DashboardWidgetLabel.MAIL.ToDescription(),
                    DashboardWidgetLabel.DOCUMENTS.ToDescription(),
                    DashboardWidgetLabel.UNREGISTEREDMAIL.ToDescription(),
                    DashboardWidgetLabel.WORKFLOW.ToDescription(),
                    DashboardWidgetLabel.PACKAGES.ToDescription(),
                    DashboardWidgetLabel.TRANSMITTALS.ToDescription(),
                    DashboardWidgetLabel.TASKS.ToDescription(),
                    DashboardWidgetLabel.MYSTATISTICS.ToDescription(),
                    DashboardWidgetLabel.CONTRACTORVIEW.ToDescription(),
                    DashboardWidgetLabel.FORMS.ToDescription(),
                    DashboardWidgetLabel.GALLERY.ToDescription(),
                    DashboardWidgetLabel.SUBMISSIONVIEW.ToDescription(),
                    DashboardWidgetLabel.PROJECTANNOUNCEMENTS.ToDescription(),
                    DashboardWidgetLabel.PROJECTDESCRIPTION.ToDescription(),
                    DashboardWidgetLabel.USEFULCONTACTS.ToDescription(),
                    DashboardWidgetLabel.RESPONSESOUTSTANDING.ToDescription(),
                    DashboardWidgetLabel.DOCMUENTBARCHART.ToDescription()
                };
        }

        public class MailModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string[] SubItemMenus = { "Inbox", "Drafts", "Sent Items", "Unregistered", "Deleted Items" };

            public string DefaultFilter_Personal = "Personal";
            public string DefaultFilter_Company = "Company";
            public string GridViewName = "GridViewMail";
            

            public MailInformation mailInformation = new MailInformation()
            {
                From = "Automation Admin2",
                Type = "PCO",
                Subject = "TestMail",
                Date = "12-11-18 04:51 PM",
            };
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

        public class PublishedReportsModules : NavigateToModulesFromTheLeftNavSmoke
        {
            public string[] ListButton = { "Delete", "Download", "Create New Task" };
        }
    }
    public class MailInformation
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
    }

}
