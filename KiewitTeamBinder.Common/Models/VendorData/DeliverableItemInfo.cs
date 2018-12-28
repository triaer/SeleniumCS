using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.VendorData
{
    public class DeliverableItemInfo
    {
        public string ContractNumber { get; set; }
        public string ItemID { get; set; }
        public string LineItemNumber { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Criticality { get; set; }
        public string Status { get; set; }
    }
}
