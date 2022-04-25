using DaNangBayBooking.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class BookRoomVm
    {
        public Guid BookRoomID { get; set; }

        public Guid AccommodationID { get; set; }

        public Guid UserID { get; set; }

        public string No { get; set; }

        public int Qty { get; set; }

        public DateTime BookingDate { get; set; }

        public string BookingUser { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CheckInName { get; set; }

        public string CheckInMail { get; set; }

        public string CheckInNote { get; set; }

        public string CheckInIdentityCard { get; set; }

        public Decimal TotalPrice { get; set; }

        public StatusEnum Status { get; set; }
    }
}
