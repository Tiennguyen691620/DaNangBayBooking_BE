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
            builder.Property(x => x.Gender).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Avatar);

            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUsers).HasForeignKey(x => x.AppRoleID);
            builder.HasOne(x => x.Ward).WithMany(x => x.AppUsers).HasForeignKey(x => x.WardID);

        }
    }
}
