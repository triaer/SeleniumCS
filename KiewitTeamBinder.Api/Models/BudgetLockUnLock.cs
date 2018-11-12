using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class BudgetLockUnLock
    {
        [JsonProperty(PropertyName = "costItemIds")]
        public IList<object> CostItemIds { get; set; }
        [JsonProperty(PropertyName = "projectId")]
        public int ProjectId { get; set; }
        [JsonProperty(PropertyName = "accountId")]
        public int AccountId { get; set; }
    }
}
