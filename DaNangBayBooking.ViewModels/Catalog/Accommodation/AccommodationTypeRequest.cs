using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class AccommodationTypeRequest
    {
        public Guid AccommodationTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string No { get; set; }
    }
}
