using DaNangBayBooking.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class AccommodationUpdateStatusRequest
    {
        public bool Status { get; set; }

        public Guid accommodationID { get; set; }
    }
}
