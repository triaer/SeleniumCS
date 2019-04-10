using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikey.DataObjects
{
    public class CartItem
    {
        public Product _product;
        public int _quantity;
        public string _customerRef;


        public CartItem()
        {
            //init
        }

        public Product getProduct()
        {
            return this._product;
        }

        public int getQuantity()
        {
            return this._quantity;
        }

        public string getCustomerReference()
        {
            return this._customerRef;
        }

    }
}
