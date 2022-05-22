using DaNangBayBooking.Data.EF;
using DaNangBayBooking.Utilities.Extensions;
using DaNangBayBooking.ViewModels.Catalog.Report;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Reports
{
    public class ReportService : IReportService
    {

        private readonly DaNangDbContext _context;

        public ReportService(
            DaNangDbContext context
            )
        {
            _context = context;
        }

        public async Task<ApiResult<RevenueVm>> ReportRevenue(FilterRevenueRequest request)
        {
            var bookRoom = _context.BookRooms.Where(x => x.Status == Data.Enums.BookingStatus.Closed).ToList();
            /*var bookRoom = from br in _context.BookRooms
                           join brt in _context.BookRoomDetails on br.BookRoomID equals brt.BookRoomID
                           join r in _context.Rooms on brt.RoomID equals r.RoomID
                           select new { br, brt, r };*/

            if (request.AccommodationId != null)
            {
                bookRoom = bookRoom.Where(x => x.AccommodationID == request.AccommodationId).ToList();
            }

            var RevenueReprortViewByBookRoom = new List<RevenueReprortViewByBookRoom>();
            var RevenueReportViewByDate = new List<RevenueReportViewByDate>();

            var convertFromDate = request.FromDate.FromUnixTimeStamp();
            var convertToDate = request.ToDate.FromUnixTimeStamp();

            var countDay = (convertToDate - convertFromDate).Value.Days;
            var Date = DateTime.Parse(convertFromDate.Value.ToShortDateString());
            var toDate = DateTime.Parse(Date.ToShortDateString());
            for (var i = 0; i <= countDay; i++) {
                toDate = toDate.AddDays(1);
                var bookRooms = bookRoom.Where(x => x.BookingDate >= Date && x.BookingDate < toDate);
                var ViewByDate = new RevenueReportViewByDate() {
                    Date = Date.ToSecondsTimestamp(),
                    Amount = bookRooms.Count(),
                };
                ViewByDate.Childs = new List<RevenueReprortViewByBookRoom>();
                foreach(var item in bookRooms)
                {
                    /*var bookRoomDetail = _context.BookRoomDetails.FirstOrDefault(x => x.BookRoomID == item.BookRoomID);*/
                    var accommodation = await _context.Accommodations.FindAsync(item.AccommodationID);
                    var childs = new RevenueReprortViewByBookRoom() {
                        ObjectId = item.BookRoomID,
                        Name = accommodation.Name,
                        Amount = item.TotalPrice,
                        Qty = 1,
                    };
                    ViewByDate.Childs.Add(childs);
                    RevenueReprortViewByBookRoom.Add(childs);
                }
                RevenueReportViewByDate.Add(ViewByDate);
                Date = Date.AddDays(1);
            }
            return new ApiSuccessResult<RevenueVm> (new RevenueVm()
            {
                ViewByAccommodation = RevenueReprortViewByBookRoom,
                ViewByDate = RevenueReportViewByDate
            });

        }
    }
}
