﻿using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.Accommodation;
using DaNangBayBooking.ViewModels.Catalog.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.BookRoom
{
    public class BookRoomCreateRequest
    {
        //public Guid BookRoomID { get; set; }

        public string No { get; set; }

        public int Qty { get; set; }

        public DateTime BookingDate { get; set; }

        public string BookingUser { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CheckInName { get; set; }

        public string CheckInMail { get; set; }

        public string CheckInNote { get; set; }

        public string CheckInIdentityCard { get; set; }

        public string CheckInPhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public int ChildNumber { get; set; }

        public int PersonNumber { get; set; }

        public AccommodationVm Accommodation { get; set; }

        public List<RoomVm> Room { get; set; }

        public BookingStatus Status { get; set; }
    }
}