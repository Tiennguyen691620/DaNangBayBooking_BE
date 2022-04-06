using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.Districts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Districts
{
    public class DistrictService : IDistrictService
    {
        private readonly DaNangDbContext _context;

        public DistrictService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<List<DistrictVm>> GetAll(Guid ProvinceID)
        {
            var query = _context.Districts.Where(p => p.ProvinceID == ProvinceID);
            return await query.Select(x => new DistrictVm()
            {
                DistrictID = x.DistrictID,
                Name = x.Name,
                No = x.No,
                SortOrder = x.SortOrder,
            }).ToListAsync();
        }
    }
}
