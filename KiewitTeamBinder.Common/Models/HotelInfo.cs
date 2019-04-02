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
        private string roomName;
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
            this.hotelName = hotelName;
            this.roomName = roomName;
            this.roomQuantity = roomQuantity;
            this.roomPrice = roomPrice;
        }

        public HotelInfo() { }

    }
}
