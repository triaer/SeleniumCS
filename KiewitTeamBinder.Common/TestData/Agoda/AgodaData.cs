using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.Agoda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.TestData.Agoda
{
    public class AgodaData
    {
        public PlaceToStay placeToStay = new PlaceToStay
        {
            Destination = "Phu Quoc",
            CheckinTime = "Next month",
            Duration = 3,
            TravelType = AgodaEnums.Occupancy.Group.ToDescription(),
            Room = 2,
            Adults = 4,
        };

        public string choosenPlace = "Arcadia Phu Quoc Resort";

        public Room room = new Room
        {
            IsFreeBreakfast = true,
            RoomType = "Standard Rom",
        };

        public CustomerInformation customerInfoFullname = new CustomerInformation
        {
            FullName = "Hanh Nguyen",
            Email = "hanhnguyen@gmail.com",
            RetypeEmail = "hanhnguyen@gmail.com",
        };
    }
}
