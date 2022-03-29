using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class ImageAccommodationConfiguration : IEntityTypeConfiguration<ImageAccommodation>
    {
        public void Configure(EntityTypeBuilder<ImageAccommodation> builder)
        {
            builder.ToTable("ImageAccommodations");

            builder.HasKey(x => x.ImageAccommodationID);

            builder.Property(x => x.Image);
            builder.Property(x => x.SortOrder);

            builder.HasOne(x => x.Accommodation).WithMany(x => x.imageAccommodations).HasForeignKey(x => x.AccommodationID);
        }
    }
}
