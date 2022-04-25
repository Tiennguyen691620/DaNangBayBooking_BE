using DaNangBayBooking.Data.Enums;
using DaNangBayBooking.ViewModels.Catalog.Locations;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class CreateAdminRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ngày sinh")]
        public long Dob { get; set; }
        //public DateTime Dob { get; set; }

        public string Address { get; set; }

        public string IdentityCard { get; set; }

        public bool Gender { get; set; }

        public string Avatar { get; set; }

        public string No { get; set; }

        public long ActiveDate { get; set; }

        public bool Status { get; set; }

        public LocationProvince Province { get; set; }

        public LocationDistrict District { get; set; }

        public LocationSubDistrict SubDistrict { get; set; }

    }
}
