using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common
{
    public class AgodaEnums
    {
        public enum Occupancy
        {
            [Description("traveler-solo")]
            Solo,
            [Description("traveler-couples")]
            Couple,
            [Description("traveler-families")]
            Family,
            [Description("traveler-group")]
            Group,
            [Description("traveler-business")]
            Bussiness,
            [Description("occupancyRooms")]
            Room,
            [Description("occupancyAdults")]
            Adults,
            [Description("occupancyChildren")]
            Children
        }

        public enum RoomProperties
        {
            [Description("breakfast-include")]
            FreeBreakfast,
            [Description("free-cancellation")]
            FreeCancellation,
            [Description("non-smoking")]
            NonSmoking,
            [Description("twin-bed")]
            TwinBed
        }

        public enum Payment
        {
            //Textbox
            [Description("email")]
            Email,
            [Description("reEmail")]
            RetypeEmail,
            [Description("phoneNumber")]
            MobileNumber,
            [Description("promotionCode")]
            PromotionCode,
            //Combobox
            [Description("countryOfResidence")]
            ResidenceCountry,
            [Description("guestCountryOfResidence")]
            GuestResidenceCountry,
            [Description("arrivalTime")]
            ArrivalTime,
            //Radio Button
            [Description("non-smoking-radio")]
            NonSmokingRoom,
            [Description("smoking-radio")]
            SmokingRoom,
            [Description("king-bed-radio")]
            LargeBed,
            [Description("twin-bed-radio")]
            TwinBeds
        }
    }
}
