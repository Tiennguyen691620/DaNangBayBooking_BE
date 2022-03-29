using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class BookRoomDetailConfiguration : IEntityTypeConfiguration<BookRoomDetail>
    {
        public void Configure(EntityTypeBuilder<BookRoomDetail> builder)
        {
            builder.ToTable("BookRoomDetails");

            builder.HasKey(x => x.BookRoomDetailID);

            builder.Property(x => x.CancelReason).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CancelDate).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ChildNumber).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PersonNumber).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Status);

            builder.HasOne(x => x.BookRoom).WithMany(x => x.BookRoomDetails).HasForeignKey(x => x.BookRoomID);
            builder.HasOne(x => x.Room).WithOne(x => x.BookRoomDetails).HasForeignKey<BookRoomDetail>(x => x.RoomID).OnDelete(DeleteBehavior.ClientCascade);
            
        }
    }
}
