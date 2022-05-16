using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class AccommodationAvailable
    {
        public Guid Id { get; set; }

        public string RoomName { get; set; }

        public string RoomTypeName { get; set; }

        public long?  Date { get; set; }

        public DateTime DateAvailable { get; set; }

        public long Qty { get; set; }
    }
}
