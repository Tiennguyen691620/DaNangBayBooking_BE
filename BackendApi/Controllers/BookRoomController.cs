using DaNangBayBooking.Application.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
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
    public class BookRoomController : ControllerBase
    {

        private readonly IBookRoomService _bookRoomService;

        public BookRoomController(
            IBookRoomService bookRoomService
            )
        {
            _bookRoomService = bookRoomService;
        }


        /// <summary>
        /// Đặt phòng
        /// </summary>
        /// 
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateBookingRoom(BookRoomCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookRoomService.CreateBookingRoom(request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách đặt phòng phân trang 
        /// </summary>
        /// 
        [HttpGet("filter/client")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<BookRoomVm>>> FilterBookingClient([FromQuery] FilterBookRoomRequest request)
        {
            await _bookRoomService.CloseBooking(request);
            var bookRoom = await _bookRoomService.FilterBookingClient(request);
            return Ok(bookRoom);
        }

        /// <summary>
        /// Lấy danh sách đặt phòng phân trang 
        /// </summary>
        /// 
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<BookRoomVm>>> FilterBooking([FromQuery] FilterBookRoomRequest request)
        {
            var bookRoom = await _bookRoomService.FilterBooking(request);
            return Ok(bookRoom);
        }

        /// <summary>
        /// Hủy đặt phòng
        /// </summary>
        /// 
        [HttpPost("cancel")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CancelBooking(CancelBookingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookRoomService.CancelBooking(request);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Hủy đặt phòng bởi CSLT
        /// </summary>
        /// 
        [HttpGet("cancel/accommodation/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CancelBookingByAccommodation(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookRoomService.CancelBookingByAccommodation(id);

            if (result == null)
            {
                return BadRequest(result.Data);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Xác nhận đặt phòng bởi CSLT
        /// </summary>
        /// 
        [HttpGet("success/{bookRoomId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<string>>> SuccessBookingByAccommodation(Guid bookRoomId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookRoomService.SuccessBookingByAccommodation(bookRoomId);

            if (result == null)
            {
                return BadRequest(result.Data);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Lấy thông tin chi tiết đặt phòng
        /// </summary>
        /// 
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookRoomVm>> GetById(Guid id)
        {
            var bookRoom = await _bookRoomService.GetById(id);
            return Ok(bookRoom);
        }


        /// <summary>
        /// Lấy báo cáo danh sách hủy/đặt phòng
        /// </summary>
        /// 
        [HttpGet("filter/report")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<BookRoomVm>>> ReportBooking([FromQuery] FilterBookRoomReportRequest request)
        {
            var bookRoomReport = await _bookRoomService.ReportBooking(request);
            return Ok(bookRoomReport);
        }

    }
}
