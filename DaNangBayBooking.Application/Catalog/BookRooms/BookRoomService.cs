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

        public async Task<ApiResult<PagedResult<BookRoomVm>>> FilterBooking(FilterBookRoomRequest request)
        {
            var query = from br in _context.BookRooms
                        join a in _context.Accommodations on br.AccommodationID equals a.AccommodationID
                        join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                        join r in _context.Rooms on brt.RoomID equals r.RoomID
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
                    TotalPrice = x.br.TotalPrice,
                    Status = x.br.Status,
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

        public async Task<ApiResult<bool>> CancelBooking(CancelBookingRequest request)
        {
            var checkStatusBooking = await _context.BookRooms.FindAsync(request.Id);
            if(checkStatusBooking == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            checkStatusBooking.Status = BookingStatus.Canceled;
            var checkCancelReson = _context.BookRoomDetails.FirstOrDefault(x => x.BookRoomID == checkStatusBooking.BookRoomID);
            var cancelReson = await _context.BookRoomDetails.FindAsync(checkCancelReson.BookRoomDetailID);
            if(cancelReson == null)
            {
                return new ApiSuccessResult<bool>(false);
            }
            cancelReson.Status = BookingStatus.Canceled;
            cancelReson.CancelReason = request.CancelReason;
            cancelReson.CancelDate = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiSuccessResult<bool>(false);
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
    }
}
