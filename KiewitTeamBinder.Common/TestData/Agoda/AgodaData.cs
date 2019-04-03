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

        public PlaceToStay placeToStayWithCurrency = new PlaceToStay
        {
            Destination = "Phu Quoc",
            CheckinTime = "Next month",
            Duration = 3,
            TravelType = AgodaEnums.Occupancy.Group.ToDescription(),
            Room = 2,
            Adults = 4,
            Currency = AgodaEnums.Currency.USD.ToDescription(),
        };

        public string choosenPlace = "Arcadia Phu Quoc Resort";

        public Room room = new Room
        {
            IsFreeBreakfast = true,
            RoomType = "Premier Beach Front",
            RoomPosition = 1
        };

        public CustomerInformation customerInfoFullname = new CustomerInformation
        {
            FullName = "Hanh Nguyen",
            Email = "hanhnguyen@gmail.com",
            RetypeEmail = "hanhnguyen@gmail.com",
            MobileNumber = Utils.RandomNumberString(10)
        };

        public CustomerInformation customerInfor = new CustomerInformation
        {
            FirstName = "Hanh",
            LastName = "Nguyen",
            Email = "hanhnguyen@gmail.com",
            RetypeEmail = "hanhnguyen@gmail.com",
            MobileNumber = Utils.RandomNumberString(10)
        };
    }
}
