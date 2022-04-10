using DaNangBayBooking.Application.Catalog.Accommodations;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
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
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationService _accommodationService;

        public AccommodationController(
            IAccommodationService accommodationService

            )
        {
            _accommodationService = accommodationService;
        }

        /// <summary>
        /// Lấy danh sách  tài khoản phân trang 
        /// </summary>
        /// 
        [HttpGet("get-all-paging")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<List<AccommodationVm>>>> GetAllPaging([FromQuery] GetAccommodationPagingRequest request)
        {
            var Accommodation = await _accommodationService.GetAccommodationsAllPaging(request);
            return Ok(Accommodation);
        }
        /// <summary>
        /// Lấy thông tin chi tiết tài khoản
        /// </summary>
        /// 
        [HttpGet("get-by-id/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AccommodationVm>> GetById(Guid id)
        {
            var Accommodation = await _accommodationService.GetById(id);
            return Ok(Accommodation);
        }
    }
}
