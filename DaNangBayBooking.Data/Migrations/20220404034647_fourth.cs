using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AppUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                table: "AppUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "bf9eb70f-49a4-448d-a51d-bf7bfad68c51");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ActiveDate", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { new DateTime(2022, 4, 4, 10, 46, 47, 18, DateTimeKind.Local).AddTicks(7415), "e59f8a46-49d4-4e67-9a47-0f09bfa50d6d", "AQAAAAEAACcQAAAAEGUv7HVLdljeg52wC7vPwmRgwnm57cP1sINZDj+zCcwWQJKWh8qHMyTNWklf/YQbvA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveDate",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AppUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "2e245b60-40b5-43da-a6e4-d1fffb7e18bf");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f7395dde-58dd-42d2-8704-03c8b843fb29", "AQAAAAEAACcQAAAAEDFoRE59wJSb/f6KTLd1q+UUP/noyXvrTSQiVwS5ErQgMed/mPouQJfEiBVvWPivKA==" });
        }
    }
}
