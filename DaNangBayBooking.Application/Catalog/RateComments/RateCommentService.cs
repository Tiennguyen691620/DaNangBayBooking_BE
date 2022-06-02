using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Data.Entities;
using DaNangBayBooking.Utilities.Extensions;
using DaNangBayBooking.ViewModels.Catalog.RateComment;
using DaNangBayBooking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.RateComments
{
    public class RateCommentService : IRateCommentService
    {
        private readonly DaNangDbContext _context;
        public RateCommentService(
            DaNangDbContext context
            )
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateRateComment(CreateRateCommentRequest request)
        {
            var comment = new RateComment()
            {
                BookRoomID = request.BookRoomId,
                Description = request.Description,
                RateCommentDate = DateTime.Now,
                Title = request.Title,
                Rating = request.Rating,
            };
            _context.RateComments.Add(comment);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiSuccessResult<bool>(false);
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<RateCommentVm>>> GetAllRateComment(GetAllRateCommentRequest request)
        {
            var query = from rc in _context.RateComments
                        join br in _context.BookRooms on rc.BookRoomID equals br.BookRoomID
                        join ac in _context.Accommodations on br.AccommodationID equals ac.AccommodationID
                        join brd in _context.BookRoomDetails on br.BookRoomID equals brd.BookRoomID
                        join r in _context.Rooms on brd.RoomID equals r.RoomID
                        join rt in _context.RoomTypes on r.RoomTypeID equals rt.RoomTypeID
                        join u in _context.AppUsers on br.UserID equals u.Id
                        where ac.AccommodationID == request.AccommodationId
                        select new { rc, br, ac, brd, r, rt, u };

            var result = await query.Select(x => new RateCommentVm()
            {
                RateCommentId = x.rc.RateCommentID,
                Description = x.rc.Description,
                RateCommentDate = x.rc.RateCommentDate.ToSecondsTimestamp(),
                Title = x.rc.Title,
                Rating = x.rc.Rating,
                FullName = x.u.FullName,
                AvatarUrl = x.u.Avatar,
                RoomName = x.r.Name,
                RoomTypeName = x.rt.Name,
                TotalDay = x.br.Qty,
                BookingDate = x.br.BookingDate.ToSecondsTimestamp(),
            }).ToListAsync();
            return new ApiSuccessResult<List<RateCommentVm>>(result);
        }

        public async Task<ApiResult<GetQtyRateComment>> GetQtyAndPontRateComment(Guid AccommodationId)
        {
            var query = await _context.Accommodations.FindAsync(AccommodationId);
            var bookRoom = from br in _context.BookRooms
                           join rc in _context.RateComments on br.BookRoomID equals rc.BookRoomID
                           where br.AccommodationID == query.AccommodationID
                           select new { br, rc};
            var count = bookRoom.Count() == 0 ? 1 : bookRoom.Count();
            var res = new GetQtyRateComment()
            {
                Qty = await bookRoom.CountAsync(),
                Point = (bookRoom.Sum(x => x.rc.Rating) / count),
            };
            return new ApiSuccessResult<GetQtyRateComment>(res);
        }
    }
}
