using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Agoda
{
    public class CustomerInformation
    {
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RetypeEmail { get; set; }
        public string MobileNumber { get; set; }
        public string Country { get; set; }
        public bool IsBookForSomeoneElse { get; set; }
        public string GuestFullName { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestCountry { get; set; }
        public bool IsArrangeTransportation { get; set; }
        public bool NonSmokingRoom { get; set; }
        public bool IsLargeBedOrTwinBeds { get; set; }
        public string ArrivalTime { get; set; }
    }
}
