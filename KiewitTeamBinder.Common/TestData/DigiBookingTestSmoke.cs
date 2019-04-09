using KiewitTeamBinder.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class DigiBookingTestSmoke
    {
        public string Category = "Battery Products";
        public string SubCategory = "Accessories";
        public List<DigiProduct> Products = new List<DigiProduct>()
        {
            new DigiProduct()
            {
                KeyPartNumber = "36-117-ND",
                ManufacturerPartNumber = "117",
                Quantity = 5,
                CustomerReference = "Cus001"
            },

            new DigiProduct()
            {
                KeyPartNumber = "BI-UM-1-2-ND",
                ManufacturerPartNumber = "BI-UM-1-2",
                Quantity = 2,
                CustomerReference = "Cus002"
            },

            new DigiProduct()
            {
                KeyPartNumber = "432-1335-ND",
                ManufacturerPartNumber = "BDA10-RA",
                Quantity = 1,
                CustomerReference = "Cus001"
            }
        };

        public List<DigiProduct> EditedProducts = new List<DigiProduct>()
        {
            new DigiProduct()
            {
                KeyPartNumber = "36-117-ND",
                ManufacturerPartNumber = "117",
                Quantity = 10,
                CustomerReference = "Cus003"
            },

            new DigiProduct()
            {
                KeyPartNumber = "BI-UM-1-2-ND",
                ManufacturerPartNumber = "BI-UM-1-2",
                Quantity = 3,
                CustomerReference = "Cus001"
            },

            new DigiProduct()
            {
                KeyPartNumber = "432-1335-ND",
                ManufacturerPartNumber = "BDA10-RA",
                Quantity = 5,
                CustomerReference = "Cus002"
            }
        };

        public List<DigiProduct> DeletedProducts = new List<DigiProduct>()
        {
            new DigiProduct()
            {
                KeyPartNumber = "BI-UM-1-2-ND",
                ManufacturerPartNumber = "BI-UM-1-2"
            },

            new DigiProduct()
            {
                KeyPartNumber = "432-1335-ND",
                ManufacturerPartNumber = "BDA10-RA"
            }
        };
    }
}
