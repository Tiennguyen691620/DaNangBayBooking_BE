using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.RoomTypes
{
    public class RoomTypeVm
    {
        public Guid RoomTypeID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string No { get; set; }

        public bool Status { get; set; }
    }
}
