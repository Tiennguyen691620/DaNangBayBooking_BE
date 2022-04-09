using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "7eabacc7-8f8c-41f0-82c4-22292e9217ac");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"), "aedac7d5-8861-44ef-af69-e3da9fd0e74b", "Cliener role", "Client", "Client" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"), new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666") });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ActiveDate", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { new DateTime(2022, 4, 9, 13, 51, 41, 631, DateTimeKind.Local).AddTicks(7258), "8fd88555-d2db-4339-8735-302e9ce29b58", "AQAAAAEAACcQAAAAEEweHZq94KjzFIT9/oYf5EXI3xZ4MgobErzTgCyF42otvfRjdAAZmrWlyDV67LYoxQ==" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveDate", "Address", "AppRoleID", "Avatar", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "Gender", "IdentityCard", "LocationID", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"), 0, new DateTime(2022, 4, 9, 13, 51, 41, 640, DateTimeKind.Local).AddTicks(9923), "100, Âu Cơ", new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"), null, "28684c24-c026-4aca-b820-e5659658eca5", new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiennguyen3129@gmail.com", true, "Nguyễn Tân Tiến", "Nam", "241777698", new Guid("f4f9a364-599c-11ec-ab77-0639800004fa"), false, null, "tiennguyen3129@gmail.com", "tiennguyen", "AQAAAAEAACcQAAAAEPX1L1vOh8STUUMT+JTZfH55/L1NhjK+dyiQbgkZzUnoZgVX/vGhtLZ7nKG3y9acGw==", null, false, "", 1, false, "tiennguyen" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"), new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("4d4f5b12-bc9a-46b1-ba0b-07cea34e35f8"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "bc5fc421-77d9-4e94-8de6-a337e98eb9c0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ActiveDate", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { new DateTime(2022, 4, 9, 12, 4, 50, 157, DateTimeKind.Local).AddTicks(6252), "6bc99e80-b204-4071-812c-f435d74d5881", "AQAAAAEAACcQAAAAENxyClQYFwGpSZQxVtwMgz6XYtJWcHFebm4Z4fwjezKdKf5j46/SmY+rFh5YCI7Q/w==" });
        }
    }
}
