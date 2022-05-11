using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Rooms
{
    public class RoomRequest
    {
        public Guid RoomID { get; set; }

        public RoomTypeVm RoomType { get; set; }

        public string Name { get; set; }

        public int AvailableQty { get; set; }

        public int PurchasedQty { get; set; }

        public int MaximumPeople { get; set; }

        public Decimal Price { get; set; }

        public int BookedQty { get; set; }

        public string Description { get; set; }

        public string No { get; set; }

        public string Image { get; set; }
    }
}
