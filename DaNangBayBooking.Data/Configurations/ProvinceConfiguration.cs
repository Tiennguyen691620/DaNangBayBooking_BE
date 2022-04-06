using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces");

            builder.HasKey(x => x.ProvinceID);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.No).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SortOrder);
        }
    }
}
