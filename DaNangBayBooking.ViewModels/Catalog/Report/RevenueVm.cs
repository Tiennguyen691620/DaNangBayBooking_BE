using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Report
{
    public class RevenueVm
    {
        public List<RevenueReprortViewByBookRoom> ViewByAccommodation { get; set; }

        public List<RevenueReportViewByDate> ViewByDate { get; set; }
    }

    public class RevenueReprortViewByBookRoom
    {
        public Guid? ObjectId { get; set; }

        public string Name { get; set; }

        public decimal? Amount { get; set; }

        public long? Qty { get; set; }
    }

    public class RevenueReportViewByDate
    {
        public long? Date { get; set; }

        public decimal? Amount { get; set; }

        public  List<RevenueReprortViewByBookRoom> Childs { get; set; }
    }
}
