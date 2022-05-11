using DaNangBayBooking.ViewModels.Catalog.BookRoom;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.BookRooms
{
    public interface IBookRoomService
    {
        Task<ApiResult<bool>> CreateBookingRoom(BookRoomCreateRequest request);
    }
}
