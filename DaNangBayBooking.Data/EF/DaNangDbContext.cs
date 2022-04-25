using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DaNangBayBooking.Data.Configurations;
using DaNangBayBooking.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DaNangBayBooking.Data.Extensions;

namespace DaNangBayBooking.Data.EF
{
    public class DaNangDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DaNangDbContext( DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccommodationConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BookRoomConfiguration());
            modelBuilder.ApplyConfiguration(new BookRoomDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ImageAccommodationConfiguration());
            modelBuilder.ApplyConfiguration(new ImageRoomConfiguration());
            modelBuilder.ApplyConfiguration(new RateCommentConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UtilityConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);

        }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<BookRoom> BookRooms { get; set; }
        public DbSet<BookRoomDetail> BookRoomDetails { get; set; }
        public DbSet<ImageAccommodation> ImageAccommodations { get; set; }
        public DbSet<ImageRoom> ImageRooms { get; set; }
        public DbSet<RateComment> RateComments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
