using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.RoomTypes
{
    public class RoomTypeUpdateRequest
    {
        public Guid RoomTypeID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
