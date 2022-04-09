using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class initial : Migration
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
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    No = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roomtype", x => x.RoomTypeID);
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
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Phone = table.Column<int>(maxLength: 200, nullable: false),
                    MapURL = table.Column<string>(maxLength: 200, nullable: false),
                    No = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false)
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
                    PhoneNumber = table.Column<string>(nullable: true),
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
                    Gender = table.Column<string>(maxLength: 10, nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    ActiveDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
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
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AvailableQty = table.Column<int>(maxLength: 200, nullable: false),
                    PurchasedQty = table.Column<int>(maxLength: 200, nullable: false),
                    MaximumPeople = table.Column<int>(maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(maxLength: 200, nullable: false),
                    BookedQty = table.Column<int>(maxLength: 200, nullable: false),
                    No = table.Column<string>(maxLength: 200, nullable: false)
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
                    Qty = table.Column<int>(maxLength: 200, nullable: false),
                    BookingDate = table.Column<DateTime>(maxLength: 200, nullable: false),
                    BookingUser = table.Column<string>(maxLength: 200, nullable: false),
                    FromDate = table.Column<DateTime>(maxLength: 200, nullable: false),
                    ToDate = table.Column<DateTime>(maxLength: 200, nullable: false),
                    CheckInName = table.Column<string>(maxLength: 200, nullable: false),
                    CheckInMail = table.Column<string>(maxLength: 200, nullable: false),
                    CheckInNote = table.Column<string>(maxLength: 200, nullable: false),
                    CheckInIdentityCard = table.Column<string>(maxLength: 200, nullable: false),
                    TotalPrice = table.Column<decimal>(maxLength: 200, nullable: false),
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
                    ChildNumber = table.Column<int>(maxLength: 200, nullable: false),
                    CancelDate = table.Column<DateTime>(maxLength: 200, nullable: false),
                    CancelReason = table.Column<string>(maxLength: 200, nullable: false),
                    PersonNumber = table.Column<int>(maxLength: 200, nullable: false),
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
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Rating = table.Column<string>(maxLength: 200, nullable: false),
                    RateCommentDate = table.Column<DateTime>(maxLength: 200, nullable: false)
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
                values: new object[] { new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"), "bc5fc421-77d9-4e94-8de6-a337e98eb9c0", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"), new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045") });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Code", "IsDeleted", "Name", "ParentID", "SortOrder", "Type" },
                values: new object[] { new Guid("f4f9a364-599c-11ec-ab77-0639800004fa"), "SM97", false, "Tỉnh Sóc Trăng", new Guid("0c0103f5-792f-11ec-8f95-0639800004fa"), 51, "Province" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveDate", "Address", "AppRoleID", "Avatar", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "Gender", "IdentityCard", "LocationID", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"), 0, new DateTime(2022, 4, 9, 12, 4, 50, 157, DateTimeKind.Local).AddTicks(6252), "100, Âu Cơ", new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"), null, "6bc99e80-b204-4071-812c-f435d74d5881", new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiennguyen691620@gmail.com", true, "Nguyễn Tân Tiến", "Nam", "241777698", new Guid("f4f9a364-599c-11ec-ab77-0639800004fa"), false, null, "tiennguyen691620@gmail.com", "admin", "AQAAAAEAACcQAAAAENxyClQYFwGpSZQxVtwMgz6XYtJWcHFebm4Z4fwjezKdKf5j46/SmY+rFh5YCI7Q/w==", null, false, "", 1, false, "admin" });

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
                column: "RoomID",
                unique: true);

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
