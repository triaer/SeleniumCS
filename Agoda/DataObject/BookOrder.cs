using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agoda.DataObject
{
    public class BookOrder
    {
        public string destination = "Phu Quoc";
        public string checkInDate = null;
        public string checkoutDate = null;
        static public int guestNumber = 4;
        static public int roomInNeed = 2;

        public BookOrder()
        {
            //init
        }

        public BookOrder(string language = "EN")
        {
            //init
            switch (language)
            {
                case "EN":
                    destination = "Phu Quoc";
                    return;
                case "DE":
                    destination = "Phu Quoc";
                    return;
                case "JA":
                    destination = "フーコック島";
                    return;
                default:
                    destination = "Phu Quoc";
                    return;
            }
            
        }
    }
}
