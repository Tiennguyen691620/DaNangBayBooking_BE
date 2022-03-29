using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class ImageAccommodation
    {
        public Guid ImageAccommodationID { get; set; }

        public Guid AccommodationID { get; set; }

        public string Image { get; set; }

        public int SortOrder { get; set; }

        public Accommodation Accommodation { get; set; }
    }
}
