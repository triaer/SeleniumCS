using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Common.Models
{
    public class DigiProduct
    {
        public string KeyPartNumber { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int Quantity { get; set; }
        public string CustomerReference { get; set; }
    }
}
