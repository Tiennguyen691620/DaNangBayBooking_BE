using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRooms
{
    public class FilterBookRoomReportRequest : PagingRequestBase
    {
        public Guid? AccommodationId { get; set; }

        public long? BookingFromDate { get; set; }

        public long? BookingToDate { get; set; }

        public long? CheckInFromDate { get; set; }

        public long? CheckInToDate { get; set; }

        public long? Status { get; set; }
    }
}
