using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Locations
{
    public class LocationDistrict
    {
        public Guid LocationID { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? ParentID { get; set; }
        public int SortOrder { get; set; }
        public string Code { get; set; }
    }
}
