using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.AccommodationType
{
    public class AccommodationTypeService : IAccommodationTypeService
    {
        private readonly DaNangDbContext _context;

        public AccommodationTypeService(DaNangDbContext context)
        {
            _context = context;
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
