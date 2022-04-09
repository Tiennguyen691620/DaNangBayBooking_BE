using DaNangBayBooking.Application.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Common;
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
        private readonly IAccommodationTypeService _accommodationTypeService;

        public AccommodationController(
            IAccommodationTypeService accommodationTypeService

            )
        {
            _accommodationTypeService = accommodationTypeService;
        }

        /// <summary>
        /// Lấy tất cả loại CSLT
        /// </summary>
        [HttpGet("accommodationType/get-all")]
        public async Task<ActionResult<ApiResult<List<AccommodationTypeVm>>>> GetAll()
        {
            var accommodationTypes = await _accommodationTypeService.GetAll();
            return Ok(accommodationTypes);
        }
    }
}
