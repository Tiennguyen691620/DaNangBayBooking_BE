using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Wards
{
    public class WardVm
    {
        public Guid WardID { get; set; }

        public string Name { get; set; }

        public string No { get; set; }

        public int SortOrder { get; set; }
    }
}
