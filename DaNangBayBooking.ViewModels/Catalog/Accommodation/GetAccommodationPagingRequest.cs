using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Accommodation
{
    public class GetAccommodationPagingRequest : PagingRequestBase
    {
        public string SearchKey { get; set; }

        public Guid? AccommodationTypeID { get; set; }

        public Guid? ProvinceID { get; set; }

        public Guid? DistrictID { get; set; }

        public Guid? SubDistrictID { get; set; }


    }
}
