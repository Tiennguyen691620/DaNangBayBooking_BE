using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.RoomTypes
{
    public interface IRoomTypeService
    {
        Task<ApiResult<List<RoomTypeVm>>> GetAll();
        Task<ApiResult<RoomType>> Create( RoomTypeRequest request );
        Task<ApiResult<PagedResult<RoomTypeVm>>> GetAllPaging( GetRoomTypePagingRequest request );
        Task<ApiResult<bool>> Delete(RoomTypeDeleteRequest request);
        Task<ApiResult<bool>> Update(RoomTypeUpdateRequest request);
        Task<ApiResult<RoomTypeVm>> GetByID(Guid RoomTypeID);
        Task<ApiResult<bool>> UpdateStatusRoomType(Guid RoomTypeId, bool Status);

    }
}
