using DaNangBayBooking.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.RateComment
{
    public class CreateRateCommentRequest
    {
        //public Guid? RateCommentId { get; set; }

        public Guid BookRoomId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long Rating { get; set; }
    }
}
