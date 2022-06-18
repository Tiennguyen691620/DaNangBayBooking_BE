using DaNangBayBooking.Application.Catalog.RoomTypes;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
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
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(
            IRoomTypeService roomTypeService

            )
        {
            _roomTypeService = roomTypeService;
        }

        /// <summary>
        /// Lấy tất cả loại phòng 
        /// </summary>
        [HttpGet("get-all")]
        public async Task<ActionResult<ApiResult<List<RoomTypeVm>>>> GetAllRoomType()
        {
            var roomTypes = await _roomTypeService.GetAll();
            return Ok(roomTypes);
        }

        /// <summary>
        /// Lấy loại phòng theo ID
        /// </summary>
        [HttpGet("get/{roomTypeID}")]
        public async Task<ActionResult<ApiResult<RoomTypeVm>>> GetByID(Guid roomTypeID)
        {
            var roomTypes = await _roomTypeService.GetByID(roomTypeID);
            return Ok(roomTypes);
        }

        /// <summary>
        /// Tạo mới loại phòng
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<bool>>> Post(RoomTypeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roomTypeService.Create(request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách loại phòng phân trang và tìm kiếm
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<RoomTypeVm>>> GetAllPaging([FromQuery] GetRoomTypePagingRequest request)
        {
            var roomTypes = await _roomTypeService.GetAllPaging(request);
            return Ok(roomTypes);
        }

        /// <summary>
        /// Xóa và vô hiệu hóa loại phòng
        /// </summary>
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResult<bool>>> Delete([FromBody] RoomTypeDeleteRequest request)
        {
            var roomTypes = await _roomTypeService.Delete(request);
            return Ok(roomTypes);
        }

        /// <summary>
        /// Cập nhật loại phòng
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResult<bool>>> Update([FromBody] RoomTypeUpdateRequest request)
        {
            var roomTypes = await _roomTypeService.Update(request);
            return Ok(roomTypes);
        }

        /// <summary>
        /// Cập nhật trạng thái của loại phòng
        /// </summary>
        /// 
        [HttpPut("update/{RoomTypeId}/status")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateStatusRoomType(Guid RoomTypeId, bool Status)
        {
            var RoomType = await _roomTypeService.UpdateStatusRoomType(RoomTypeId, Status);
            return Ok(RoomType);
        }
    }
}
