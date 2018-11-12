using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class CostCategoryDetailsMessage
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "value")]
        public IList<CostCategoryDetailsVm> CostCategoryMessage { get; set; }
    }

    public class CostCategoryDetailsVm
    {
        public int CostCategoryID { get; set; }
        public string Name { get; set; }
        public string DiplayLangName { get; set; }
        public int? ParentID { get; set; }
        public double CurrentEstimateValue { get; set; }
        public double CurrentBudgetValue { get; set; }
        public double JTDValue { get; set; }
        public double ForecastValue { get; set; }
        public double ForecastRemainingValue { get; set; }
        public bool IsExpanded { get; set; }
    }
}