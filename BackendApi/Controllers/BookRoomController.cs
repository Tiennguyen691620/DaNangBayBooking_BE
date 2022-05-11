using DaNangBayBooking.Application.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.BookRoom;
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
        [HttpPost("create/book-room")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateBookingRoom(BookRoomCreateRequest request)
        {
            var Accommodation = await _bookRoomService.CreateBookingRoom(request);
            return Ok(Accommodation);
        }
    }
}
