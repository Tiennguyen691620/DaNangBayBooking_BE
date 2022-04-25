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
