using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.VendorData
{
    public class Contract
    {
        public string ContractNumber { get; set; }
        public string Description { get; set; }
        public string VendorCompany { get; set; }
        public string ExpeditingContract { get; set; }
        public string Status { get; set; }
    }
    public class ItemPurchased
    {
        public string ItemID { get; set; }
        public string Description { get; set; }
        public string ContractNumber { get; set; }
        public string Status { get; set; }
    }
    public class DeliverableLine
    {
        public string ContractNumber { get; set; }
        public string ItemID { get; set; }
        public string LineItemNumber { get; set; }
        public string Description { get; set; }
        public string DeliverableType { get; set; }
        public string Criticality { get; set; }
        public string Status { get; set; }
    }
}
