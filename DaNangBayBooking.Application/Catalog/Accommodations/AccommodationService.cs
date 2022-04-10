using DaNangBayBooking.Data.EF;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DaNangBayBooking.ViewModels.Catalog.AccommodationType;
using DaNangBayBooking.ViewModels.Catalog.BookRooms;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Catalog.Utilities;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using DaNangBayBooking.ViewModels.Catalog.Images;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using DaNangBayBooking.Application.Common.Storage;
using DaNangBayBooking.Data.Entities;

namespace DaNangBayBooking.Application.Catalog.Accommodations
{
    public class AccommodationService : IAccommodationService
    {
        private readonly DaNangDbContext _context;
        private readonly IStorageService _storageService;
        public AccommodationService(
            DaNangDbContext context,
            IStorageService iStorageService
            )
        {
            _context = context;
            _storageService = iStorageService;
        }

        public async Task<ApiResult<ImageVm>> AddImage(ImageCreateRequest request)
        {
            var img = new ImageVm();
            if (request.File != null)
            {
                img = await this.SaveFile(request.File);
                //var dt = request.File.Length;
            }

            //await _context.SaveChangesAsync();
            return new ApiSuccessResult<ImageVm>(img);
        }
        private async Task<ImageVm> SaveFile(IFormFile? file)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var orgFileExtension = Path.GetExtension(originalFileName);
            var guid = Guid.NewGuid();
            var fileName = $"{guid}{orgFileExtension}";
            var fileRequest = await _storageService.SaveFileImgAsync(file.OpenReadStream(), fileName);
            return new ImageVm()
            {
                ImageID = guid,
                FileName = fileName,
                OrgFileName = originalFileName,
                OrgFileExtension = orgFileExtension,
                FileUrl = fileRequest.FileUrl,
                Container = fileRequest.Container
            };
        }

