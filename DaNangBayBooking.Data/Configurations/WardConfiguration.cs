using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class WardConfiguration : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.ToTable("Wards");

            builder.HasKey(x => x.WardID);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.No).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.District).WithMany(x => x.Wards).HasForeignKey(x => x.DistrictID);
        }
    }
}
