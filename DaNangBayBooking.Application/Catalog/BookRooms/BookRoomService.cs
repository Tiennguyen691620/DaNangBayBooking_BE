using DaNangBayBooking.Application.Catalog.BookRooms;
using DaNangBayBooking.Application.Common.Storage;
using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.BookRoom;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using DaNangBayBooking.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Bookings
{
    public class BookRoomService : IBookRoomService
    {
        private readonly DaNangDbContext _context;
        private readonly IStorageService _storageService;

        public BookRoomService(
            DaNangDbContext context,
            IStorageService iStorageService
            )
        {
            _context = context;
            _storageService = iStorageService;
        }

        public async Task<ApiResult<bool>> CreateBookingRoom(BookRoomCreateRequest request)
        {
            string year = DateTime.Now.ToString("yy");
            int count = await _context.BookRooms.Where(x => x.No.Contains("BK-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "BK-" + DateTime.Now.ToString("yy") + "-000" + (count + 1);
            else if (count < 99) str = "BK-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 999) str = "BK-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 9999) str = "BK-" + DateTime.Now.ToString("yy") + "-" + (count + 1);

            var BookRoom = new BookRoom()
            {
                No = str,
                Qty = request.Qty,
                BookingDate = DateTime.Now,
                BookingUser = request.BookingUser,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                CheckInName = request.CheckInName,
                CheckInMail = request.CheckInMail,
                CheckInNote = request.CheckInNote,
                CheckInIdentityCard = request.CheckInIdentityCard,
                CheckInPhoneNumber = request.CheckInPhoneNumber,
                TotalPrice = request.TotalPrice,
                AccommodationID = request.Accommodation.AccommodationID,
                Status = Data.Enums.BookingStatus.Success,
            };

            BookRoom.BookRoomDetails = new List<BookRoomDetail>();
            foreach (var i in request.Room) {
                var listRoom = new BookRoomDetail()
                {
                    RoomID = i.RoomID,
                    ChildNumber = request.ChildNumber,
                    PersonNumber = request.PersonNumber,
                    Status = Data.Enums.BookingStatus.Success,
                };
                BookRoom.BookRoomDetails.Add(listRoom);
            }
            await _context.BookRooms.AddAsync(BookRoom);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }
    }
}
