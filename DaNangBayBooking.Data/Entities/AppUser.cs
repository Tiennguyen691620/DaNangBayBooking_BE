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

        public DateTime Dob { get; set; }

        public string Address { get; set; }

        public DateTime LastLoginDate { get; set; }

        public Status Status  { get; set; }

        public AppRole AppRole { get; set; }

        public List<BookRoom> BookRooms { get; set; }

    }
}
