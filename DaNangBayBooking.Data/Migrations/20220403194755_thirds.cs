using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class thirds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"));

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "WardID",
                keyValue: new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictID",
                keyValue: new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceID",
                keyValue: new Guid("9385925d-40c0-4a6b-8e30-3c516ff4c0ff"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "2e245b60-40b5-43da-a6e4-d1fffb7e18bf");

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceID", "Name", "No", "SortOrder" },
                values: new object[] { new Guid("8a0f40a0-5fff-4afa-b878-0eb7c43bdd59"), "Thành phố Đà Nẵng", "48", 32 });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictID", "Name", "No", "ProvinceID", "SortOrder" },
                values: new object[] { new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"), "Quận Liên Chiểu", "490", new Guid("8a0f40a0-5fff-4afa-b878-0eb7c43bdd59"), 360 });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "WardID", "DistrictID", "Name", "No", "SortOrder" },
                values: new object[] { new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c"), new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"), "Phường Hòa Hiệp Bắc", "20194", 1 });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AppRoleID", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "Gender", "IdentityCard", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "WardID" },
                values: new object[] { new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"), 0, "100, Âu Cơ", new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"), "f7395dde-58dd-42d2-8704-03c8b843fb29", new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiennguyen691620@gmail.com", true, "Nguyễn Tân Tiến", "Nam", "241777698", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "tiennguyen691620@gmail.com", "admin", "AQAAAAEAACcQAAAAEDFoRE59wJSb/f6KTLd1q+UUP/noyXvrTSQiVwS5ErQgMed/mPouQJfEiBVvWPivKA==", null, false, "", 1, false, "admin", new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"));

            migrationBuilder.DeleteData(
                table: "Wards",
                keyColumn: "WardID",
                keyValue: new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "DistrictID",
                keyValue: new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceID",
                keyValue: new Guid("8a0f40a0-5fff-4afa-b878-0eb7c43bdd59"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "e386328d-b95f-4b80-8292-711129e77695");

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceID", "Name", "No", "SortOrder" },
                values: new object[] { new Guid("9385925d-40c0-4a6b-8e30-3c516ff4c0ff"), "Thành phố Đà Nẵng", "48", 32 });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictID", "Name", "No", "ProvinceID", "SortOrder" },
                values: new object[] { new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"), "Quận Liên Chiểu", "490", new Guid("9385925d-40c0-4a6b-8e30-3c516ff4c0ff"), 360 });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "WardID", "DistrictID", "Name", "No", "SortOrder" },
                values: new object[] { new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c"), new Guid("6bff281c-0fc4-4635-9a46-6fb6f34c6732"), "Phường Hòa Hiệp Bắc", "20194", 1 });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AppRoleID", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "Gender", "IdentityCard", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "WardID" },
                values: new object[] { new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"), 0, "100, Âu Cơ", new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"), "cf290b2b-1145-4b57-9feb-36c43437d683", new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiennguyen691620@gmail.com", true, "Nguyễn Tân Tiến", "Nam", "241777698", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "tiennguyen691620@gmail.com", "admin", "AQAAAAEAACcQAAAAEMspfj/fJB83RRggqRxSp7X+QOscX8MI+ZGQSpJ5fYIxXBJmnjPPlrr4n+MFPJbs6g==", null, false, "", 1, false, "admin", new Guid("ad4a9655-2853-48c1-bc51-cfc722accb3c") });
        }
    }
}
