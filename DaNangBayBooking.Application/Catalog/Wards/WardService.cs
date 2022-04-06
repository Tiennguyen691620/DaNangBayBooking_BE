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
    public class WardService : IWardService
    {
        private readonly DaNangDbContext _context;

        public WardService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<List<WardVm>> GetAll(Guid DistrictID)
        {
            var query = _context.Wards.Where(p => p.DistrictID == DistrictID);
            return await query.Select(x => new WardVm()
            {
                WardID = x.WardID,
                Name = x.Name,
                No = x.No,
                SortOrder = x.SortOrder,
            }).ToListAsync();
        }
    }
}
