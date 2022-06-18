using DaNangBayBooking.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class RateComment
    {
        public Guid RateCommentID { get; set; }

        public Guid BookRoomID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long Rating { get; set; }

        public DateTime RateCommentDate { get; set; }

        public BookRoom BookRoom { get; set; }

    }
}
