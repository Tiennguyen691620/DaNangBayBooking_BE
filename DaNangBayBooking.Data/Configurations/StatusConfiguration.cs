using DaNangBayBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");

            builder.HasKey(x => x.Key);

            builder.Property(x => x.Value).HasMaxLength(int.MaxValue).IsRequired();
            builder.Property(x => x.DisplayText).HasMaxLength(int.MaxValue).IsRequired();
            builder.Property(x => x.Group).HasMaxLength(int.MaxValue).IsRequired();
        }
    }
}
