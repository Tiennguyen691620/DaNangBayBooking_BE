using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class CancelBookingRequest
    {
        public Guid Id { get; set; }

        public string CancelReason { get; set; }
    }
}
