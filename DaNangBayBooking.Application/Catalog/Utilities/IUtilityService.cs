using DaNangBayBooking.ViewModels.Catalog.Utilities;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Utilities
{
    public interface IUtilityService
    {
        Task<ApiResult<List<UtilityVm>>> GetDetailUtility(Guid AccommodationID);
        Task<ApiResult<bool>> CreateUtility(List<CreateUtilityRequest> request, Guid AccommodationID);
        Task<ApiResult<bool>> UpdateUtility(List<UpdateUtilityRequest> request, Guid AccommodationID);
    }
}
