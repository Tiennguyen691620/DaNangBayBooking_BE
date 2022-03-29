using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Enums;

namespace DaNangBayBooking.Data.Entities
{
    public class BookRoomDetail
    {
        public Guid BookRoomDetailID { get; set; }

        public Guid RoomID { get; set; }

        public Guid BookRoomID { get; set; }

        public int ChildNumber { get; set; }

        public DateTime CancelDate { get; set; }

        public string CancelReason { get; set; }

        public int PersonNumber { get; set; }

        public Status Status { get; set; }

        public BookRoom BookRoom { get; set; }

        public Room Room { get; set; }

    }
}
