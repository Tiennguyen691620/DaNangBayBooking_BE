﻿// <auto-generated />
using System;
using DaNangBayBooking.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DaNangBayBooking.Data.Migrations
{
    [DbContext(typeof(DaNangDbContext))]
    [Migration("20220531031659_eight")]
    partial class eight
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Accommodation", b =>
                {
                    b.Property<Guid>("AccommodationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AbbreviationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("AccommodationTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MapURL")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AccommodationID");

                    b.HasIndex("AccommodationTypeID");

                    b.HasIndex("LocationID");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.AccommodationType", b =>
                {
                    b.Property<Guid>("AccommodationTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("AccommodationTypeID");

                    b.ToTable("AccommodationTypes");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("key");

                    b.ToTable("AppConfigs");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                            ConcurrencyStamp = "f557d543-5ae4-4b09-9d7b-cc243c73f78f",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        },
                        new
                        {
                            Id = new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                            ConcurrencyStamp = "efb65949-81c2-410c-990a-ac9750c91a59",
                            Description = "Cliener role",
                            Name = "Client",
                            NormalizedName = "Client"
                        });
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ActiveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AppRoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("Gender")
                        .HasColumnType("bit")
                        .HasMaxLength(10);

                    b.Property<string>("IdentityCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("LocationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("No")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppRoleID");

                    b.HasIndex("LocationID");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.BookRoom", b =>
                {
                    b.Property<Guid>("BookRoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccommodationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CheckInIdentityCard")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CheckInMail")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CheckInName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CheckInNote")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("CheckInPhoneNumber")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("No")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalDay")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("bookingUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookRoomID");

                    b.HasIndex("AccommodationID");

                    b.HasIndex("UserID");

                    b.ToTable("BookRooms");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.BookRoomDetail", b =>
                {
                    b.Property<Guid>("BookRoomDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookRoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CancelReason")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<int>("ChildNumber")
                        .HasColumnType("int");

                    b.Property<int>("PersonNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("BookRoomDetailID");

                    b.HasIndex("BookRoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("BookRoomDetails");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.ImageAccommodation", b =>
                {
                    b.Property<Guid>("ImageAccommodationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccommodationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("ImageAccommodationID");

                    b.HasIndex("AccommodationID");

                    b.ToTable("ImageAccommodations");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.ImageRoom", b =>
                {
                    b.Property<Guid>("ImageRoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("ImageRoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("ImageRooms");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Location", b =>
                {
                    b.Property<Guid>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("ParentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("LocationID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.RateComment", b =>
                {
                    b.Property<Guid>("RateCommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookRoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("RateCommentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RateCommentID");

                    b.HasIndex("BookRoomID")
                        .IsUnique();

                    b.ToTable("RateComments");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccommodationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableQty")
                        .HasColumnType("int")
                        .HasMaxLength(200);

                    b.Property<int>("BookedQty")
                        .HasColumnType("int")
                        .HasMaxLength(2147483647);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumPeople")
                        .HasColumnType("int")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasMaxLength(2147483647);

                    b.Property<int>("PurchasedQty")
                        .HasColumnType("int")
                        .HasMaxLength(200);

                    b.Property<Guid>("RoomTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoomID");

                    b.HasIndex("AccommodationID");

                    b.HasIndex("RoomTypeID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.RoomType", b =>
                {
                    b.Property<Guid>("RoomTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("RoomTypeID");

                    b.ToTable("Roomtype");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Status", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(2147483647);

                    b.HasKey("Key");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Utility", b =>
                {
                    b.Property<Guid>("UtilityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccommodationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("UtilityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("UtilityID");

                    b.HasIndex("AccommodationID");

                    b.ToTable("Utilities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                            RoleId = new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045")
                        },
                        new
                        {
                            UserId = new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"),
                            RoleId = new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Accommodation", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.AccommodationType", "AccommodationType")
                        .WithMany("Accommodations")
                        .HasForeignKey("AccommodationTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DaNangBayBooking.Data.Entities.Location", "Location")
                        .WithMany("Accommodations")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.AppUser", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.AppRole", "AppRole")
                        .WithMany("AppUsers")
                        .HasForeignKey("AppRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DaNangBayBooking.Data.Entities.Location", "Location")
                        .WithMany("AppUsers")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.BookRoom", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.Accommodation", "Accommodation")
                        .WithMany("BookRooms")
                        .HasForeignKey("AccommodationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DaNangBayBooking.Data.Entities.AppUser", "AppUser")
                        .WithMany("BookRooms")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.BookRoomDetail", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.BookRoom", "BookRoom")
                        .WithMany("BookRoomDetails")
                        .HasForeignKey("BookRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DaNangBayBooking.Data.Entities.Room", "Room")
                        .WithMany("BookRoomDetails")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.ImageAccommodation", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.Accommodation", "Accommodation")
                        .WithMany("imageAccommodations")
                        .HasForeignKey("AccommodationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.ImageRoom", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.Room", "Room")
                        .WithMany("ImageRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.RateComment", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.BookRoom", "BookRoom")
                        .WithOne("RateComments")
                        .HasForeignKey("DaNangBayBooking.Data.Entities.RateComment", "BookRoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Room", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.Accommodation", "Accommodation")
                        .WithMany("Rooms")
                        .HasForeignKey("AccommodationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DaNangBayBooking.Data.Entities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaNangBayBooking.Data.Entities.Utility", b =>
                {
                    b.HasOne("DaNangBayBooking.Data.Entities.Accommodation", "Accommodation")
                        .WithMany("Utilities")
                        .HasForeignKey("AccommodationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
