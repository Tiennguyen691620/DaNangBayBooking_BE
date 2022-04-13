using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Rooms
{
    public class CreateRoomRequest
    {
        public Guid RoomTypeID { get; set; }

        public string Name { get; set; }

        public int AvailableQty { get; set; }

        public int MaximumPeople { get; set; }

        public Decimal Price { get; set; }

        public List<ImageAccommodationCreateRequest> Image { get; set; }
    }
}
