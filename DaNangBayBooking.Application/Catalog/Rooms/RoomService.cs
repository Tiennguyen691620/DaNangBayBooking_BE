using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly DaNangDbContext _context;
        public RoomService(
            DaNangDbContext context
            )
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateRoom(Guid AccommodationID, CreateRoomRequest request)
        {
            string year = DateTime.Now.ToString("yy");
            int count = await _context.Rooms.Where(x => x.No.Contains("R-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "R-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 99) str = "R-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 999) str = "R-" + DateTime.Now.ToString("yy") + "-" + (count + 1);
            var room = new Room()
            {
                AccommodationID = AccommodationID,
                No = str,
                RoomTypeID = request.RoomTypeID,
                Name = request.Name,
                AvailableQty = request.AvailableQty,
                PurchasedQty = 0,
                MaximumPeople = request.MaximumPeople,
                Price = request.Price,
                BookedQty = 0,
                ImageRooms = request.Images.Select(i => new ImageRoom()
                {
                    ImageRoomID = i.Id,
                    Image = i.Image,
                }).ToList(),
            };
            _context.Rooms.Add(room);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<RoomVm>>> GetRoomDetail(Guid AccommodationID)
        {
            var roomTypes = from a in _context.RoomTypes select a;
            var imageRooms = from img in _context.ImageRooms select img;
            var rooms = await _context.Rooms.Where(x => x.AccommodationID == AccommodationID).Select(x => new RoomVm() { 
            RoomID = x.RoomID,
            //AccommodationID = x.AccommodationID,
            /*RoomType = roomTypes.Where(i => i.RoomTypeID == x.RoomTypeID).Select(i => new RoomTypeVm()
            {
                RoomTypeID = i.RoomTypeID,
                Description = i.Description,
                Name = i.Name,
                No = i.No,
                Status = i.Status,
            }),*/
            Name = x.Name,
            AvailableQty = x.AvailableQty,
            PurchasedQty = x.PurchasedQty,
            MaximumPeople = x.MaximumPeople,
            BookedQty = x.BookedQty,
            Price = x.Price,
            No = x.No,
            Images = imageRooms.Where(i => i.RoomID == x.RoomID).Select(i => new ImageAccommodationVm()
               {
                  Id = i.ImageRoomID,
                  Image = i.Image,
               }).ToList(),
        }).ToListAsync();
            return new ApiSuccessResult<List<RoomVm>>(rooms);
        }

        public Task<ApiResult<bool>> UpdateRoom(Guid AccommodationID, UpdateRoomRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
