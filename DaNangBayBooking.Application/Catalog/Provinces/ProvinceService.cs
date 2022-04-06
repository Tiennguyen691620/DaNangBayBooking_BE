using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.Provinces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Provinces
{
    public class ProvinceService : IProvinceService
    {
        private readonly DaNangDbContext _context;

        public ProvinceService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProvinceVm>> GetAll()
        {
            var query = _context.Provinces;
            return await query.Select(x => new ProvinceVm()
            {
                ProvinceID = x.ProvinceID,
                Name = x.Name,
                No = x.No,
                SortOrder = x.SortOrder,
            }).ToListAsync();
        }

        public async Task<ProvinceVm> GetById(Guid ProvinceID)
        {
            var provinces = await _context.Provinces.FindAsync(ProvinceID);
            return new ProvinceVm() 
            { 
                ProvinceID = provinces.ProvinceID,
                Name = provinces.Name,
                No = provinces.No,
                SortOrder = provinces.SortOrder,
            };
        }
    }
}
