using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class AccommodationType
    {
        public Guid AccommodationTypeID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public string Description { get; set; }

        public List<Accommodation> Accommodations { get; set; }
    }
}
