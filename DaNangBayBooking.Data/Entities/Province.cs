using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
     public class Province
    {
        public Guid ProvinceID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public int SortOrder { get; set; }

        public List<District> Districts { get; set; }
    }
}
