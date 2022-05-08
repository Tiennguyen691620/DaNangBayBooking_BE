using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(x => x.RoomID);

            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.AvailableQty).HasMaxLength(200);
            builder.Property(x => x.BookedQty).HasMaxLength(int.MaxValue);
            builder.Property(x => x.PurchasedQty).HasMaxLength(200);
            builder.Property(x => x.MaximumPeople).HasMaxLength(200);
            builder.Property(x => x.Price).HasMaxLength(int.MaxValue);

            builder.HasOne(x => x.Accommodation).WithMany(x => x.Rooms).HasForeignKey(x => x.AccommodationID);
            builder.HasOne(x => x.RoomType).WithMany(x => x.Rooms).HasForeignKey(x => x.RoomTypeID);
        }
    }
}
