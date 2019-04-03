using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Agoda
{
    public class PlaceToStay
    {
        public string Destination { get; set; }
        public int CheckinDate { get; set; }
        public string CheckinTime { get; set; }
        public int Duration { get; set; }
        public string TravelType { get; set; }
        public int Room { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Currency { get; set; }
    }
}
