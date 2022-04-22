using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.Utilities;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Utilities
{
    public class UtilityService : IUtilityService
    {
        private readonly DaNangDbContext _context;
        public UtilityService(
            DaNangDbContext context
            )
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateUtility(List<CreateUtilityRequest> request, Guid AccommodationID)
        {
            foreach(var x in request)
            {
                var utility =  new Utility()
                {
                    AccommodationID = AccommodationID,
                    UtilityType = x.UtilityType,
                    IsPrivate = x.IsPrivate,
                };
                await _context.Utilities.AddAsync(utility);
            }
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<UtilityVm>>> GetDetailUtility(Guid AccommodationID)
        {
            var utilities = await _context.Utilities.Where(x => x.AccommodationID == AccommodationID).Select(x => new UtilityVm()
            {
                UtilityID = x.UtilityID,
                UtilityType = x.UtilityType,
                IsPrivate = x.IsPrivate,
            }).ToListAsync();
            return new ApiSuccessResult<List<UtilityVm>>(utilities);
        }

        public async Task<ApiResult<bool>> UpdateUtility(List<UpdateUtilityRequest> request, Guid AccommodationID)
        {
            var listUtility = _context.Utilities.Where(x => x.AccommodationID == AccommodationID);
            if( listUtility != null)
            {
            foreach (var x in listUtility) {
                var removeUtility = await _context.Utilities.FindAsync(x.UtilityID);
                _context.Utilities.Remove(removeUtility);
            }
            }
            var accommodation = await _context.Accommodations.FindAsync(AccommodationID);
            accommodation.Utilities = new List<Utility>();
            accommodation.Utilities = request.Select(x => new Utility() { 
                UtilityType = x.UtilityType,
                IsPrivate = x.IsPrivate,
            }).ToList();
            var result = await _context.SaveChangesAsync();
            /*var a = 0;
            foreach(var x in request) {
                var utility = await _context.Utilities.FindAsync(x.UtilityID);
                //if()
                utility.UtilityID = x.UtilityID;
                utility.UtilityType = x.UtilityType;
                utility.IsPrivate = x.IsPrivate;
            var result = await _context.SaveChangesAsync();
                if(result !=0 ) {
                    a = a + 1;
                }
            }*/
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }
    }
}
