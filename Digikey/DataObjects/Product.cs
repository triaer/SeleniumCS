using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikey.DataObjects
{
    public class Product
    {
        public string _digiKey;
        public string _mfgPartNumber;
        public string _manufacturer;

        public Product()
        {
            //init
        }

        public Product(string _digiKey, string _mfgPartNumber, string _manufacturer)
        {
            this._digiKey = _digiKey;
            this._mfgPartNumber = _mfgPartNumber;
            this._manufacturer = _manufacturer;
        }

        public string getDigiKey()
        {
            return this._digiKey;
        }

        public string getMfgPartNumber()
        {
            return this._mfgPartNumber;
        }

        public string getManufacturer()
        {
            return this._manufacturer;
        }

        private void test()
        {
            this._manufacturer = "test";
            
        }
    }
}
