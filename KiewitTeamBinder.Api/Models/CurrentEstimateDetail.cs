using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class CurrEstimateDetail
    {
        public string CostItemIds { get; set; }
        public string PlugColumn { get; set; }
        public string CETotalEqpHours { get; set; }
        public string CEFinalMH { get; set; }
        public string CEConstEqpTotalCost { get; set; }
        public string CELaborTotalCost { get; set; }
        public string CEFinalCost { get; set; }
        public string CELaborCostPerManHour { get; set; }
    }

    public class CurrEstimateDetailMessage
    {
        public IList<CurrEstimateDetail> CurrEstimateDetails { get; set; }
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
    }


}
