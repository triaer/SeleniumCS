using System.ComponentModel;

namespace SelenCS.Common
{
    public class SelenCSENums
    {

        public enum GetDateTime
        {
            TODAY,
            YESTERDAY,
            TOMORROW,
            N_DAYS_AGO
        }

        public enum TravelerType
        {
            [Description("Solo traveler")]
            Solo,
            [Description("Couple/Pair")]
            Couples,
            [Description("Family travelers")]
            Families,
            [Description("Group travelers")]
            Group,
            [Description("Business travelers")]
            Business
        }
    }
}

      