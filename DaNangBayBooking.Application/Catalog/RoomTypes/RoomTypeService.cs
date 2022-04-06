using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.RoomTypes
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly DaNangDbContext _context;
        public RoomTypeService(DaNangDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<RoomTypeVm>>> GetAll()
        {
            var query = await _context.RoomTypes.Select(x => new RoomTypeVm()
            {
                RoomTypeID = x.RoomTypeID,
                Name = x.Name,
                No = x.No,
                Description = x.Description,
            }).ToListAsync();
            return new ApiSuccessResult<List<RoomTypeVm>>(query);
        }

        public async Task<ApiResult<PagedResult<RoomTypeVm>>> GetAllPaging(GetRoomTypePagingRequest request)
        {
            var result = from rt in _context.RoomTypes select new { rt };
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                result = result.Where(x => x.rt.Name.Contains(request.SearchKey)
                 || x.rt.No.Contains(request.SearchKey));
            }
            int totalRow = await result.CountAsync();

            var data = await result.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RoomTypeVm()
                {
                    RoomTypeID = x.rt.RoomTypeID,
                    Name = x.rt.Name,
                    Description = x.rt.Description,
                    No = x.rt.No,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<RoomTypeVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<RoomTypeVm>>(pagedResult);
        }

        public async Task<ApiResult<RoomType>> Post( RoomTypeRequest request )
        {
            string year = DateTime.Now.ToString("yy");
            int count = await _context.RoomTypes.Where(x => x.No.Contains("RT-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "RT-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 99) str = "RT-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 999) str = "RT-" + DateTime.Now.ToString("yy") + "-" + (count + 1);
            var clinics = new RoomType()
            {
                Name = request.Name,
                Description = request.Description,
                No = str
            };
            _context.RoomTypes.Add(clinics);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<RoomType>(clinics);
        }
    }
}
