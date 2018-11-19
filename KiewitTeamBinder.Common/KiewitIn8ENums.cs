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

        public enum VendorDataMenusForVendorAccount
        {
            [Description("Holding Area")]
            HoldingArea
        }

        public enum VendorDataMenusForAdminAndStandardAccount
        {
            [Description("Holding Area")]
            HoldingArea,
            [Description("Vendor Data Register")]
            VendorDataRegister
        }

        public enum TableComboBoxType
        {
            [Description("Rev")]
            Rev,
            [Description("Sts")]
            Sts,
            [Description("Discipline")]
            Discipline,
            [Description("Category")]
            Category,
            [Description("Type")]
            Type,
            [Description("PFLocation")]
            PFLocation,
            [Description("SpecificationRef")]
            SpecificationRef,
            [Description("SubType")]
            SubType,
            HoldingArea,
        }
    }
}
      