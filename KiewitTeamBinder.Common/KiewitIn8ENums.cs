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
          
        public enum SettingPage
        {
            [Description("User management")]
            UserManagement,
            [Description("Project settings")]
            ProjectSettings,
            [Description("System")]
            System,
            [Description("Roles and permissions")]
            RolesAndPermissions,
            [Description("Organizational breakdown structure")]
            OrganizationalBreakdownStructure,
            [Description("Organization settings")]
            OrganizationSettings,
            [Description("Application integrations")]
            ApplicationIntegrations,
            [Description("Library")]
            Library,
            [Description("Account code structure")]
            Accountcodestructure,
            [Description("Cost center")]
            CostCenter,
            [Description("Currencies")]
            Currencies,
            [Description("Disciplines and commodities")]
            DisciplinesAndCommodities,
            [Description("Documents")]
            Documents,
            [Description("Field attributes")]
            FieldAttributes,
            [Description("Operational resources")]
            OperationalResources,
            [Description("Qualifications")]
            Qualifications,
            [Description("Schedules")]
            Schedules,
            [Description("Unions")]
            Unions,
            [Description("Units of measure")]
            UnitsOfMeasure,
            [Description("Vendors")]
            Vendors,
            [Description("Workflows")]
            Workflows,
            [Description("Projects")]
            Projects
        }

        
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

    }

}
      