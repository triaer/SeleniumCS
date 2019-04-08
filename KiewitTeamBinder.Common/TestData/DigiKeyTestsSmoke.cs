using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class DigiKeyTestsSmoke
    {
        public string Category = "Battery Products";
        public string subCategory = "Accessories";
        public string QuantityOrder = "5";
        public string Reference = "ABC";
        public int Quantity = 3;
        public string[] MultiReference = { "Ref1", "ref2", "ref3" };
        public string [] ModifiedRef = { "ADF", "DFF", "ABSSSC" };
        public string [] ModifiedQuantity = { "10", "20", "30" };
        public int DeleteProducts = 2;

    }
}
