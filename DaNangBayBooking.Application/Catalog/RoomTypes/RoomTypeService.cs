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

        public async Task<ApiResult<bool>> Delete(RoomTypeDeleteRequest request)
        {
            var deleteRoomType = await _context.RoomTypes.FindAsync(request.RoomTypeID);
            /*if (deleteRoomType.Status == true) {
                deleteRoomType.Status = false;
                _context.SaveChanges();
                return new ApiSuccessResult<bool>(true);
            }*/
            if (_context.Rooms.FirstOrDefault(x => x.RoomTypeID == request.RoomTypeID) != null){
                return new ApiSuccessResult<bool>(false);
            }
            _context.RoomTypes.Remove(deleteRoomType);
            _context.SaveChanges();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<RoomTypeVm>>> GetAll()
        {
            var query = await _context.RoomTypes.Where(item => item.Status == true).Select(x => new RoomTypeVm()
            {
                RoomTypeID = x.RoomTypeID,
                Name = x.Name,
                No = x.No,
                Description = x.Description,
                Status = x.Status,
            }).ToListAsync();
            return new ApiSuccessResult<List<RoomTypeVm>>(query);
        }

        public async Task<ApiResult<PagedResult<RoomTypeVm>>> GetAllPaging(GetRoomTypePagingRequest request)
        {
            var result = from rt in _context.RoomTypes select new { rt };
            if (!string.IsNullOrEmpty(request.searchKey))
            {
                result = result.Where(x => x.rt.Name.Contains(request.searchKey)
                 || x.rt.No.Contains(request.searchKey));
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
                    Status = x.rt.Status,
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

        public async Task<ApiResult<RoomTypeVm>> GetByID(Guid RoomTypeID)
        {
            var getRoomTypeByID = await _context.RoomTypes.FindAsync(RoomTypeID);
            var result = new RoomTypeVm() {
                RoomTypeID = getRoomTypeByID.RoomTypeID,
                Name = getRoomTypeByID.Name,
                Description = getRoomTypeByID.Description,
                No = getRoomTypeByID.No,
                Status = getRoomTypeByID.Status,
            };
            return  new ApiSuccessResult<RoomTypeVm>(result);
        }

        public async Task<ApiResult<RoomType>> Create( RoomTypeRequest request )
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
                No = str,
                Status = true,
            };
            _context.RoomTypes.Add(clinics);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<RoomType>(clinics);
        }

        public async Task<ApiResult<bool>> Update(RoomTypeUpdateRequest request)
        {
            var updateRoomType = await _context.RoomTypes.FindAsync(request.RoomTypeID);
            if (updateRoomType == null) {
                return new ApiSuccessResult<bool>(false);
            }
            updateRoomType.Name = request.Name;
            updateRoomType.Description = request.Description;
            updateRoomType.Status = request.Status;
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> UpdateStatusRoomType(Guid RoomTypeId, bool Status)
        {
            var checkStatus = await _context.RoomTypes.FindAsync(RoomTypeId);
            if (checkStatus == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            checkStatus.Status = Status;
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
        }
    }
}
