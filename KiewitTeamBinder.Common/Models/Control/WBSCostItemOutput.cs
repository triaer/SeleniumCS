using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Control
{
    public class WBSCostItemOutput
    {
        public double ForecastQty { get; set; }

        public int WBSPhaseCode { get; set; }

        public double TotalCurrentBudget { get; set; }

        public double QtyCurrentBudget { get; set; }

        public double MHRsCurrentBudget { get; set; }

        public double TotalCurrentEstimate { get; set; }

        public double QtyCurrentEstimate { get; set; }

        public double MHRsCurrentEstimate { get; set; }

        public double TotalLiveForeCast { get; set; }

        public double MHRsLiveForeCast { get; set; }
        public double TotalCostToDate { get; set; }
    }
}
