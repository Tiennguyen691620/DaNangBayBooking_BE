using DaNangBayBooking.Application.Catalog.RateComments;
using DaNangBayBooking.ViewModels.Catalog.RateComment;
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
    public class RateCommentController : ControllerBase
    {
        private readonly IRateCommentService _rateCommentService;

        public RateCommentController(
            IRateCommentService rateCommentService

            )
        {
            _rateCommentService = rateCommentService;
        }

        /// <summary>
        /// Tạo mới đánh giá
        /// </summary>
        /// 
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateRateComment(CreateRateCommentRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _rateCommentService.CreateRateComment(request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả đánh giá
        /// </summary>
        /// 
        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<List<RateCommentVm>>>> GetAllRateComment([FromQuery] GetAllRateCommentRequest request)
        {
            var Accommodation = await _rateCommentService.GetAllRateComment(request);
            return Ok(Accommodation);
        }

        /// <summary>
        /// Lấy tất điểm và số lượng đánh giá
        /// </summary>
        /// 
        [HttpGet("get-all/point")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<GetQtyRateComment>>> GetQtyAndPontRateComment([FromQuery] Guid accommodationId)
        {
            var Accommodation = await _rateCommentService.GetQtyAndPontRateComment(accommodationId);
            return Ok(Accommodation);
        }
    }
}
