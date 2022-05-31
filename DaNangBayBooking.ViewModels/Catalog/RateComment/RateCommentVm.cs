using DaNangBayBooking.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.RateComment
{
    public class RateCommentVm
    {
        public Guid RateCommentId { get; set; }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public string RoomName { get; set; }

        public string RoomTypeName { get; set; }

        public int TotalDay { get; set; }

        public long? BookingDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long Rating { get; set; }

        public long? RateCommentDate { get; set; }
    }
}
