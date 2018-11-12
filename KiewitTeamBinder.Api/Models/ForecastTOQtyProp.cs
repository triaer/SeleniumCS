using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class ForecastTOQtyPropVm
    {
        public IList<int> CostItemIds { get; set; }
        public string ForecastTOQtyValue { get; set; }
        public string ProportionalColumnName { get; set; }
        public bool KeepCostAsItIs { get; set; }
        public bool IsCEFinalMH { get; set; }
        public bool IsCETotalEqpHours { get; set; }
    }

    public class ForecastTOQtyPropMessage
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public ForecastTOQtyPropVm ForecastTOQtyPropVm { get; set; }
    }

}
