using DaNangBayBooking.ViewModels.Catalog.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class RegisterRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public long Dob { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Display(Name = "CMND/CCCD")]
        public string IdentityCard { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //public string Avatar { get; set; }

        public string No { get; set; }

        //public string Address { get; set; }

        public long ActiveDate { get; set; }

        public bool Status { get; set; }

        public LocationProvince Province { get; set; }

        public LocationDistrict District { get; set; }

        public LocationSubDistrict SubDistrict { get; set; }

    }
}
