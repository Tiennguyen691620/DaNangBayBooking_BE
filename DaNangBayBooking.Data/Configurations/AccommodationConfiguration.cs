using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.ToTable("Accommodations");

            builder.HasKey(x => x.AccommodationID);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.AbbreviationName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(int.MaxValue);
            builder.Property(x => x.MapURL).HasMaxLength(int.MaxValue);
            builder.Property(x => x.No).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Status);

            builder.HasOne(x => x.Location).WithMany(x => x.Accommodations).HasForeignKey(x => x.LocationID);
            builder.HasOne(x => x.AccommodationType).WithMany(x => x.Accommodations).HasForeignKey(x => x.AccommodationTypeID);

        }
    }
}
