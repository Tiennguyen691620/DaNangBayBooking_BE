using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class Utility
    {
        public Guid UtilityID { get; set; }

        public Guid AccommodationID { get; set; }

        public string UtilityType { get; set; }

        public Boolean IsPrivate { get; set; }

        public Accommodation Accommodation { get; set; }
    }
}
