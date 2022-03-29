using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaNangBayBooking.Data.Configurations
{
    public class RateCommentConfiguration : IEntityTypeConfiguration<RateComment>
    {
        public void Configure(EntityTypeBuilder<RateComment> builder)
        {
            builder.ToTable("RateComments");

            builder.HasKey(x => x.RateCommentID);

            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.RateCommentDate).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Rating).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.BookRoom).WithOne(x => x.RateComments).HasForeignKey<RateComment>(x => x.BookRoomID);
        }
    }
}
