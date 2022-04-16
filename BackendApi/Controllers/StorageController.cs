using DaNangBayBooking.Application.Catalog.Accommodations;
using DaNangBayBooking.ViewModels.Catalog.Images;
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
    public class StorageController : ControllerBase
    {
        private readonly IAccommodationService _iAcommodationService;
        public StorageController(IAccommodationService iAccommodationService)
        {
            _iAcommodationService = iAccommodationService;
        }
        /// <summary>
        /// Thêm ảnh
        /// </summary>
        /// 
        [HttpPost("images/upload/")]
        public async Task<ActionResult<ApiResult<ImageVm>>> Create([FromForm] ImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _iAcommodationService.AddImage(request);
            if (!result.IsSuccessed)
                return BadRequest();

            return Ok(result);
        }
    }
}
