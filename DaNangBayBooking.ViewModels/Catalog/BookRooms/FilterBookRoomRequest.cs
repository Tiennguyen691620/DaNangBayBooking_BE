using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class FilterBookRoomRequest : PagingRequestBase
    {
        public string SearchKey { get; set; }

        public long? BookingFromDate { get; set; }

        public long? BookingToDate { get; set; }

        public long? FromDate { get; set; }

        public long? ToDate { get; set; }

        public Guid? UserId { get; set; }

        public BookingStatus? Status { get; set; }
    }
}
