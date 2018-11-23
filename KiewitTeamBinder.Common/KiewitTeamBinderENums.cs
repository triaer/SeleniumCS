using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI
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
    }
}
      