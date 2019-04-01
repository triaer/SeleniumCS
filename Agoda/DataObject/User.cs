using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agoda.DataObject
{
    public class User
    {
        public string username = "Nguyen Tuan Vinh";
        public string emailAddress = "vinh.tuan.nguyen@logigear.com";
        public string mobileNumber = "0831234567";
        public string countryResidence = "Vietnam";

        public bool bookForSomeoneElse = true;
        public string guestName = "Vladimir Putin";
        public string guestResidence = "Russia";

        public bool smokingRoom = false;
        public bool largeBed = true;

        public User()
        {
            // init user.
        }


    }
}
