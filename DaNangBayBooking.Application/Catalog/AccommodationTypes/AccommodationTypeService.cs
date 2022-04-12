using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.AccommodationTypes
{
    public class AccommodationTypeService : IAccommodationTypeService
    {
        private readonly DaNangDbContext _context;

        public AccommodationTypeService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(AccommodationTypeCreateRequest request)
        {
            string year = DateTime.Now.ToString("yy");
            int count = await _context.AccommodationTypes.Where(x => x.No.Contains("ACCT-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "ACCT-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 99) str = "ACCT-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 999) str = "ACCT-" + DateTime.Now.ToString("yy") + "-" + (count + 1);
            var clinics = new AccommodationType()
            {
                Name = request.Name,
                Description = request.Description,
                No = str,
            };
            _context.AccommodationTypes.Add(clinics);
            var result = await _context.SaveChangesAsync();
            if( result == 1)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
        }

        public async Task<ApiResult<List<AccommodationTypeVm>>> GetAll()
        {
            var query = await _context.AccommodationTypes.Select(x => new AccommodationTypeVm()
            {
                AccommodationTypeID = x.AccommodationTypeID,
                Name = x.Name,
                No = x.No,
                Description = x.Description,
            }).ToListAsync();
            return new ApiSuccessResult<List<AccommodationTypeVm>>(query);
        }
    }
}
