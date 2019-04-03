using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.Common.Models
{
    public class BookingInfo
    {
        private string destination = String.Empty;
        private DateTime checkInDate;
        private int duration = 0;
        private TravelerType travelerType;
        private int room = 0;
        private int adults = 0;
        private int children = 0;

        public string Destination
        {
            get
            {
                return destination;
            }

            set
            {
                destination = value;
            }
        }

        public DateTime CheckInDate
        {
            get
            {
                return checkInDate;
            }

            set
            {
                checkInDate = value;
            }
        }

        public DateTime CheckOutDate
        {
            get
            {
                return checkInDate.AddDays(duration);
            }
        }

        public int Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public TravelerType TravelerType
        {
            get
            {
                return travelerType;
            }

            set
            {
                travelerType = value;
            }
        }

        public int Room
        {
            get
            {
                return room;
            }

            set
            {
                room = value;
            }
        }

        public int Adults
        {
            get
            {
                return adults;
            }

            set
            {
                adults = value;
            }
        }

        public int Children
        {
            get
            {
                return children;
            }

            set
            {
                children = value;
            }
        }

        public BookingInfo(string destination, DateTime checkInDate, int duration, TravelerType travelerType, int room, int adults, int children)
        {
            this.Destination = destination;
            this.CheckInDate = checkInDate;
            this.duration = duration;
            this.travelerType = travelerType;
            this.Room = room;
            this.Adults = adults;
            this.Children = children;
        }

        public BookingInfo() { }
    }
}
