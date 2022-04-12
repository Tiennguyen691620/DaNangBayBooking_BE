using DaNangBayBooking.Application.Catalog.Wards;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaNangBayBooking.BackendApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MasterDataController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public MasterDataController(
            ILocationService locationService

            )
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Lấy phường/xã theo id
        /// </summary>
        /// 
        [HttpGet("location/get-by-id/{Id}")]
        public async Task<ActionResult<ApiResult<LocationVm>>> GetById(Guid Id)
        {
            var result = await _locationService.GetById(Id);
            if (result == null)
                return BadRequest("Cannot find location");
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả danh sách phường/xã theo quận/huyện
        /// </summary>
        /// 
        [HttpGet("location/get-all${district}/subDistrict")]
        public async Task<ActionResult<ApiResult<List<LocationVm>>>> GetAllSubDistrict(Guid district)
        {
            var result = await _locationService.GetAllSubDistrict(district);
            return Ok(result);
        }
        /// <summary>
        /// Lấy tất cả danh sách quận/huyện theo tỉnh/TP
        /// </summary>
        /// 
        [HttpGet("location/get-all/${province}/district")]
        public async Task<ActionResult<ApiResult<List<LocationVm>>>> GetAllDictrict(Guid province)
        {
            var result = await _locationService.GetAllDistrict(province);
            return Ok(result);
        }
        /// <summary>
        /// Lấy tất cả danh sách tỉnh/thành phố
        /// </summary>
        /// 
        [HttpGet("location/get-all/province")]
        public async Task<ActionResult<ApiResult<List<LocationVm>>>> GetAllProvince()
        {
            var result = await _locationService.GetAllProvince();
            return Ok(result);
        }
    }
}
