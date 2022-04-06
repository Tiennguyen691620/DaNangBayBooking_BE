using DaNangBayBooking.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var ProvinceID = new Guid("8A0F40A0-5FFF-4AFA-B878-0EB7C43BDD59");
            var DistrictID = new Guid("6BFF281C-0FC4-4635-9A46-6FB6F34C6732");
            var WardID = new Guid("AD4A9655-2853-48C1-BC51-CFC722ACCB3C");
            modelBuilder.Entity<Ward>().HasData(new Ward
            {
                WardID = WardID,
                Name = "Phường Hòa Hiệp Bắc",
                No = "20194",
                SortOrder = 1,
                DistrictID = DistrictID,
            });
            modelBuilder.Entity<District>().HasData(new District
            {
                DistrictID = DistrictID,
                Name = "Quận Liên Chiểu",
                No = "490",
                SortOrder = 360,
                ProvinceID = ProvinceID,
            });
            modelBuilder.Entity<Province>().HasData(new Province
            {
                ProvinceID = ProvinceID,
                Name = "Thành phố Đà Nẵng",
                No = "48",
                SortOrder = 32,
            });

            //any guid
            var roleID = new Guid("3FBC6C82-5EA2-47C8-BC7C-0D9ED0281045");
            var roleAdmin = new Guid("06FDB157-C52F-4E71-ADF5-0F08BB0AF468");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleID,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = roleAdmin,
                AppRoleID = roleID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tiennguyen691620@gmail.com",
                NormalizedEmail = "tiennguyen691620@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "0889161328Tn@"),
                SecurityStamp = string.Empty,
                FullName = "Nguyễn Tân Tiến",
                Dob = new DateTime(2000, 01, 06),
                IdentityCard= "241777698",
                Gender = "Nam",
                Address = "100, Âu Cơ",
                WardID = WardID,
                ActiveDate = DateTime.Now,
                Status = Enums.Status.Active,
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleID,
                UserId = roleAdmin
            });

            
        }
    }
}
