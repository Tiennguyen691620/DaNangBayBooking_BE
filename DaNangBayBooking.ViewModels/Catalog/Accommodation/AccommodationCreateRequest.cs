using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Catalog.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class AccommodationCreateRequest
    {
        public Guid AccommodationID { get; set; }

        public Guid LocationID { get; set; }

        public Guid AccommodationTypeID { get; set; }

        public string Name { get; set; }

        public string AbbreviationName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public string MapURL { get; set; }

        public string No { get; set; }

        public Status Status { get; set; }

        public LocationVm Location { get; set; }

        public AccommodationTypeVm AccommodationType { get; set; }

        public List<ImageAccommodationVm> ImageAccommodations { get; set; }

    }
}
