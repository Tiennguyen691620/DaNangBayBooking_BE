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
        Task<ApiResult<RoomType>> Post( RoomTypeRequest request );
        Task<ApiResult<PagedResult<RoomTypeVm>>> GetAllPaging( GetRoomTypePagingRequest request );


    }
}
