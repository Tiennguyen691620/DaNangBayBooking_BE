using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class UpdateRequest
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        //public string UserName { get; set; }

        public string Email { get; set; }

        public long Dob { get; set; }

        public string Address { get; set; }

        public string IdentityCard { get; set; }

        public bool Gender { get; set; }

        public string Avatar { get; set; }

        public string No { get; set; }

        public long? ActiveDate { get; set; }

        public bool Status { get; set; }

        public RoleVm Role { get; set; }

        public LocationProvince Province { get; set; }

        public LocationDistrict District { get; set; }

        public LocationSubDistrict SubDistrict { get; set; }
    }
}
