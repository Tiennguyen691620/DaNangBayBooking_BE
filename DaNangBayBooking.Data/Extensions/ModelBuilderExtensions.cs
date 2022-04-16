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
            var LocationID = new Guid("f4f9a364-599c-11ec-ab77-0639800004fa");
            modelBuilder.Entity<Location>().HasData(new Location
            {
                LocationID = LocationID,
                IsDeleted = false,
                Name = "Tỉnh Sóc Trăng",
                Type = "Province",
                ParentID = new Guid("0c0103f5-792f-11ec-8f95-0639800004fa"),
                SortOrder = 51,
                Code = "SM97",
            });

            //any guid
            var roleID1 = new Guid("3FBC6C82-5EA2-47C8-BC7C-0D9ED0281045");
            var roleID2 = new Guid("1A31C9DF-861D-4E53-B076-C3081E1C2666");
            var roleAdmin = new Guid("06FDB157-C52F-4E71-ADF5-0F08BB0AF468");
            var roleClientID = new Guid("4D4F5B12-BC9A-46B1-BA0B-07CEA34E35F8");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleID1,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            },
            new AppRole
            {
                Id = roleID2,
                Name = "Client",
                NormalizedName = "Client",
                Description = "Cliener role"
            }
            );


            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = roleAdmin,
                AppRoleID = roleID1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tiennguyen691620@gmail.com",
                NormalizedEmail = "tiennguyen691620@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "0889161328Tn@"),
                SecurityStamp = string.Empty,
                FullName = "Nguyễn Tân Tiến",
                Dob = new DateTime(2000, 01, 06),
                PhoneNumber = "0889161328",
                IdentityCard= "241777698",
                Gender = "Nam",
                Address = "100, Âu Cơ",
                No = "DNB-22-00001",
                LocationID = LocationID,
                ActiveDate = DateTime.Now,
                Status = Enums.Status.Active,
            },
            new AppUser
            {
                Id = roleClientID,
                AppRoleID = roleID2,
                UserName = "tiennguyen",
                NormalizedUserName = "tiennguyen",
                Email = "tiennguyen3129@gmail.com",
                NormalizedEmail = "tiennguyen3129@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "0889161328Tn@"),
                SecurityStamp = string.Empty,
                FullName = "Nguyễn Tân Tiến",
                Dob = new DateTime(2000, 01, 06),
                PhoneNumber = "0889161329",
                IdentityCard = "241777698",
                Gender = "Nam",
                Address = "100, Âu Cơ",
                No = "DNB-22-00002",
                LocationID = LocationID,
                ActiveDate = DateTime.Now,
                Status = Enums.Status.Active,
            }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleID1,
                UserId = roleAdmin
            },
            new IdentityUserRole<Guid>
            {
                RoleId = roleID2,
                UserId = roleClientID
            }
            );

            
        }
    }
}
