using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Report
{
    public class FilterRevenueRequest
    {
        public long? FromDate { get; set; }

        public long? ToDate { get; set; }

        public Guid AccommodationId { get; set; }
    }
}
