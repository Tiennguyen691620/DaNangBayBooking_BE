using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Wards
{
    public interface ILocationService
    {
        //Task<ApiResult<Locations>> Create(LocationCreateRequest request);

        //Task<ApiResult<Locations>> Update(LocationUpdateRequest request);

        //Task<ApiResult<int>> Delete(Guid Id);

        //Task<ApiResult<PagedResult<LocationVm>>> GetAllPaging(GetLocationPagingRequest request);

        Task<ApiResult<List<LocationVm>>> GetAllSubDistrict(Guid DistrictID);
        Task<ApiResult<List<LocationVm>>> GetAllDistrict(Guid ProvinceID);
        Task<ApiResult<List<LocationVm>>> GetAllProvince();
        Task<ApiResult<LocationVm>> GetById(Guid Id);
    }
}
