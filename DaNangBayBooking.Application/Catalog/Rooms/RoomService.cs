using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
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
            var room = new Room()
            {
                AccommodationID = AccommodationID,
                RoomTypeID = request.RoomTypeID,
                Name = request.Name,
                AvailableQty = request.AvailableQty,
                PurchasedQty = 0,
                MaximumPeople = request.MaximumPeople,
                Price = request.Price,
                BookedQty = 0,
                ImageRooms = request.Image.Select(i => new ImageRoom()
                {
                    ImageRoomID = i.Id,
                    Image = i.Image,
                    SortOrder = 1,
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
            var imageRooms = from img in _context.ImageRooms select img;
            var rooms = await _context.Rooms.Where(x => x.AccommodationID == AccommodationID).Select(x => new RoomVm() { 
            RoomID = x.RoomID,
            AccommodationID = x.AccommodationID,
            RoomTypeID = x.RoomTypeID,
            Name = x.Name,
            AvailableQty = x.AvailableQty,
            PurchasedQty = x.PurchasedQty,
            MaximumPeople = x.MaximumPeople,
            BookedQty = x.BookedQty,
            Price = x.Price,
            No = x.No,
            ImageRooms = imageRooms.Where(i => i.RoomID == x.RoomID).Select(i => new ImageAccommodationVm()
               {
                  ImageAccommodationID = i.ImageRoomID,
                  //AccommodationID = x.RoomID,
                  Image = i.Image,
                  SortOrder = i.SortOrder
               }).ToList(),
        }).ToListAsync();
            return new ApiSuccessResult<List<RoomVm>>(rooms);
        }
    }
}
