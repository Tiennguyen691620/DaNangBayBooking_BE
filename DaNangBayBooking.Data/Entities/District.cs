using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DaNangBayBooking.Data.Entities
{
    public class District
    {
        public Guid DistrictID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public int SortOrder { get; set; }

        public List <Ward> Wards { get; set; }
    }
}
