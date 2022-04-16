using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Images;
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
        Task<ApiResult<bool>> CreateAccommodation(AccommodationCreateRequest request);
        Task<ApiResult<bool>> UpdateAccommodation(AccommodationUpdateRequest request);
        Task<ApiResult<bool>> DeleteAccommodation(AccommodationDeleteRequest request);
        Task<ApiResult<ImageVm>> AddImage(ImageCreateRequest request);
    }
}
