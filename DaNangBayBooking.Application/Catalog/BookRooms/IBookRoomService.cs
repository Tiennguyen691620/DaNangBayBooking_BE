using DaNangBayBooking.ViewModels.Catalog.BookRooms;
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

        Task<ApiResult<PagedResult<BookRoomVm>>> FilterBookingClient(FilterBookRoomRequest request);

        Task<ApiResult<PagedResult<BookRoomVm>>> FilterBooking(FilterBookRoomRequest request);

        Task<ApiResult<bool>> CancelBooking(CancelBookingRequest request);

        Task<ApiResult<BookRoomVm>> GetById(Guid id);

        Task<ApiResult<List<BookRoomVm>>> ReportBooking(FilterBookRoomReportRequest request);

        Task<ApiResult<bool>> CancelBookingByAccommodation(CancelBookingRequest request);

        Task<ApiResult<bool>> SuccessBookingByAccommodation(BookRoomVm request);


    }
}
