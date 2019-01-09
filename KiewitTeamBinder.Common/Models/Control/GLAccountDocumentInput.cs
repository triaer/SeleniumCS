using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Control
{
    public class GLAccountDocumentInput
    {
        public string DocumentDate { get; set; }
        public string PostingDate { get; set; }
        public string Reference { get; set; }
        public string DocHeaderText { get; set; }
        public string CrossCCNo { get; set; }
        public string CostJob { get; set; }
        public string Currency { get; set; }

        public string[] GLLineItemHeader { get; set; }
        public string[] GLLineItemInfo_1 { get; set; }
        public string[] GLLineItemInfo_2 { get; set; }

    }
}
