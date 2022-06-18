using DaNangBayBooking.Application.Catalog.Reports;
using DaNangBayBooking.ViewModels.Catalog.Report;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaNangBayBooking.BackendApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(
            IReportService reportService
            )
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Báo cáo thống kê doanh thu của CSLT
        /// </summary>
        /// 
        [HttpGet("filter/revenue")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<RevenueVm>>> ReportRevenue([FromQuery] FilterRevenueRequest request)
        {
            var revenueReport = await _reportService.ReportRevenue(request);
            return Ok(revenueReport);
        }
    }
}
