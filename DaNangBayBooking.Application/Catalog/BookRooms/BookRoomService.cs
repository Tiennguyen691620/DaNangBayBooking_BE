using DaNangBayBooking.Application.Catalog.BookRooms;
using DaNangBayBooking.Application.Common.Storage;
using DaNangBayBooking.Data.EF;
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
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.RoomTypes;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.BookRoomDetail;

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
            var sendMailCustomer = await _context.AppUsers.FindAsync(request.UserId);
            string year = DateTime.Now.ToString("ddMMyy");
            int count = await _context.BookRooms.Where(x => x.UserID == sendMailCustomer.Id).CountAsync();
            string str = "";
            if (count < 9) str = "BK-" + DateTime.Now.ToString("ddMMyy") + "-000" + (count + 1);
            else if (count < 99) str = "BK-" + DateTime.Now.ToString("ddMMyy") + "-00" + (count + 1);
            else if (count < 999) str = "BK-" + DateTime.Now.ToString("ddMMyy") + "-0" + (count + 1);
            else if (count < 9999) str = "BK-" + DateTime.Now.ToString("ddMMyy") + "-" + (count + 1);

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
                BookingUser = request.bookingUser,
                TotalPrice = request.TotalPrice,
                AccommodationID = request.Accommodation.AccommodationID,
                Status = Data.Enums.BookingStatus.Confirmed,
            };

            BookRoom.BookRoomDetails = new List<BookRoomDetail>();
            var listRoom = new BookRoomDetail()
            {
                RoomID = request.Room.RoomID,
                ChildNumber = request.ChildNumber,
                PersonNumber = request.PersonNumber,
                Status = Data.Enums.BookingStatus.Confirmed,
            };
            BookRoom.BookRoomDetails.Add(listRoom);
            await _context.BookRooms.AddAsync(BookRoom);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            var sendMailAccommodation = await _context.Accommodations.FindAsync(request.Accommodation.AccommodationID);
            var room = await _context.Rooms.FindAsync(listRoom.RoomID);
            var roomType = await _context.RoomTypes.FindAsync(room.RoomTypeID);
            await SendEmailToAccommodation(sendMailAccommodation, BookRoom, listRoom, room, roomType);
            return new ApiSuccessResult<bool>(true);
        }

        private async Task SendEmailToAccommodation(Accommodation accommodation, BookRoom bookRoom, BookRoomDetail listRoom, Room room, RoomType roomType)
        {
            string appDomain = _config.GetSection("Application:AppDomain").Value;
            string AcceptBooking = _config.GetSection("Application:AcceptBooking").Value;
            string CancelBooking = _config.GetSection("Application:CancelBooking").Value;
            
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { accommodation.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Name}}", bookRoom.Accommodation.Name),
                    new KeyValuePair<string, string>("{{BookingUser}}", bookRoom.BookingUser),
                    new KeyValuePair<string, string>("{{CheckInName}}", bookRoom.CheckInName),
                    new KeyValuePair<string, string>("{{CheckInPhoneNumber}}", bookRoom.CheckInPhoneNumber),
                    new KeyValuePair<string, string>("{{CheckInIdentityCard}}", bookRoom.CheckInIdentityCard),
                    new KeyValuePair<string, string>("{{CheckInNote}}", bookRoom.CheckInNote),
                    new KeyValuePair<string, string>("{{FromDate}}", bookRoom.FromDate.ToShortDateString()),
                    new KeyValuePair<string, string>("{{ToDate}}", bookRoom.ToDate.ToShortDateString()),
                    new KeyValuePair<string, string>("{{Qty}}", bookRoom.Qty.ToString()),
                    new KeyValuePair<string, string>("{{TotalDay}}", bookRoom.TotalDay.ToString()),
                    new KeyValuePair<string, string>("{{TotalPrice}}", bookRoom.TotalPrice.ToString("#,###,###")),
                    new KeyValuePair<string, string>("{{No}}", bookRoom.No),
                    new KeyValuePair<string, string>("{{PersonNumber}}", listRoom.PersonNumber.ToString()),
                    new KeyValuePair<string, string>("{{ChildNumber}}", listRoom.ChildNumber.ToString()),
                    new KeyValuePair<string, string>("{{RoomName}}", room.Name),
                    new KeyValuePair<string, string>("{{RoomTypeName}}", roomType.Name),
                    new KeyValuePair<string, string>("{{LinkSuccess}}", string.Format(appDomain + AcceptBooking, bookRoom.BookRoomID)),
                    new KeyValuePair<string, string>("{{LinkCancel}}", string.Format(appDomain + CancelBooking, bookRoom.BookRoomID)),
                }
            };

            await _emailService.SendEmailBookRoomToAccommodation(options);
        }

        public async Task<ApiResult<PagedResult<BookRoomVm>>> FilterBooking(FilterBookRoomRequest request)
        {
            var query = from br in _context.BookRooms
                        join a in _context.Accommodations on br.AccommodationID equals a.AccommodationID
                        join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                        join r in _context.Rooms on brt.RoomID equals r.RoomID
                        //where br.UserID == request.UserId
                        select new { br, a, brt, r };


            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.br.CheckInPhoneNumber.Contains(request.SearchKey)
                 || x.br.No.Contains(request.SearchKey) || x.br.Accommodation.Name.Contains(request.SearchKey));
            }

            if (request.FromDate != null && request.ToDate != null)
            {
                var fromDate = request.FromDate.FromUnixTimeStamp();
                var toDate = request.ToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.FromDate >= fromDate && x.br.ToDate <= toDate);
            }

            if (request.BookingFromDate != null && request.BookingToDate != null)
            {
                var fromDate = request.BookingFromDate.FromUnixTimeStamp();
                var toDate = request.BookingToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.BookingDate >= fromDate && x.br.BookingDate <= toDate);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BookRoomVm()
                {
                    BookRoomID = x.br.BookRoomID,
                    No = x.br.No,
                    Qty = x.br.Qty,
                    BookingDate = x.br.BookingDate.ToSecondsTimestamp(),
                    FromDate = x.br.FromDate.ToSecondsTimestamp(),
                    ToDate = x.br.ToDate.ToSecondsTimestamp(),
                    TotalDay = x.br.TotalDay,
                    CheckInName = x.br.CheckInName,
                    CheckInMail = x.br.CheckInMail,
                    CheckInNote = x.br.CheckInNote,
                    CheckInIdentityCard = x.br.CheckInIdentityCard,
                    CheckInPhoneNumber = x.br.CheckInPhoneNumber,
                    bookingUser = x.br.BookingUser,
                    TotalPrice = x.br.TotalPrice,
                    Status = x.br.Status,
                    Accommodation = new AccommodationVm()
                    {
                        Name = x.a.Name,
                    },
                    BookRoomDetail = new BookRoomDetailVm()
                    {

                    },
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<BookRoomVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<BookRoomVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<BookRoomVm>>> FilterBookingClient(FilterBookRoomRequest request)
        {
            var query = from br in _context.BookRooms
                        join a in _context.Accommodations on br.AccommodationID equals a.AccommodationID
                        join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                        join r in _context.Rooms on brt.RoomID equals r.RoomID
                        join rc in _context.RateComments on br.BookRoomID equals rc.BookRoomID into ratecomment
                        from rc in ratecomment.DefaultIfEmpty()
                        where br.UserID == request.UserId
                        select new { br, a, brt, r, rc };


            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.br.CheckInPhoneNumber.Contains(request.SearchKey)
                 || x.br.No.Contains(request.SearchKey) || x.br.Accommodation.Name.Contains(request.SearchKey));
            }

            if (request.FromDate != null && request.ToDate != null)
            {
                var fromDate = request.FromDate.FromUnixTimeStamp();
                var toDate = request.ToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.FromDate >= fromDate && x.br.ToDate <= toDate);
            }

            if (request.BookingFromDate != null && request.BookingToDate != null)
            {
                var fromDate = request.BookingFromDate.FromUnixTimeStamp();
                var toDate = request.BookingToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.BookingDate >= fromDate && x.br.BookingDate <= toDate);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BookRoomVm()
                {
                    BookRoomID = x.br.BookRoomID,
                    No = x.br.No,
                    Qty = x.br.Qty,
                    BookingDate = x.br.BookingDate.ToSecondsTimestamp(),
                    FromDate = x.br.FromDate.ToSecondsTimestamp(),
                    ToDate = x.br.ToDate.ToSecondsTimestamp(),
                    TotalDay = x.br.TotalDay,
                    CheckInName = x.br.CheckInName,
                    CheckInMail = x.br.CheckInMail,
                    CheckInNote = x.br.CheckInNote,
                    CheckInIdentityCard = x.br.CheckInIdentityCard,
                    CheckInPhoneNumber = x.br.CheckInPhoneNumber,
                    bookingUser = x.br.BookingUser,
                    TotalPrice = x.br.TotalPrice,
                    Status = x.br.Status,
                    CheckComment = x.rc == null ? true : false,
                    Accommodation = new AccommodationVm() { 
                        Name = x.a.Name,
                    },
                    BookRoomDetail = new BookRoomDetailVm()
                    {

                    },
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<BookRoomVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<BookRoomVm>>(pagedResult);
        }


        public async Task<ApiResult<bool>> CloseBooking(FilterBookRoomRequest request)
        {
            var expired = from br in _context.BookRooms
                          join a in _context.Accommodations on br.AccommodationID equals a.AccommodationID
                          join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                          join r in _context.Rooms on brt.RoomID equals r.RoomID
                          where br.UserID == request.UserId && br.ToDate <= DateTime.Now && br.Status == BookingStatus.Success
                          select new { br, a, brt, r };
            foreach (var remove in expired)
            {
                var addexpired = await _context.BookRooms.FindAsync(remove.br.BookRoomID);
                addexpired.Status = BookingStatus.Closed;
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> CancelBooking(CancelBookingRequest request)
        {
            var checkStatusBooking = await _context.BookRooms.FindAsync(request.Id);
            if(checkStatusBooking == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            checkStatusBooking.Status = BookingStatus.Canceled;
            var checkCancelReson = _context.BookRoomDetails.FirstOrDefault(x => x.BookRoomID == checkStatusBooking.BookRoomID);
            var bookDetail = await _context.BookRoomDetails.FindAsync(checkCancelReson.BookRoomDetailID);
            var room = await _context.Rooms.FindAsync(bookDetail.RoomID);
            var roomType = await _context.RoomTypes.FindAsync(room.RoomTypeID);
            if(bookDetail == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            bookDetail.Status = BookingStatus.Canceled;
            bookDetail.CancelReason = request.CancelReason;
            bookDetail.CancelDate = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            var sendMailAccommodation = await _context.Accommodations.FindAsync(checkStatusBooking.AccommodationID);
            if (result != 0)
            {
                await SendEmailCancelToAccommodation(sendMailAccommodation, checkStatusBooking, bookDetail, room, roomType);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
        }

        private async Task SendEmailCancelToAccommodation(Accommodation accommodation, BookRoom bookRoom, BookRoomDetail bookDetail, Room room, RoomType roomType)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { accommodation.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Name}}", bookRoom.Accommodation.Name),
                    new KeyValuePair<string, string>("{{BookingUser}}", bookRoom.BookingUser),
                    new KeyValuePair<string, string>("{{CheckInName}}", bookRoom.CheckInName),
                    new KeyValuePair<string, string>("{{CheckInPhoneNumber}}", bookRoom.CheckInPhoneNumber),
                    new KeyValuePair<string, string>("{{CheckInIdentityCard}}", bookRoom.CheckInIdentityCard),
                    new KeyValuePair<string, string>("{{CheckInNote}}", bookRoom.CheckInNote),
                    new KeyValuePair<string, string>("{{FromDate}}", bookRoom.FromDate.ToShortDateString()),
                    new KeyValuePair<string, string>("{{ToDate}}", bookRoom.ToDate.ToShortDateString()),
                    new KeyValuePair<string, string>("{{Qty}}", bookRoom.Qty.ToString()),
                    new KeyValuePair<string, string>("{{TotalDay}}", bookRoom.TotalDay.ToString()),
                    new KeyValuePair<string, string>("{{TotalPrice}}", bookRoom.TotalPrice.ToString("#,###,###")),
                    new KeyValuePair<string, string>("{{No}}", bookRoom.No),
                    new KeyValuePair<string, string>("{{PersonNumber}}", bookDetail.PersonNumber.ToString()),
                    new KeyValuePair<string, string>("{{ChildNumber}}", bookDetail.ChildNumber.ToString()),
                    new KeyValuePair<string, string>("{{RoomName}}", room.Name),
                    new KeyValuePair<string, string>("{{RoomTypeName}}", roomType.Name),
                }
            };

            await _emailService.SendEmailCancelToAccommodation(options);
        }

        public async Task<ApiResult<BookRoomVm>> GetById(Guid id)
        {
            var bookRoom = await _context.BookRooms.FindAsync(id);
            if(bookRoom == null)
            {
                return new ApiErrorResult<BookRoomVm>("Đặt phòng không tồn tại");
            }

            var accommodation = await _context.Accommodations.FindAsync(bookRoom.AccommodationID);
            var bookRoomDetail = _context.BookRoomDetails.Where(x => x.BookRoomID == bookRoom.BookRoomID).FirstOrDefault();
            var room = await _context.Rooms.FindAsync(bookRoomDetail.RoomID);

            var imageRoom = _context.ImageRooms.Where(x => x.RoomID == room.RoomID).FirstOrDefault();
            var roomType = await _context.RoomTypes.FindAsync(room.RoomTypeID);
            var imageAccommodations = from img in _context.ImageAccommodations select img;
            var accommodationtype = await _context.AccommodationTypes.FindAsync(accommodation.AccommodationTypeID);
            var sd = await _context.Locations.FindAsync(accommodation.LocationID);
            var d = await _context.Locations.FindAsync(sd.ParentID);
            var p = await _context.Locations.FindAsync(d.ParentID);

            var bookRoomVm = new BookRoomVm() { 
                BookRoomID = bookRoom.BookRoomID,
                No = bookRoom.No,
                Qty = bookRoom.Qty,
                TotalDay = bookRoom.TotalDay,
                TotalPrice = bookRoom.TotalPrice,
                BookingDate = bookRoom.BookingDate.ToSecondsTimestamp(),
                FromDate = bookRoom.BookingDate.ToSecondsTimestamp(),
                ToDate = bookRoom.ToDate.ToSecondsTimestamp(),
                CheckInName = bookRoom.CheckInName,
                CheckInMail = bookRoom.CheckInMail,
                CheckInIdentityCard = bookRoom.CheckInIdentityCard,
                CheckInPhoneNumber = bookRoom.CheckInPhoneNumber,
                CheckInNote = bookRoom.CheckInNote,
                bookingUser = bookRoom.BookingUser,
                Status = bookRoom.Status,
                
                Accommodation = new AccommodationVm()
                {
                    AccommodationID = accommodation.AccommodationID,
                    Name = accommodation.Name,
                    AbbreviationName = accommodation.AbbreviationName,
                    Description = accommodation.Description,
                    Email = accommodation.Email,
                    Phone = accommodation.Phone,
                    MapURL = accommodation.MapURL,
                    No = accommodation.No,
                    Address = accommodation.Address,
                    Status = accommodation.Status,
                    Province = new LocationProvince()
                    {
                        LocationID = p.LocationID,
                        Name = p.Name,
                        IsDeleted = p.IsDeleted,
                        ParentID = p.ParentID,
                        Code = p.Code,
                        Type = p.Type,
                        SortOrder = p.SortOrder
                    },
                    District = new LocationDistrict()
                    {
                        LocationID = d.LocationID,
                        Name = d.Name,
                        IsDeleted = d.IsDeleted,
                        ParentID = d.ParentID,
                        Code = d.Code,
                        Type = d.Type,
                        SortOrder = d.SortOrder
                    },
                    SubDistrict = new LocationSubDistrict()
                    {
                        LocationID = sd.LocationID,
                        Name = sd.Name,
                        IsDeleted = sd.IsDeleted,
                        ParentID = sd.ParentID,
                        Code = sd.Code,
                        Type = sd.Type,
                        SortOrder = sd.SortOrder
                    },
                    AccommodationType = new AccommodationTypeVm()
                    {
                        AccommodationTypeID = accommodationtype.AccommodationTypeID,
                        Name = accommodationtype.Name,
                        Description = accommodationtype.Description,
                        No = accommodationtype.No,
                    },
                    Images = imageAccommodations.Where(i => i.AccommodationID == accommodation.AccommodationID).Select(i => new ImageAccommodationVm()
                    {
                        Id = i.ImageAccommodationID,
                        Image = i.Image,
                    }).ToList(),
                },
                BookRoomDetail = new BookRoomDetailVm()
                {
                    BookRoomDetailID = bookRoomDetail.BookRoomDetailID,
                    ChildNumber = bookRoomDetail.ChildNumber,
                    PersonNumber = bookRoomDetail.PersonNumber,
                    CancelDate = bookRoomDetail.CancelDate,
                    CancelReason = bookRoomDetail.CancelReason,
                    Status = bookRoomDetail.Status,
                    Room = new RoomVm()
                    {
                        RoomID = room.RoomID,
                        RoomType = new RoomTypeVm()
                        {
                            RoomTypeID = roomType.RoomTypeID,
                            Description = roomType.Description,
                            Name = roomType.Name,
                            No = roomType.No,
                            Status = roomType.Status,
                        },
                        Name = room.Name,
                        AvailableQty = room.AvailableQty,
                        PurchasedQty = room.PurchasedQty,
                        MaximumPeople = room.MaximumPeople,
                        BookedQty = room.BookedQty,
                        Price = room.Price,
                        No = room.No,
                        Image = imageRoom.Image,
                    },
                }
                
            };
            return new ApiSuccessResult<BookRoomVm>(bookRoomVm);
        }

        public async Task<ApiResult<List<BookRoomVm>>> ReportBooking(FilterBookRoomReportRequest request)
        {
            var query = from br in _context.BookRooms
                        join a in _context.Accommodations on br.AccommodationID equals a.AccommodationID
                        join t in _context.AccommodationTypes on a.AccommodationTypeID equals t.AccommodationTypeID
                        join sd in _context.Locations on a.LocationID equals sd.LocationID
                        join d in _context.Locations on sd.ParentID equals d.LocationID
                        join p in _context.Locations on d.ParentID equals p.LocationID
                        join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                        join r in _context.Rooms on brt.RoomID equals r.RoomID
                        join rt in _context.RoomTypes on r.RoomTypeID equals rt.RoomTypeID
                        select new { br, a, brt, r, sd, d, p, t, rt };
            var imageAccommodations = from img in _context.ImageAccommodations select img;
            //var imageRoom = _context.ImageRooms.Where(x => x.RoomID == .RoomID).FirstOrDefault();

            if (request.AccommodationId != null)
            {
                query = query.Where(x => x.a.AccommodationID == request.AccommodationId);
            }
            
            if (request.Status != null)
            {
                var status = (BookingStatus)request.Status;
                query = query.Where(x => x.br.Status == status);
            }

            if (request.CheckInFromDate != null && request.CheckInToDate != null)
            {
                var fromDate = request.CheckInFromDate.FromUnixTimeStamp();
                var toDate = request.CheckInToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.FromDate >= fromDate && x.br.ToDate <= toDate);
            }

            if (request.BookingFromDate != null && request.BookingToDate != null)
            {
                var fromDate = request.BookingFromDate.FromUnixTimeStamp();
                var toDate = request.BookingToDate.FromUnixTimeStamp();
                query = query.Where(x => x.br.BookingDate >= fromDate && x.br.BookingDate <= toDate);
            }

            var data = await query.Select(x => new BookRoomVm() {
                BookRoomID = x.br.BookRoomID,
                No = x.br.No,
                Qty = x.br.Qty,
                TotalDay = x.br.TotalDay,
                TotalPrice = x.br.TotalPrice,
                BookingDate = x.br.BookingDate.ToSecondsTimestamp(),
                FromDate = x.br.BookingDate.ToSecondsTimestamp(),
                ToDate = x.br.ToDate.ToSecondsTimestamp(),
                CheckInName = x.br.CheckInName,
                CheckInMail = x.br.CheckInMail,
                CheckInIdentityCard = x.br.CheckInIdentityCard,
                CheckInPhoneNumber = x.br.CheckInPhoneNumber,
                CheckInNote = x.br.CheckInNote,
                bookingUser = x.br.BookingUser,
                Status = x.br.Status,
                Accommodation = new AccommodationVm()
                {
                    AccommodationID = x.a.AccommodationID,
                    Name = x.a.Name,
                    AbbreviationName = x.a.AbbreviationName,
                    Description = x.a.Description,
                    Email = x.a.Email,
                    Phone = x.a.Phone,
                    MapURL = x.a.MapURL,
                    No = x.a.No,
                    Address = x.a.Address,
                    Status = x.a.Status,
                    Province = new LocationProvince()
                    {
                        LocationID = x.p.LocationID,
                        Name = x.p.Name,
                        IsDeleted = x.p.IsDeleted,
                        ParentID = x.p.ParentID,
                        Code = x.p.Code,
                        Type = x.p.Type,
                        SortOrder = x.p.SortOrder
                    },
                    District = new LocationDistrict()
                    {
                        LocationID = x.d.LocationID,
                        Name = x.d.Name,
                        IsDeleted = x.d.IsDeleted,
                        ParentID = x.d.ParentID,
                        Code = x.d.Code,
                        Type = x.d.Type,
                        SortOrder = x.d.SortOrder
                    },
                    SubDistrict = new LocationSubDistrict()
                    {
                        LocationID = x.sd.LocationID,
                        Name = x.sd.Name,
                        IsDeleted = x.sd.IsDeleted,
                        ParentID = x.sd.ParentID,
                        Code = x.sd.Code,
                        Type = x.sd.Type,
                        SortOrder = x.sd.SortOrder
                    },
                    AccommodationType = new AccommodationTypeVm()
                    {
                        AccommodationTypeID = x.t.AccommodationTypeID,
                        Name = x.t.Name,
                        Description = x.t.Description,
                        No = x.t.No,
                    },
                    Images = imageAccommodations.Where(i => i.AccommodationID == x.a.AccommodationID).Select(i => new ImageAccommodationVm()
                    {
                        Id = i.ImageAccommodationID,
                        Image = i.Image,
                    }).ToList(),
                },
                BookRoomDetail = new BookRoomDetailVm()
                {
                    BookRoomDetailID = x.brt.BookRoomDetailID,
                    ChildNumber = x.brt.ChildNumber,
                    PersonNumber = x.brt.PersonNumber,
                    CancelDate = x.brt.CancelDate,
                    CancelReason = x.brt.CancelReason,
                    Status = x.brt.Status,
                    Room = new RoomVm()
                    {
                        RoomID = x.r.RoomID,
                        RoomType = new RoomTypeVm()
                        {
                            RoomTypeID = x.rt.RoomTypeID,
                            Description = x.rt.Description,
                            Name = x.rt.Name,
                            No = x.rt.No,
                            Status = x.rt.Status,
                        },
                        Name = x.r.Name,
                        AvailableQty = x.r.AvailableQty,
                        PurchasedQty = x.r.PurchasedQty,
                        MaximumPeople = x.r.MaximumPeople,
                        BookedQty = x.r.BookedQty,
                        Price = x.r.Price,
                        No = x.r.No,
                        //Image = imageRoom.Image,
                    },
                }
            }).ToListAsync();
            return new ApiSuccessResult<List<BookRoomVm>>(data);
        }

        public async Task<ApiResult<string>> CancelBookingByAccommodation(Guid Id)
        {
            var checkStatusBooking = await _context.BookRooms.FindAsync(Id);
            if (checkStatusBooking == null)
            {
                return new ApiSuccessResult<string>("Hủy không thành công");
            }
            if (checkStatusBooking.Status != BookingStatus.Confirmed)
            {
                return new ApiSuccessResult<string>("Hủy không thành công");
            }
            checkStatusBooking.Status = BookingStatus.Canceled;
            var checkCancelReson = _context.BookRoomDetails.FirstOrDefault(x => x.BookRoomID == checkStatusBooking.BookRoomID);
            var cancelReson = await _context.BookRoomDetails.FindAsync(checkCancelReson.BookRoomDetailID);
            if (cancelReson == null)
            {
                return new ApiSuccessResult<string>("Hủy không thành công");
            }
            if(cancelReson.Status != BookingStatus.Confirmed)
            {
                return new ApiSuccessResult<string>("Hủy không thành công");
            }
            cancelReson.Status = BookingStatus.Canceled;
            cancelReson.CancelReason = "Không hợp lệ";
            cancelReson.CancelDate = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            var sendMailUser = await _context.AppUsers.FindAsync(checkStatusBooking.UserID);
            if (result != 0)
            {
                await SendEmailCancelToUser(sendMailUser, checkStatusBooking);
                return new ApiSuccessResult<string>("Hủy thành công");
            }
            return new ApiSuccessResult<string>("Hủy không thành công");
        }

        private async Task SendEmailCancelToUser(AppUser sendMailUser, BookRoom checkStatusBooking)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { sendMailUser.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{FullName}}", sendMailUser.FullName),
                    new KeyValuePair<string, string>("{{BookRoomNo}}", checkStatusBooking.No),
                }
            };

            await _emailService.SendEmailCancelToUser(options);
        }

        public async Task<ApiResult<string>> SuccessBookingByAccommodation(Guid bookRoomId)
        {
            var checkStatusBooking = await _context.BookRooms.FindAsync(bookRoomId);
            if (checkStatusBooking == null)
            {
                return new ApiSuccessResult<string>("Xác nhận không thành công");
            }
            if (checkStatusBooking.Status != BookingStatus.Confirmed)
            {
                return new ApiSuccessResult<string>("Xác nhận không thành công");
            }
            checkStatusBooking.Status = BookingStatus.Success;
            var checkStatus = _context.BookRoomDetails.FirstOrDefault(x => x.BookRoomID == checkStatusBooking.BookRoomID);
            var statusBookRoomDetail = await _context.BookRoomDetails.FindAsync(checkStatus.BookRoomDetailID);
            /*if (cancelReson == null)
            {
                return new ApiSuccessResult<bool>(false);
            }*/
            if (statusBookRoomDetail.Status != BookingStatus.Confirmed)
            {
                return new ApiSuccessResult<string>("Xác nhận không thành công");
            }
            statusBookRoomDetail.Status = BookingStatus.Success;
            //cancelReson.CancelReason = request.CancelReason;
            //cancelReson.CancelDate = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            var sendMailUser = await _context.AppUsers.FindAsync(checkStatusBooking.UserID);
            if (result != 0)
            {
                await SendEmailSuccessBookingToUser(sendMailUser, checkStatusBooking);
                return new ApiSuccessResult<string>("Xác nhận thành công");
            }
            return new ApiSuccessResult<string>("Xác nhận không thành công");
        }
        private async Task SendEmailSuccessBookingToUser(AppUser sendMailUser, BookRoom checkStatusBooking)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { sendMailUser.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{FullName}}", sendMailUser.FullName),
                    new KeyValuePair<string, string>("{{BookRoomNo}}", checkStatusBooking.No),
                    //new KeyValuePair<string, string>("{{Name}}", checkStatusBooking.Accommodation.Name),
                }
            };

            await _emailService.SendEmailSuccessBookingToUser(options);
        }
    }
}
