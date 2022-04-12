using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.AccommodationTypes
{
    public interface IAccommodationTypeService
    {
        Task<ApiResult<List<AccommodationTypeVm>>> GetAll();
        Task<ApiResult<bool>> Create(AccommodationTypeCreateRequest request);
    }
}
