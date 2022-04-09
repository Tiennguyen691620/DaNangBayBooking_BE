using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class Location
    {
        public Guid LocationID { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? ParentID { get; set; }
        public int SortOrder { get; set; }
        public string Code { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<AppUser> AppUsers { get; set; }
    }
}
