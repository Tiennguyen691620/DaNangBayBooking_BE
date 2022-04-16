using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Enums;

namespace DaNangBayBooking.Data.Entities
{
    public class Accommodation
    {
        public Guid AccommodationID { get; set; }

        public Guid LocationID { get; set; }

        public Guid AccommodationTypeID { get; set; }

        public string Name { get; set; }

        public string AbbreviationName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MapURL { get; set; }

        public string No { get; set; }

        public Status Status { get; set; }

        public Location Location { get; set; }

        public AccommodationType AccommodationType { get; set; }

        public List<Utility> Utilities { get; set; }

        public List<ImageAccommodation> imageAccommodations { get; set; }

        public List<Room> Rooms { get; set; }

        public List<BookRoom> BookRooms { get; set; }

    }
}