        public async Task<ApiResult<PagedResult<AccommodationVm>>> GetAccommodationsAllPaging(GetAccommodationPagingRequest request)
        {
            var query = from a in _context.Accommodations
                        join t in _context.AccommodationTypes on a.AccommodationTypeID equals t.AccommodationTypeID
                        join l in _context.Locations on a.LocationID equals l.LocationID
                        select new { a, t, l };

            var broom = from br in _context.BookRooms select br;
            var utilities = from ul in _context.Utilities select ul;
            var rooms = from rm in _context.Rooms select rm;
            var imageAccommodations = from img in _context.ImageAccommodations select img;

            //var patient = _context.Patients;

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x => x.a.Name.Contains(request.SearchKey)
                 || x.a.Email.Contains(request.SearchKey));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AccommodationVm()
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
                    LocationID = x.a.LocationID,
                    AccommodationTypeID = x.a.AccommodationTypeID,
                    AccommodationType = new AccommodationTypeVm()
                    {
                        AccommodationTypeID = x.t.AccommodationTypeID,
                        Name = x.t.Name,
                        Description = x.t.Description,
                        No = x.t.No,
                    },
                    Location = new LocationVm()
                    {
                        LocationID = x.l.LocationID,
                        Name = x.l.Name,
                        SortOrder = x.l.SortOrder,
                        Code = x.l.Code,
                        IsDeleted = x.l.IsDeleted,
                        ParentID = x.l.ParentID,
                        Type = x.l.Type
                    },
                    Utilities = utilities.Where(u => u.AccommodationID == x.a.AccommodationID).Select(u => new UtilityVm()
                    {
                        UtilityID = u.UtilityID,
                        AccommodationID = x.a.AccommodationID,
                        UtilityType = u.UtilityType,
                        IsPrivate = u.IsPrivate
                    }).ToList(),
                    Rooms = rooms.Where(r => r.AccommodationID == x.a.AccommodationID).Select(r => new RoomVm()
                    {
                        RoomID = r.RoomID,
                        AccommodationID = x.a.AccommodationID,
                        RoomTypeID = r.RoomTypeID,
                        Name = r.Name,
                        AvailableQty = r.AvailableQty,
                        Price = r.Price,
                        BookedQty = r.BookedQty,
                        PurchasedQty = r.PurchasedQty,
                        MaximumPeople = r.MaximumPeople,
                        No = r.No
                    }).ToList(),
                    ImageAccommodations = imageAccommodations.Where(i => i.AccommodationID == x.a.AccommodationID).Select(i => new ImageAccommodationVm()
                    {
                        ImageAccommodationID = i.AccommodationID,
                        AccommodationID = x.a.AccommodationID,
                        Image = i.Image,
                        SortOrder = i.SortOrder
                    }).ToList(),
                    BookRooms = broom.Where(b => b.AccommodationID == x.a.AccommodationID).Select(br => new BookRoomVm()
                    {
                        BookRoomID = br.BookRoomID,
                        BookingDate = br.BookingDate,
                        AccommodationID = x.a.AccommodationID,
                        No = br.No,
                        Qty = br.Qty,
                        BookingUser = br.BookingUser,
                        FromDate = br.FromDate,
                        ToDate = br.ToDate,
                        CheckInIdentityCard = br.CheckInIdentityCard,
                        CheckInMail = br.CheckInMail,
                        CheckInName = br.CheckInName,
                        CheckInNote = br.CheckInNote,
                        Status = br.Status,
                        TotalPrice = br.TotalPrice
                    }).ToList()
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<AccommodationVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<AccommodationVm>>(pagedResult);
        }

        public async Task<ApiResult<AccommodationVm>> GetById(Guid id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            var accommodationtype = await _context.AccommodationTypes.FindAsync(accommodation.AccommodationTypeID);
            var location = await _context.Locations.FindAsync(accommodation.LocationID);
            if (accommodation == null)
            {
                return new ApiErrorResult<AccommodationVm>("Accommodation không tồn tại");
            }
            var broom = from br in _context.BookRooms select br;
            var utilities = from ul in _context.Utilities select ul;
            var rooms = from rm in _context.Rooms select rm;
            var imageAccommodations = from img in _context.ImageAccommodations select img;

            var accommodationVm = new AccommodationVm()
            {
                AccommodationID = accommodation.AccommodationID,
                Name = accommodation.Name,
                AbbreviationName = accommodation.AbbreviationName,
                Description = accommodation.Description,
                Email = accommodation.Email,
                Phone = accommodation.Phone ,
                MapURL = accommodation.MapURL,
                No = accommodation.No,
                Address = accommodation.Address,
                Status = accommodation.Status,
                LocationID = accommodation.LocationID,
                AccommodationTypeID = accommodation.AccommodationTypeID,
                AccommodationType = new AccommodationTypeVm()
                {
                    AccommodationTypeID = accommodationtype.AccommodationTypeID,
                    Name = accommodationtype.Name,
                    Description = accommodationtype.Description,
                    No = accommodationtype.No,
                },
                Location = new LocationVm()
                {
                    LocationID = location.LocationID,
                    Name = location.Name,
                    SortOrder = location.SortOrder,
                    Code = location.Code,
                    IsDeleted = location.IsDeleted,
                    ParentID = location.ParentID,
                    Type = location.Type
                },
                Utilities = utilities.Where(u => u.AccommodationID == accommodation.AccommodationID).Select(u => new UtilityVm()
                { 
                    UtilityID = u.UtilityID,
                    AccommodationID = u.AccommodationID,
                    UtilityType = u.UtilityType,
                    IsPrivate = u.IsPrivate
                }).ToList(),
                Rooms = rooms.Where(r => r.AccommodationID == accommodation.AccommodationID).Select(r => new RoomVm()
                {
                    RoomID = r.RoomID,
                    AccommodationID = accommodation.AccommodationID,
                    RoomTypeID = r.RoomTypeID,
                    Name = r.Name,
                    AvailableQty = r.AvailableQty,
                    Price = r.Price,
                    BookedQty = r.BookedQty,
                    PurchasedQty = r.PurchasedQty,
                    MaximumPeople = r.MaximumPeople,
                    No = r.No
                }).ToList(),
                ImageAccommodations = imageAccommodations.Where(i => i.AccommodationID == accommodation.AccommodationID).Select(i => new ImageAccommodationVm()
                {
                    ImageAccommodationID = i.AccommodationID,
                    AccommodationID = accommodation.AccommodationID,
                    Image = i.Image,
                    SortOrder = i.SortOrder
                }).ToList(),
                BookRooms = broom.Where(b => b.AccommodationID == accommodation.AccommodationID).Select(br => new BookRoomVm()
                {
                    BookRoomID = br.BookRoomID,
                    BookingDate = br.BookingDate,
                    AccommodationID = accommodation.AccommodationID,
                    No = br.No,
                    Qty = br.Qty,
                    BookingUser = br.BookingUser,
                    FromDate = br.FromDate,
                    ToDate = br.ToDate,
                    CheckInIdentityCard = br.CheckInIdentityCard,
                    CheckInMail = br.CheckInMail,
                    CheckInName = br.CheckInName,
                    CheckInNote = br.CheckInNote,
                    Status = br.Status,
                    TotalPrice = br.TotalPrice
                }).ToList()
            };
            return new ApiSuccessResult<AccommodationVm>(accommodationVm);
        }

        public async Task<ApiResult<bool>> CreateAccommodation(AccommodationCreateRequest request)
        {
            var accommodation = new Accommodation()
            {
                AccommodationID = request.AccommodationID,
                LocationID = request.LocationID,
                AccommodationTypeID = request.AccommodationTypeID,
                Name = request.Name,
                AbbreviationName = request.AbbreviationName,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone,
                Description = request.Description,
                MapURL = request.MapURL,
                No = request.No,
                Status = request.Status,
                imageAccommodations = request.Image.Select(i => new ImageAccommodation() {
                    ImageAccommodationID = i.Id,
                    Image = i.Image,
                    SortOrder = 1,
                }).ToList(),
            };
            _context.Accommodations.Add(accommodation);
            var result = await _context.SaveChangesAsync();
            if(result == 0)
            {
            return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }
    }
}
