using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Accommodations
{
    public interface IAccommodationService
    {
        Task<ApiResult<PagedResult<AccommodationVm>>> GetAccommodationsAllPaging(GetAccommodationPagingRequest request);
        Task<ApiResult<AccommodationVm>> GetById(Guid id);
        //Task<ApiResult<bool>> CreateAccommodation(AccommodationCreateRequest request);

    }
}
