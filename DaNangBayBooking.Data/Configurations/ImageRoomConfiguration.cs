using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class ImageRoomConfiguration : IEntityTypeConfiguration<ImageRoom>
    {
        public void Configure(EntityTypeBuilder<ImageRoom> builder)
        {
            builder.ToTable("ImageRooms");

            builder.HasKey(x => x.ImageRoomID);

            builder.Property(x => x.Image);
            builder.Property(x => x.SortOrder);

            builder.HasOne(x => x.Room).WithMany(x => x.ImageRooms).HasForeignKey(x => x.RoomID);
        }
    }
}
