using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class BookRoomConfiguration : IEntityTypeConfiguration<BookRoom>
    {
        public void Configure(EntityTypeBuilder<BookRoom> builder)
        {
            builder.ToTable("BookRooms");

            builder.HasKey(x => x.BookRoomID);

            builder.Property(x => x.BookingDate).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CheckInName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CheckInMail).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CheckInIdentityCard).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CheckInNote).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CheckInPhoneNumber).HasMaxLength(200).IsRequired();
            builder.Property(x => x.FromDate).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ToDate).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Qty).HasMaxLength(200).IsRequired();
            builder.Property(x => x.TotalPrice).HasMaxLength(200).IsRequired();
            builder.Property(x => x.No).HasMaxLength(200);
            builder.Property(x => x.Status);

            builder.HasOne(x => x.Accommodation).WithMany(x => x.BookRooms).HasForeignKey(x => x.AccommodationID);
            builder.HasOne(x => x.AppUser).WithMany(x => x.BookRooms).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
