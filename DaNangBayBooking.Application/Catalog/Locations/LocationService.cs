using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.Wards;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Wards
{
    /*public class LocationService : ILocationService
    {
        private readonly DaNangDbContext _context;

        public LocationService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<List<LocationVm>> GetAll(Guid DistrictID)
        {
            var query = _context.Wards.Where(p => p.DistrictID == DistrictID);
            return await query.Select(x => new LocationVm()
            {
                WardID = x.WardID,
                Name = x.Name,
                No = x.No,
                SortOrder = x.SortOrder,
            }).ToListAsync();
        }
    }*/
}
