using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DaNangBayBooking.Data.Configurations
{
    public class UtilityConfiguration : IEntityTypeConfiguration<Utility>
    {
        public void Configure(EntityTypeBuilder<Utility> builder)
        {
            builder.ToTable("Utilities");

            builder.HasKey(x => x.UtilityID);

            builder.Property(x => x.UtilityType).HasMaxLength(200).IsRequired();
            builder.Property(x => x.IsPrivate);

            builder.HasOne(x => x.Accommodation).WithMany(x => x.Utilities).HasForeignKey(x => x.AccommodationID);
        }
    }
}
