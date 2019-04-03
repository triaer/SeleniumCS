using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models
{
    public class HotelInfo
    {
        private string hotelName;
        private string actualHotelName;
        private string roomName;
        private string actualRoomName;
        private int roomQuantity;
        private double roomPrice;

        public string HotelName
        {
            get
            {
                return hotelName;
            }

            set
            {
                hotelName = value;
            }
        }

        public string ActualHotelName
        {
            get
            {
                return actualHotelName;
            }

            set
            {
                actualHotelName = value;
            }
        }

        public string RoomName
        {
            get
            {
                return roomName;
            }

            set
            {
                roomName = value;
            }
        }

        public string ActualRoomName
        {
            get
            {
                return actualRoomName;
            }

            set
            {
                actualRoomName = value;
            }
        }

        public int RoomQuantity
        {
            get
            {
                return roomQuantity;
            }

            set
            {
                roomQuantity = value;
            }
        }

        public double RoomPrice
        {
            get
            {
                return roomPrice;
            }

            set
            {
                roomPrice = value;
            }
        }

        public HotelInfo(string hotelName, string roomName, int roomQuantity, double roomPrice)
        {
            this.HotelName = hotelName;
            this.RoomName = roomName;
            this.RoomQuantity = roomQuantity;
            this.RoomPrice = roomPrice;
            this.actualHotelName = hotelName;
            this.actualRoomName = roomName;
        }

        public HotelInfo() { }

    }
}
