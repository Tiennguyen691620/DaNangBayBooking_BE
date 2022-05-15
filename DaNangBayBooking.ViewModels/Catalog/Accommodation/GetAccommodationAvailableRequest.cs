using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class GetAccommodationAvailableRequest
    {

        public Guid? RoomId { get; set; }

        public long FromDate { get; set; }

        public long ToDate { get; set; }
    }
}
