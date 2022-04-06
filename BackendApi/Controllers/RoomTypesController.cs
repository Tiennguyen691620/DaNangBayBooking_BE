using DaNangBayBooking.Application.Catalog.RoomTypes;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
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
        public async Task<IActionResult> GetAllRoomType()
        {
            var roomTypes = await _roomTypeService.GetAll();
            return Ok(roomTypes);
        }

        /// <summary>
        /// Tạo mới loại phòng
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Post(RoomTypeRequest request)
        {
            var roomTypes = await _roomTypeService.Post(request);
            return Ok(roomTypes);
        }

        /// <summary>
        /// Lấy danh sách loại phòng phân trang và tìm kiếm
        /// </summary>
        [HttpGet("filter/")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetRoomTypePagingRequest request)
        {
            var roomTypes = await _roomTypeService.GetAllPaging(request);
            return Ok(roomTypes);
        }
    }
}
