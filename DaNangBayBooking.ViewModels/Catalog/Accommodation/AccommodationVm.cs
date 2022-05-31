using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Catalog.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class AccommodationVm
    {
        public Guid AccommodationID { get; set; }

        public string Name { get; set; }

        public string AbbreviationName { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MapURL { get; set; }

        public string No { get; set; }

        public bool Status { get; set; }

        public LocationProvince Province { get; set; }

        public LocationDistrict District { get; set; }

        public LocationSubDistrict SubDistrict { get; set; }

        public AccommodationTypeVm AccommodationType { get; set; }

        public List<ImageAccommodationVm> Images { get; set; }
        public int Qty { get; set; }
        public long Point { get; set; }
    }
}
