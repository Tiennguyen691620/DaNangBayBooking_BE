using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Districts
{
    public class DistrictVm
    {
        public Guid DistrictID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public int SortOrder { get; set; }
    }
}
