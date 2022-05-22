using DaNangBayBooking.ViewModels.Catalog.Report;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Reports
{
    public interface IReportService
    {
        Task<ApiResult<RevenueVm>> ReportRevenue(FilterRevenueRequest request);

    }
}
