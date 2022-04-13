using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Rooms
{
    public interface IRoomService
    {
        Task<ApiResult<List<RoomVm>>> GetRoomDetail(Guid AccommodationID);
        Task<ApiResult<bool>> CreateRoom(Guid AccommodationID, CreateRoomRequest request);
    }
}
