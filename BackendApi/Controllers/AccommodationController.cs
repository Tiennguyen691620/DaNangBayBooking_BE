using DaNangBayBooking.Application.Catalog.Accommodations;
using DaNangBayBooking.Application.Catalog.AccommodationTypes;
using DaNangBayBooking.Application.Catalog.Rooms;
using DaNangBayBooking.Application.Catalog.Utilities;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Catalog.Utilities;
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
        private readonly IRoomService _roomService;
        private readonly IUtilityService _utilityService;

        public AccommodationController(
            IAccommodationService accommodationService,
            IAccommodationTypeService iAccommodationTypeService,
            IRoomService roomService,
            IUtilityService iutilityService

            )
        {
            _accommodationService = accommodationService;
            _accommodationTypeService = iAccommodationTypeService;
            _roomService = roomService;
            _utilityService = iutilityService;
        }

        /// <summary>
        /// Lấy danh sách CSLT phân trang 
        /// </summary>
        /// 
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<AccommodationVm>>> GetAllPaging([FromQuery] GetAccommodationPagingRequest request)
        {
            var Accommodation = await _accommodationService.GetAccommodationsAllPaging(request);
            return Ok(Accommodation);
        }
        /// <summary>
        /// Lấy thông tin chi tiết CSLT
        /// </summary>
        /// 
        [HttpGet("detail/{id}")]
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
        /// Cập nhật CSLT
        /// </summary>
        /// 
        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateAccommodation(AccommodationUpdateRequest request)
        {
            var Accommodation = await _accommodationService.UpdateAccommodation(request);
            return Ok(Accommodation);
        }

        /// <summary>
        /// Xóa CSLT
        /// </summary>
        /// 
        [HttpDelete("delete")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> DeleteAccommodation([FromBody] AccommodationDeleteRequest request)
        {
            var Accommodation = await _accommodationService.DeleteAccommodation(request);
            return Ok(Accommodation);
        }


        /// <summary>
        /// Cập nhật trạng thái của CSLT
        /// </summary>
        /// 
        [HttpPut("update/{AccommodationID}/status")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateStatusAccommodation(Guid AccommodationID, bool Status)
        {
            var Accommodation = await _accommodationService.UpdateStatusAccommodation(AccommodationID, Status);
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

        /// <summary>
        /// Xem chi tiết phòng của CSLT
        /// </summary>
        /// 
        [HttpGet("get/room/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<RoomVm>>> GetRoom(Guid accommodationID)
        {
            var Room = await _roomService.GetRoomDetail(accommodationID);
            return Ok(Room);
        }

        /// <summary>
        /// Tạo mới phòng của CSLT
        /// </summary>
        /// 
        [HttpPost("create/room/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateRoom(Guid accommodationID, CreateRoomRequest request)
        {
            var Room = await _roomService.CreateRoom(accommodationID, request);
            return Ok(Room);
        }

        /// <summary>
        /// Cập nhật phòng của CSLT
        /// </summary>
        /// 
        [HttpPut("update/room/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateRoom(Guid accommodationID, List<UpdateRoomRequest> request)
        {
            var Utilities = await _roomService.UpdateRoom(accommodationID, request);
            return Ok(Utilities);
        }


        /// <summary>
        /// Xem chi tiết tiện ích của CSLT
        /// </summary>
        /// 
        [HttpGet("get/utility/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<List<UtilityVm>>>> GetDetailUtility(Guid accommodationID)
        {
            var Utilities = await _utilityService.GetDetailUtility(accommodationID);
            return Ok(Utilities);
        }
        
        /// <summary>
        /// Tạo mới tiện ích của CSLT
        /// </summary>
        /// 
        [HttpPost("create/utility/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> CreateUtility(List<CreateUtilityRequest> request, Guid accommodationID)
        {
            var Utilities = await _utilityService.CreateUtility(request, accommodationID);
            return Ok(Utilities);
        }

        /// <summary>
        /// Cập nhật tiện ích của CSLT
        /// </summary>
        /// 
        [HttpPut("update/utility/{accommodationID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<bool>>> UpdateUtility(List<UpdateUtilityRequest> request, Guid accommodationID)
        {
            var Utilities = await _utilityService.UpdateUtility(request, accommodationID);
            return Ok(Utilities);
        }

    }
}
