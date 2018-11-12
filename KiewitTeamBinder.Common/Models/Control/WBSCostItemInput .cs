using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Control
{
    public class WBSCostItemInput
    {
        public string Description { get; set; }

        public string AccountCode { get; set; }

        public double ForecastQty { get; set; }

        public int OptionIndxForecastQty { get; set; }

        public double FinalCost { get; set; }

        public double FinalMHrs { get; set; }

        public int OptionIndxFinalMHrs { get; set; }

        public double TotalEquipmentHrs { get; set; }

        public int OptionIndxTotalEquipmentHrs { get; set; }

        public string AllowAsBuiltSelection { get; set; }

        public string CostSource { get; set; }

        public string ForecastMethod { get; set; }
    }
}
