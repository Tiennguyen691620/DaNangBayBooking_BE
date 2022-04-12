using DaNangBayBooking.Application.Catalog.Accommodations;
using DaNangBayBooking.Application.Catalog.AccommodationTypes;
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
        private readonly IAccommodationTypeService _accommodationTypeService;

        public AccommodationController(
            IAccommodationService accommodationService,
            IAccommodationTypeService iAccommodationTypeService

            )
        {
            _accommodationService = accommodationService;
            _accommodationTypeService = iAccommodationTypeService;
        }

        /// <summary>
        /// Lấy danh sách CSLT phân trang 
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
        /// Lấy thông tin chi tiết CSLT
        /// </summary>
        /// 
        [HttpGet("get-by-id/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AccommodationVm>> GetById(Guid id)
        {
            var Accommodation = await _accommodationService.GetById(id);
            return Ok(Accommodation);
        }

        /// <summary>
        /// Tạo mới CSLT
        /// </summary>
        /// 
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> Post(AccommodationCreateRequest request)
        {
            var Accommodation = await _accommodationService.CreateAccommodation(request);
            return Ok(Accommodation);
        }

        /// <summary>
        /// Lấy danh sách tất cả loại CSLT
        /// </summary>
        /// 
        [HttpGet("get-all/accommodationType")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<List<AccommodationTypeVm>>>> GetAllAccommodationType()
        {
            var AccommodationType = await _accommodationTypeService.GetAll();
            return Ok(AccommodationType);
        }

        /// <summary>
        /// Tạo mới loại CSLT
        /// </summary>
        /// 
        [HttpPost("create/accommodationType")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateAccommodationType(AccommodationTypeCreateRequest request)
        {
            var AccommodationType = await _accommodationTypeService.Create(request);
            return Ok(AccommodationType);
        }
    }
}
