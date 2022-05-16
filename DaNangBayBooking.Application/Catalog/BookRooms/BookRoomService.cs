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
using Webgentle.BookStore.Service;
using Microsoft.Extensions.Configuration;
using DaNangBayBooking.ViewModels.System.Users;
using DaNangBayBooking.Utilities.Extensions;

namespace DaNangBayBooking.Application.Catalog.Bookings
{
    public class BookRoomService : IBookRoomService
    {
        private readonly DaNangDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public BookRoomService(
            IEmailService emailService,
            IConfiguration config,
            DaNangDbContext context,
            IStorageService iStorageService
            )
        {
            _context = context;
            _storageService = iStorageService;
            _config = config;
            _emailService = emailService;
        }

        public async Task<ApiResult<bool>> CreateBookingRoom(BookRoomCreateRequest request)
        {
            var sendMailCustomer = await _context.AppUsers.FirstAsync(x => x.Email == request.CheckInMail);
            string year = DateTime.Now.ToString("yy");
            int count = await _context.BookRooms.Where(x => x.No.Contains("BK-" + year)).CountAsync();
            string str = "";
            if (count < 9) str = "BK-" + DateTime.Now.ToString("yy") + "-000" + (count + 1);
            else if (count < 99) str = "BK-" + DateTime.Now.ToString("yy") + "-00" + (count + 1);
            else if (count < 999) str = "BK-" + DateTime.Now.ToString("yy") + "-0" + (count + 1);
            else if (count < 9999) str = "BK-" + DateTime.Now.ToString("yy") + "-" + (count + 1);

            var BookRoom = new BookRoom()
            {
                UserID = sendMailCustomer.Id,
                No = str,
                Qty = request.Qty,
                BookingDate = DateTime.Now,
                FromDate = request.FromDate.FromUnixTimeStamp(),
                ToDate = request.ToDate.FromUnixTimeStamp(),
                TotalDay = request.TotalDay,
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
            var listRoom = new BookRoomDetail()
            {
                RoomID = request.Room.RoomID,
                ChildNumber = request.ChildNumber,
                PersonNumber = request.PersonNumber,
                Status = Data.Enums.BookingStatus.Success,
            };
            BookRoom.BookRoomDetails.Add(listRoom);
            await _context.BookRooms.AddAsync(BookRoom);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            var sendMailAccommodation = await _context.Accommodations.FindAsync(request.Accommodation.AccommodationID);
            await SendEmailConfirmationEmailToUser(sendMailAccommodation, BookRoom);
            return new ApiSuccessResult<bool>(true);
        }


        private async Task SendEmailConfirmationEmailToUser(Accommodation accommodation, BookRoom bookRoom)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { accommodation.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{CheckInPhoneNumber}}", bookRoom.CheckInPhoneNumber),
                    new KeyValuePair<string, string>("{{Name}}", bookRoom.Accommodation.Name),
                    new KeyValuePair<string, string>("{{CheckInName}}", bookRoom.CheckInName),
                }
            };

            await _emailService.SendEmailToAccommodation(options);
        }
    }
}
