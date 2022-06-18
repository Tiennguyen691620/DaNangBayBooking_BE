﻿using DaNangBayBooking.Data.Enums;
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
        Task<ApiResult<PagedResult<AccommodationVm>>> GetAccommodationsAllPagingClient(GetAccommodationPagingRequest request);
        Task<ApiResult<List<AccommodationVm>>> GetAllAccommodation();
        Task<ApiResult<AccommodationVm>> GetById(Guid id);
        Task<ApiResult<AccommodationVm>> GetByIdClient(Guid id);
        Task<ApiResult<bool>> CreateAccommodation(AccommodationCreateRequest request);
        Task<ApiResult<bool>> UpdateAccommodation(AccommodationUpdateRequest request);
        Task<ApiResult<bool>> DeleteAccommodation(AccommodationDeleteRequest request);
        Task<ApiResult<bool>> UpdateStatusAccommodation(Guid AccommodationID, bool Status);
        Task<ApiResult<ImageVm>> AddImage(ImageCreateRequest request);
        Task<ApiResult<bool>> DeleteImage(ImageAccommodationDeleteRequest request);
        Task<ApiResult<List<AccommodationAvailable>>> GetByIdAvailable(Guid AccommodationId, GetAccommodationAvailableRequest request);

    }
}
