using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.BookRoomDetail;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class BookRoomVm
    {
        public Guid BookRoomID { get; set; }

        public string No { get; set; }

        public int Qty { get; set; }

        public long? BookingDate { get; set; }

        public long? FromDate { get; set; }

        public long? ToDate { get; set; }

        public int TotalDay { get; set; }

        public string CheckInName { get; set; }

        public string CheckInMail { get; set; }

        public string CheckInNote { get; set; }

        public string CheckInIdentityCard { get; set; }

        public string CheckInPhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public AccommodationVm Accommodation { get; set; }

        //public RoomVm Room { get; set; }

        public BookRoomDetailVm BookRoomDetail { get; set; }

        public BookingStatus Status { get; set; }
    }
}
