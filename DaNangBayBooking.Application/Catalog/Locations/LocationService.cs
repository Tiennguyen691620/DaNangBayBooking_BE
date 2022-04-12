using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Wards
{
    public class LocationService : ILocationService
    {
        private readonly DaNangDbContext _context;

        public LocationService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<LocationVm>>> GetAllSubDistrict(Guid DistrictID)
        {
            var query = _context.Locations.Where(x => x.Type.ToUpper() == "SUBDISTRICT" && x.ParentID == DistrictID);

            var rs = await query.Select(x => new LocationVm()
            {
                LocationID = x.LocationID,
                Name = x.Name,
                SortOrder = x.SortOrder,
                ParentID = x.ParentID
            }).ToListAsync();
            return new ApiSuccessResult<List<LocationVm>>(rs);
        }
        public async Task<ApiResult<List<LocationVm>>> GetAllDistrict(Guid ProvinceID)
        {
            var query = _context.Locations.Where(x => x.Type.ToUpper() == "DISTRICT" && x.ParentID == ProvinceID);

            var rs = await query.Select(x => new LocationVm()
            {
                LocationID = x.LocationID,
                Name = x.Name,
                SortOrder = x.SortOrder,
                ParentID = x.ParentID
            }).ToListAsync();
            return new ApiSuccessResult<List<LocationVm>>(rs);
        }
        public async Task<ApiResult<List<LocationVm>>> GetAllProvince()
        {
            var query = _context.Locations.Where(x => x.Type.ToUpper() == "PROVINCE");

            var rs = await query.Select(x => new LocationVm()
            {
                LocationID = x.LocationID,
                Name = x.Name,
                SortOrder = x.SortOrder,
                ParentID = x.ParentID
            }).ToListAsync();
            return new ApiSuccessResult<List<LocationVm>>(rs);
        }

        public async Task<ApiResult<LocationVm>> GetById(Guid Id)
        {
            var Locations = await _context.Locations.FindAsync(Id);
            if (Locations == null)
            return new ApiSuccessResult<LocationVm>();
            var rs = new LocationVm()
            {
                LocationID = Locations.LocationID,
                Name = Locations.Name,
                SortOrder = Locations.SortOrder,
                ParentID = Locations.ParentID,
                IsDeleted = Locations.IsDeleted,
                Code = Locations.Code,
                Type = Locations.Type
            };
            return new ApiSuccessResult<LocationVm>(rs);
        }
    }
}
