using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class BookRoomCreateRequest
    {
        //public Guid BookRoomID { get; set; }

        public string No { get; set; }

        public int Qty { get; set; }

        public DateTime BookingDate { get; set; }

        public long FromDate { get; set; }

        public long ToDate { get; set; }

        public int TotalDay { get; set; }

        public string CheckInName { get; set; }

        public string CheckInMail { get; set; }

        public string CheckInNote { get; set; }

        public string CheckInIdentityCard { get; set; }

        public string CheckInPhoneNumber { get; set; }

        public string bookingUser { get; set; }

        public decimal TotalPrice { get; set; }

        public int ChildNumber { get; set; }

        public int PersonNumber { get; set; }

        public AccommodationVm Accommodation { get; set; }

        public RoomVm Room { get; set; }

        public BookingStatus Status { get; set; }

        public Guid UserId { get; set; }
    }
}
