using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.RoomTypes
{
    public class GetRoomTypePagingRequest : PagingRequestBase
    {
        public string SearchKey { get; set; }
    }
}
