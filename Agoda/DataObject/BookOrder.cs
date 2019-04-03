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

        }

        public BookOrder(string language = "EN")
        {
            //init
            if (language.Equals("EN"))
                destination = "Phu Quoc";
        }
    }
}
