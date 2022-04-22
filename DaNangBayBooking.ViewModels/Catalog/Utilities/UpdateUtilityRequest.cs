using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Utilities
{
    public class UpdateUtilityRequest
    {
        public Guid UtilityID { get; set; }

        //public Guid AccommodationID { get; set; }

        public string UtilityType { get; set; }

        public Boolean IsPrivate { get; set; }
    }
}
