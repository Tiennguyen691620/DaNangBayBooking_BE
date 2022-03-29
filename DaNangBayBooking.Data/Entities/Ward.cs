using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class Ward
    {
        public Guid WardID { get; set; }

        public Guid DistrictID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public int SortOrder { get; set; }

        public District District { get; set; }

        public List<Accommodation> Accommodations { get; set; }
    }
}
