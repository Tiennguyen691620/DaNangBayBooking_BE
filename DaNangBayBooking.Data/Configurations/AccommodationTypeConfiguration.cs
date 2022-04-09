using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Configurations
{
    public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
    {
        public void Configure(EntityTypeBuilder<AccommodationType> builder)
        {
            builder.ToTable("AccommodationTypes");

            builder.HasKey(x => x.AccommodationTypeID);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.No).HasMaxLength(200);

        }
    }
}
