using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class ImageRoom
    {
        public Guid ImageRoomID { get; set; }

        public Guid RoomID { get; set; }

        public string Image { get; set; }

        public int SortOrder { get; set; }

        public Room Room { get; set; }
    }
}
