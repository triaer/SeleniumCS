using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common
{
    public class KiewitTeamBinderENums
    {

        public enum GetDateTime
        {
            TODAY,
            YESTERDAY,
            TOMORROW,
            N_DAYS_AGO
        }

        public enum TabMenuInTransmittalDetail
        {
            Message,
            Documents
        }

        public enum HelpMenuOptions
        {
            [Description("TeamBinder Help")]
            TeamBinderHelp,
            [Description("Frequently Asked Questions")]
            FrequentlyAskedQuestions,
            [Description("Training Videos")]
            TrainingVideos,
            [Description("Latest Release Notes")]
            LatestReleaseNotes,
            [Description("About")]
            About
        }

        public enum FilterOptions
        {
            [Description("No Filter")]
            NoFilter,
            [Description("Contains")]
            Contains,
            [Description("Does Not Contain")]
            DoesNotContain,
            [Description("Starts With")]
            StartsWith,
            [Description("Ends With")]
            EndsWith,
            [Description("Equal To")]
            EqualTo,
            [Description("Not  Equal To")]
            NotEqualTo,
            [Description("Is Empty")]
            IsEmpty,
            [Description("Is Not Empty")]
            IsNotEmpty,
        }

        public enum DocBulkUploadDropdownType
        {
            [Description("Rev")]
            Rev,
            [Description("Sts")]
            Sts,
            [Description("Discipline")]
            Disc,
            [Description("Category")]
            Cat,
            [Description("Type")]
            Type,
            [Description("PFLocation")]
            Location,
            [Description("SpecificationRef")]
            SpecReference,
            [Description("SubType")]
            SubType
        }

        public enum ToolbarButton
        {
            [Description("New Rows")]
            NewRows,
            [Description("Copy Attributes")]
            CopyAttributes,
            [Description("Get Attributes")]
            GetAttributes,
            [Description("Validate")]
            Validate,
            [Description("Remove Rows")]
            RemoveRows,
            [Description("Link Deliverable Items")]
            LinkDeliverableItems,
            [Description("Remove Deliverable Items")]
            RemoveDeliverableItems,
            [Description("Send")]
            Send,
            [Description("Close")]
            Close,
			[Description("Process")]
            Process,
            [Description("Save")]
            Save,
            [Description("More")]
            More,
            [Description("Add")]
            Add,
            [Description("Search")]
            Search,
            [Description("OK")]
            OK
        }

        public enum DocBulkUploadInputText
        {
            [Description("DocumentNo")]
            DocumentNo,
            [Description("Title")]
            Title,
            [Description("Due")]
            Due,
            [Description("Actual")]
            Actual,
            [Description("ForecastKiewit")]
            Forecast,
            [Description("AltNDocNo")]
            AltDocumentNo,
            [Description("IncTrnNo")]
            IncTrnNo,
            [Description("Error")]
            Error
        }

        public enum ViewFilterOptions
        {
            [Description("Personal")]
            Personal,
            [Description("Company")]
            Company,
            [Description("New Documents")]
            NewDocument,
            [Description("All")]
            All
        }

        public enum ModuleNameInLeftNav
        {
            [Description("Mail")]
            MAIL,
            [Description("Transmittals")]
            TRANSMITTALS,
            [Description("Packages")]
            PACKAGES,
            [Description("Tasks")]
            TASKS,
            [Description("Dashboard")]
            DASHBOARD,
            [Description("Documents")]
            DOCUMENTS,
            [Description("Forms")]
            FORMS,
            [Description("TeamPics")]
            GALLERY,
            [Description("VendorData")]
            VENDORDATA,
            [Description("PublishReports")]
            PUBLISHEDREPORTS,
        }

        public enum MainPaneTableHeaderButton
        {
            [Description("New")]
            New,
            [Description("New Rows")]
            NewRows,
            [Description("Copy Attributes")]
            CopyAttributes,
            [Description("Get Attributes")]
            GetAttributes,
            [Description("Validate")]
            Validate,
            [Description("Remove Rows")]
            RemoveRows,
            [Description("Link Deliverable Items")]
            LinkDeliverableItems,
            [Description("Remove Deliverable Items")]
            RemoveDeliverableItems,
            [Description("Reply")]
            Reply,
            [Description("Reply All")]
            ReplyAll,
            [Description("Forward")]
            Forward,
            [Description("Refresh")]
            Refresh,
            [Description("Print")]
            Print,
            [Description("Export")]
            Export,
            [Description("More")]
            More,
            [Description("Transmit")]
            Transmit,
            [Description("Process Documents")]
            ProcessDocuments,
        }

        public enum MainPaneTableHeaderLabel
        {
            [Description("Package ID")]
            PackageID,
            [Description("Package Type")]
            PackageType,
            [Description("Title")]
            Title,
            [Description("Transmittal No.")]
            TransmittalNo,
            [Description("Attach")]
            Attachment,
            [Description("Package")]
            Package,
            [Description("Received")]
            Received,
            [Description("Subject")]
            Subject,
            [Description("From")]
            From,
            [Description("Message")]
            Message,
            [Description("To")]
            To,
            [Description("TaskCount")]
            TaskCount,
            [Description("Contract Number")]
            ContractNumber,
            [Description("Document No.")]
            DocumentNo,
            [Description("Hold Process Status")]
            HoldProcessStatus,
            [Description("Status")]
            Status
        }

        public enum MainPaneHeaderDropdownItem
        {
            [Description("Create Transmittals")]
            CreateTransmittals,
            [Description("Contract")]
            Contract,
            [Description("Item Purchased")]
            ItemPurchased,
            [Description("Deliverable Line Item")]
            DeliverableLineItem,
            [Description("Link Items")]
            LinkItems,
            [Description("Documents")]
            Documents,
            [Description("Register View")]
            RegisterView,
            [Description("Contracts")]
            Contracts,
            [Description("Purchase Items")]
            PurchaseItems,
            [Description("Deliverables")]
            Deliverables,
            [Description("Documents Associated with Deliverables")]
            DocumentsAssociated,
            [Description("Expediting View")]
            ExpeditingView
        }

        public enum DialogPopupButton
        {
            Yes,
            No,
            OK

        }

        public enum ModuleSubMenuInLeftNav
        {
            [Description("Inbox")]
            INBOX,
            [Description("Drafts")]
            DRAFTS,
            [Description("Sent Items")]
            SENTITEMS,
            [Description("Pending")]
            PENDING,
            [Description("Unregistered")]
            UNREGISTERED,
            [Description("Deleted Items")]
            DELETEDITEMS,
            [Description("Vendor Data Register")]
            VENDODATAREGISTER,
            [Description("Holding Area")]
            HOLDINGAREA
        }

        public enum DashboardWidgetLabel
        {
            [Description("Mail")]
            MAIL,
            [Description("Documents")]
            DOCUMENTS,
            [Description("Unregistered Mail")]
            UNREGISTEREDMAIL,            
            [Description("Workflow")]
            WORKFLOW,
            [Description("Packages")]
            PACKAGES,
            [Description("Transmittals")]
            TRANSMITTALS,
            [Description("Tasks")]
            TASKS,
            [Description("My Statistics")]
            MYSTATISTICS,
            [Description("Contractor View")]
            CONTRACTORVIEW,
            [Description("Forms")]
            FORMS,
            [Description("Gallery")]
            GALLERY,
            [Description("Submission View (Vendor)")]
            SUBMISSIONVIEW,
            [Description("Project Announcements")]
            PROJECTANNOUNCEMENTS,
            [Description("Project Description")]
            PROJECTDESCRIPTION,
            [Description("Useful Contacts")]
            USEFULCONTACTS,
            [Description("Responses Outstanding and Overdue Mail Received")]
            RESPONSESOUTSTANDING,
            [Description("Document Bar Chart")]
            DOCMUENTBARCHART
        }

        public enum ContractField
        {
            [Description("Contract Number")]
            ContractNumber,
            [Description("Description")]
            Description,
            [Description("Vendor Company")]
            VendorCompany,
            [Description("Expediting Contract")]
            ExpeditingContract,
            [Description("Status")]
            Status
        }
        public enum ItemPurchasedField
        {
            [Description("Item ID")]
            ItemID,
            [Description("Description")]
            Description,
            [Description("Contract Number")]
            ContractNumber,
            [Description("Status")]
            Status
        }

        public enum DeliverableField
        {
            [Description("Contract Number")]
            ContractNumber,
            [Description("Item ID")]
            ItemID,
            [Description("Deliverable Type")]
            DeliverableType,
            [Description("Criticality")]
            Criticality,
            [Description("Status")]
            Status,
            [Description("Deliverable Line Item Number")]
            LineItemNumber,
            [Description("Description")]
            Description
        }
        public enum ModuleSubMenuInAddFunction
        {
            [Description("Mail")]
            Mail,
            [Description("Documents")]
            Documents,
            [Description("Transmittals")]
            Transmittals,
            [Description("Packages")]
            Packages,
            [Description("Forms")]
            Forms,
            [Description("Gallery")]
            Gallery,
            [Description("Deliverables")]
            Deliverables,
            [Description("Holding Area")]
            HoldingArea
        }

        public enum WidgetUniqueName
        {
            [Description("Mail")]
            MAIL,
            [Description("Documents")]
            DOCUMENTS,
            [Description("UnregisteredMail")]
            UNREGISTEREDMAIL,
            [Description("Workflow")]
            WORKFLOW,
            [Description("Packages")]
            PACKAGES,
            [Description("Transmittal")]
            TRANSMITTALS,
            [Description("Task")]
            TASKS,
            [Description("MyStatistic")]
            MYSTATISTICS,
            [Description("ContractorView")]
            CONTRACTORVIEW,
            [Description("Forms")]
            FORMS,
            [Description("Gallery")]
            GALLERY,
            [Description("SubmissionView")]
            SUBMISSIONVIEW,
            [Description("ProjectAnnouncements")]
            PROJECTANNOUNCEMENTS,
            [Description("ProjectDescription")]
            PROJECTDESCRIPTION,
            [Description("UsefulContact")]
            USEFULCONTACTS,
            [Description("Chart")]
            RESPONSESOUTSTANDING,
            [Description("DocumentChart")]
            DOCMUENTBARCHART
        }

        public enum DocumentDetailHeader
        {
            [Description("Document No.")]
            DocumentNo,
            [Description("Link")]
            Link,
            [Description("Description")]
            Description,
            [Description("Superseded Rev")]
            SupersededRev,
            [Description("Rev")]
            Rev,
            [Description("Disc")]
            Discipline,
            [Description("Version")]
            Version,
            [Description("Sts")]
            Status,
            [Description("Title")]
            Title,
            [Description("Received")]
            Received,
            [Description("Cat")]
            Category,
            [Description("Area")]
            Area,
            [Description("Type")]
            Type,
        }

        public enum ModuleSubItemsInContractorView
        {
            [Description("Documents Submitted")]
            DocumentsSubmitted,
            [Description("To be returned to Vendor")]
            ReturnedToVendor,
            [Description("Reviewed documents returned")]
            ReviewedDocuments,
            [Description("Deliverables due in next 30 days")]
            DeliverablesDue,
            [Description("Contracts")]
            Contracts
        }

        public enum ModuleExpandedItemsInContractorView
        {
            [Description("New")]
            NEW,
            [Description("Accepted")]
            ACCEPTED,
            [Description("CLOSED-OUT")]
            CLOSEDOUT,
            [Description("OPEN")]
            OPEN,
            [Description("ON HOLD")]
            ONHOLD,
            [Description("IN REVIEW")]
            INREVIEW,
            [Description("STARTED")]
            STARTED,
            [Description("Approved")]
            APPROVED
        }
        public enum StandardReportsTab
        {
            Reports,
            Scheduled,
            Favorites
        }
        public enum StandardReportsButtonHeader
        {
            [Description("Add to Favorites")]
            AddFavorites,
            [Description("Remove from Favorites")]
            RemoveFavorites
        }
        public enum FavoriteReportFor
        {
            Myself,
            [Description("My company")]
            MyCompany,
            [Description("My project")]
            MyProject
        }
    }
}

      