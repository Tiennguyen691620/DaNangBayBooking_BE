using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Dob).IsRequired();
            builder.Property(x => x.IdentityCard).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Gender);
            builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Avatar);

            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUsers).HasForeignKey(x => x.AppRoleID);
            builder.HasOne(x => x.Location).WithMany(x => x.AppUsers).HasForeignKey(x => x.LocationID);

        }
    }
}
