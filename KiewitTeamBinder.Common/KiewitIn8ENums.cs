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

        public enum ViewFilterOptions
        {
            [Description("Personal")]
            Personal,
            [Description("Company")]
            Company
        }

        public enum ProjectDashboardModuleName
        {
            [Description("Mail")]
            Mail,
            [Description("Transmittals")]
            Transmittals,
            [Description("Package")]
            Package,
            [Description("Tasks")]
            Tasks
        }

        public enum ProjectDashboardMailSubMenuItems
        {
            [Description("Inbox")]
            Inbox,
            [Description("Drafts")]
            Drafts,
            [Description("Sent Items")]
            SentItems,
            [Description("Unregistered")]
            Unregistered,
            [Description("Deleted Items")]
            DeletedItems,
        }
    }
}
      