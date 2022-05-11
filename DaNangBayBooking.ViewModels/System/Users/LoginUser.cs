using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class LoginUser
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Avatar { get; set; }

        public string AccessToken { get; set; }

        public string RoleName { get; set; }
    }
}
