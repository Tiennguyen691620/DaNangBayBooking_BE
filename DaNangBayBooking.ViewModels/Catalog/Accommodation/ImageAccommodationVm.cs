using System;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class ImageAccommodationVm
    {
        public Guid ImageAccommodationID { get; set; }

        public Guid AccommodationID { get; set; }

        public string Image { get; set; }

        public int SortOrder { get; set; }
    }
}