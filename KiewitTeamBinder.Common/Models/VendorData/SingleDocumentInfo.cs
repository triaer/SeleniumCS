using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.VendorData
{
    public class SingleDocumentInfo
    {
        public string DocumentNo { get; set; }
        public string RevStatus { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Discipline { get; set; }
        public string Type { get; set; }
    }

    public class PurchasedItemInfo
    {
        public KeyValuePair<string, string> ItemID { get; set; }
        public KeyValuePair<string, string> Description { get; set; }
        public KeyValuePair<string, string> ContractNumber { get; set; }
        public KeyValuePair<string, string> Status { get; set; }
    }
}
