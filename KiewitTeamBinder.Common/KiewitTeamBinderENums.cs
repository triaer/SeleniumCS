﻿using System;
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

        public enum DocBulkUploadHeaderButton
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
            RemoveDeliverableItems
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
            Company
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
            [Description("TeamPics")]
            GALLERY,
            [Description("VendorData")]
            VENDORDATA
        }

        public enum MainPaneTableHeaderButton
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
            More
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
            ContractNumber
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
            [Description("Vendor Data Register")]
            VENDODATAREGISTER,
            [Description("Holding Area")]
            HOLDINGAREA
        }

        public enum DashboardWidgit
        {
            [Description("Mail")]
            MAIL,
            [Description("Unregistered Mail")]
            UNREGISTERED_MAIL,
            [Description("Documents")]
            DOCUMENTS,
            [Description("Workflow")]
            WORKFLOW,
            [Description("Packages")]
            PACKAGES,
            [Description("Transmittals")]
            TRANSMITTALS,
            [Description("Tasks")]
            TASKS,
            [Description("My Statistics")]
            MY_STATISTICS,
            [Description("Contractor View")]
            CONTRACTOR_VIEW,
            [Description("Forms")]
            FORMS,
            [Description("Gallery")]
            GALLERY,
            [Description("Submission View (Vendor)")]
            SUBMISSION_VIEW,
        }
    }
}

      