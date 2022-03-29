using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class RoomType
    {
        public Guid RoomTypeID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string No { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
