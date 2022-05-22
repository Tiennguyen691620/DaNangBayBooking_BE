using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRoomDetail
{
    public class BookRoomDetailVm
    {
        public Guid BookRoomDetailID { get; set; }

        public int ChildNumber { get; set; }

        public DateTime? CancelDate { get; set; }

        public string? CancelReason { get; set; }

        public int PersonNumber { get; set; }

        public BookingStatus Status { get; set; }

        public BookRoomVm BookRoom { get; set; }

        public RoomVm Room { get; set; }
    }
}
