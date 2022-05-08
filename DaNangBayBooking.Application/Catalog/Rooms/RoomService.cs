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
            //var roomTypes = from a in _context.RoomTypes select a;
            //var imageRooms = from img in _context.ImageRooms select img;
            var rooms = await _context.Rooms.Where(x => x.AccommodationID == AccommodationID).Select(x => new RoomVm()
            {
                RoomID = x.RoomID,
                RoomType = new RoomTypeVm()
                {
                    RoomTypeID = x.RoomType.RoomTypeID,
                    Description = x.RoomType.Description,
                    Name = x.RoomType.Name,
                    No = x.RoomType.No,
                    Status = x.RoomType.Status,
                },
                Name = x.Name,
                AvailableQty = x.AvailableQty,
                PurchasedQty = x.PurchasedQty,
                MaximumPeople = x.MaximumPeople,
                BookedQty = x.BookedQty,
                Price = x.Price,
                No = x.No,
                Image = x.ImageRooms.FirstOrDefault().Image,
            }).ToListAsync();
            return new ApiSuccessResult<List<RoomVm>>(rooms);
        }

        public async Task<ApiResult<bool>> UpdateRoom(Guid AccommodationID, List<UpdateRoomRequest> request)
        {
            string year = DateTime.Now.ToString("yy");
            string str = "";

            var listRoom = _context.Rooms.Where(x => x.AccommodationID == AccommodationID);
            if (listRoom != null)
            {
                foreach (var x in listRoom)
                {
                    var removeRooms = await _context.Rooms.FindAsync(x.RoomID);
                    if (removeRooms != null )
                    {
                        foreach (var img in listRoom.First(x => x.RoomID == removeRooms.RoomID).ImageRooms)
                        {
                            var removeImg = await _context.ImageRooms.FindAsync(img.ImageRoomID);
                            _context.ImageRooms.Remove(removeImg);
                        }
                    }
                    _context.Rooms.Remove(removeRooms);
                }
            }
            var accommodation = await _context.Accommodations.FindAsync(AccommodationID);
            var Rooms = new List<Room>();
            var i = 0;
            foreach (var Room in request)
            {
                if (i < 9) str = "R-" + DateTime.Now.ToString("yy") + "-00" + (i + 1);
                else if (i < 99) str = "R-" + DateTime.Now.ToString("yy") + "-0" + (i + 1);
                else if (i < 999) str = "R-" + DateTime.Now.ToString("yy") + "-" + (i + 1);
                var addRoom = new Room()
                {
                    RoomID = Room.RoomID,
                    AccommodationID = AccommodationID,
                    Name = Room.Name,
                    No = str,
                    Price = Room.Price,
                    MaximumPeople = Room.MaximumPeople,
                    AvailableQty = Room.AvailableQty,
                    PurchasedQty = Room.PurchasedQty,
                    BookedQty = Room.BookedQty,
                    Description = Room.Description,
                    RoomTypeID = Room.RoomType.RoomTypeID,
                };
                i++;
                addRoom.ImageRooms = new List<ImageRoom>();
                var addImage = new ImageRoom()
                {
                    Image = Room.Image,
                    SortOrder = 1,
                };
                addRoom.ImageRooms.Add(addImage);
                Rooms.Add(addRoom);
            }
            await _context.Rooms.AddRangeAsync(Rooms);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }
    }
}
