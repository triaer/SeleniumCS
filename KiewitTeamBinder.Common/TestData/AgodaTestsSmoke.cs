using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData
{
    public class AgodaTestsSmoke
    {
        public string Place = "Phu Quoc";
        public int Month = 1;
        public int Duration = 3;
        public string Rooms = "2";
        public string Guests = "4";
        public string HotelName = "Arcadia Phu Quoc Resort";
        public double LeftPercent = 10;
        public double RightPercent = 40;

        public BookingInfo bookingInfo = new BookingInfo()
        {
            Place = "Phu Quoc",
            Month = 1,
            Duration = 3,
            Rooms = "2",
            Guests = "4",
          

        };
    }

    public class BookingInfo
    {
        public string Place { get; set; }
        public int Month { get; set; }
        public int Duration { get; set; }
        public string Rooms { get; set; }
        public string Guests { get; set; }
   

    } 
}
