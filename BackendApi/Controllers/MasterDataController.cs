using DaNangBayBooking.Application.Catalog.Districts;
using DaNangBayBooking.Application.Catalog.Provinces;
using DaNangBayBooking.Application.Catalog.Wards;
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
        private readonly IProvinceService _provinceService;
        private readonly IDistrictService _districtService;
        private readonly IWardService _wardService;

        public MasterDataController(
            IProvinceService provinceService,
            IDistrictService districtService,
            IWardService wardService

            )
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
        }

        /// <summary>
        /// Lấy tất cả tỉnh thành
        /// </summary>
        [HttpGet("loacation/get-all/province")]
        public async Task<IActionResult> GetProvinceAll()
        {
            var provinces = await _provinceService.GetAll();
            return Ok(provinces);
        }

        /// <summary>
        /// Lấy tỉnh/thành phố theo ID tỉnh/thành phố
        /// </summary>
        [HttpGet("loacation/{provinceID}/province")]
        public async Task<IActionResult> GetById(Guid provinceID)
        {
            var provinces = await _provinceService.GetById(provinceID);
            return Ok(provinces);
        }

        ///<summary>
        ///Lấy quận/huyện theo ID tỉnh/thành phố
        ///</summary>
        [HttpGet("loacation/{provinceID}/district")]
        public async Task<IActionResult> GetDistrictAll(Guid provinceID)
        {
            var districts = await _districtService.GetAll(provinceID);
            return Ok(districts);
        }

        /// <summary>
        /// Lấy phường/xã theo ID quận/huyện
        /// </summary>
        [HttpGet("loacation/{districtID}/ward")]
        public async Task<IActionResult> GetWardAll(Guid districtID)
        {
            var wards = await _wardService.GetAll(districtID);
            return Ok(wards);
        }
    }
}
