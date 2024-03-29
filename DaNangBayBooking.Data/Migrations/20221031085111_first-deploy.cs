﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class firstdeploy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationTypes",
                columns: table => new
                {
                    AccommodationTypeID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    No = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationTypes", x => x.AccommodationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    key = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Roomtype",
                columns: table => new
                {
                    RoomTypeID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2147483647, nullable: true),
                    No = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roomtype", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(maxLength: 2147483647, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 2147483647, nullable: false),
                    Group = table.Column<string>(maxLength: 2147483647, nullable: false),
                    Order = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationID = table.Column<Guid>(nullable: false),
                    LocationID = table.Column<Guid>(nullable: false),
                    AccommodationTypeID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AbbreviationName = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Phone = table.Column<string>(maxLength: 200, nullable: false),
                    MapURL = table.Column<string>(maxLength: 2147483647, nullable: true),
                    No = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationID);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccommodationTypes_AccommodationTypeID",
                        column: x => x.AccommodationTypeID,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accommodations_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    AppRoleID = table.Column<Guid>(nullable: false),
                    LocationID = table.Column<Guid>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IdentityCard = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    No = table.Column<string>(nullable: true),
                    ActiveDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppRoles_AppRoleID",
                        column: x => x.AppRoleID,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageAccommodations",
                columns: table => new
                {
                    ImageAccommodationID = table.Column<Guid>(nullable: false),
                    AccommodationID = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAccommodations", x => x.ImageAccommodationID);
                    table.ForeignKey(
                        name: "FK_ImageAccommodations_Accommodations_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<Guid>(nullable: false),
                    AccommodationID = table.Column<Guid>(nullable: false),
                    RoomTypeID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    AvailableQty = table.Column<int>(maxLength: 200, nullable: false),
                    PurchasedQty = table.Column<int>(maxLength: 200, nullable: false),
                    MaximumPeople = table.Column<int>(maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(maxLength: 2147483647, nullable: false),
                    BookedQty = table.Column<int>(maxLength: 2147483647, nullable: false),
                    No = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Accommodations_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_Roomtype_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "Roomtype",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilities",
                columns: table => new
                {
                    UtilityID = table.Column<Guid>(nullable: false),
                    AccommodationID = table.Column<Guid>(nullable: false),
                    UtilityType = table.Column<string>(maxLength: 200, nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilities", x => x.UtilityID);
                    table.ForeignKey(
                        name: "FK_Utilities_Accommodations_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRooms",
                columns: table => new
                {
                    BookRoomID = table.Column<Guid>(nullable: false),
                    AccommodationID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    No = table.Column<string>(maxLength: 200, nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    TotalDay = table.Column<int>(nullable: false),
                    CheckInName = table.Column<string>(maxLength: 200, nullable: true),
                    CheckInMail = table.Column<string>(maxLength: 200, nullable: true),
                    CheckInNote = table.Column<string>(maxLength: 200, nullable: true),
                    CheckInIdentityCard = table.Column<string>(maxLength: 200, nullable: true),
                    CheckInPhoneNumber = table.Column<string>(maxLength: 200, nullable: true),
                    BookingUser = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRooms", x => x.BookRoomID);
                    table.ForeignKey(
                        name: "FK_BookRooms_Accommodations_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRooms_AppUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageRooms",
                columns: table => new
                {
                    ImageRoomID = table.Column<Guid>(nullable: false),
                    RoomID = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageRooms", x => x.ImageRoomID);
                    table.ForeignKey(
                        name: "FK_ImageRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRoomDetails",
                columns: table => new
                {
                    BookRoomDetailID = table.Column<Guid>(nullable: false),
                    RoomID = table.Column<Guid>(nullable: false),
                    BookRoomID = table.Column<Guid>(nullable: false),
                    ChildNumber = table.Column<int>(nullable: false),
                    CancelDate = table.Column<DateTime>(nullable: true),
                    CancelReason = table.Column<string>(maxLength: 2147483647, nullable: true),
                    PersonNumber = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRoomDetails", x => x.BookRoomDetailID);
                    table.ForeignKey(
                        name: "FK_BookRoomDetails_BookRooms_BookRoomID",
                        column: x => x.BookRoomID,
                        principalTable: "BookRooms",
                        principalColumn: "BookRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRoomDetails_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateComments",
                columns: table => new
                {
                    RateCommentID = table.Column<Guid>(nullable: false),
                    BookRoomID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Rating = table.Column<long>(nullable: false),
                    RateCommentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateComments", x => x.RateCommentID);
                    table.ForeignKey(
                        name: "FK_RateComments_BookRooms_BookRoomID",
                        column: x => x.BookRoomID,
                        principalTable: "BookRooms",
                        principalColumn: "BookRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"), "56a1bb58-74be-436f-badf-c1742ed02ca6", "Administrator role", "admin", "admin" },
                    { new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"), "15cd89f1-2c6c-4d59-a59e-141c0b2c4611", "Cliener role", "Client", "Client" }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"), new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045") },
                    { new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"), new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AccommodationTypeID",
                table: "Accommodations",
                column: "AccommodationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_LocationID",
                table: "Accommodations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppRoleID",
                table: "AppUsers",
                column: "AppRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_LocationID",
                table: "AppUsers",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomDetails_BookRoomID",
                table: "BookRoomDetails",
                column: "BookRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomDetails_RoomID",
                table: "BookRoomDetails",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRooms_AccommodationID",
                table: "BookRooms",
                column: "AccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRooms_UserID",
                table: "BookRooms",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAccommodations_AccommodationID",
                table: "ImageAccommodations",
                column: "AccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageRooms_RoomID",
                table: "ImageRooms",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_RateComments_BookRoomID",
                table: "RateComments",
                column: "BookRoomID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AccommodationID",
                table: "Rooms",
                column: "AccommodationID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Utilities_AccommodationID",
                table: "Utilities",
                column: "AccommodationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "BookRoomDetails");

            migrationBuilder.DropTable(
                name: "ImageAccommodations");

            migrationBuilder.DropTable(
                name: "ImageRooms");

            migrationBuilder.DropTable(
                name: "RateComments");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Utilities");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "BookRooms");

            migrationBuilder.DropTable(
                name: "Roomtype");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AccommodationTypes");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
