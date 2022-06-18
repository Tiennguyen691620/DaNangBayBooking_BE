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

        public int MaximumPeople { get; set; }

        public decimal Price { get; set; }

        public long? DateAvailable  { get; set; }

        public DateTime Date { get; set; }

        public long Qty { get; set; }
    }
}
