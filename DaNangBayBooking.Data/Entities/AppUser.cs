using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace DaNangBayBooking.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public Guid AppRoleID { get; set; }

        public Guid LocationID { get; set; }

        public DateTime Dob { get; set; }

        public string Address { get; set; }

        public string IdentityCard { get; set; }

        public bool Gender { get; set; }

        public string Avatar { get; set; }

        public string No { get; set; }

        public DateTime ActiveDate { get; set; }

        public Status Status  { get; set; }

        public AppRole AppRole { get; set; }

        public Location Location { get; set; }

        public List<BookRoom> BookRooms { get; set; }

    }
}
