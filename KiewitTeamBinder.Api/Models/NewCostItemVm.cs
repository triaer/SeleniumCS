using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class NewCostItemVm
    {
        public int CostItemID { get; set; }
        public int SelectedCostItemId { get; set; }
        public string NewCbsPosition { get; set; }
        public string NewWbsPhasecode { get; set; }
        public int IsInherited { get; set; }
        public bool IsSubOrdinate { get; set; }
        public string RegenarateWBSPhaseCode { get; set; }
        public bool IsRegenarateWBSPhaseCode { get; set; }
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public string Currency { get; set; }
    }

    public class NewCostItemVmMessage
    {
        public NewCostItemVm newCostItemVm { get; set; }
    }
}
