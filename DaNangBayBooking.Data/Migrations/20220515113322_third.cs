using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingUser",
                table: "BookRooms");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "BookRooms",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "CheckInPhoneNumber",
                table: "BookRooms",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalDay",
                table: "BookRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "3e4d6900-7618-4175-b87a-0da4d91cc499");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "bfa02d4b-5873-47a4-ae3b-daa4e3e9d3a8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInPhoneNumber",
                table: "BookRooms");

            migrationBuilder.DropColumn(
                name: "TotalDay",
                table: "BookRooms");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "BookRooms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingUser",
                table: "BookRooms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "a5761fb1-1df4-4c9d-8173-6622a5a96a46");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "9bb6b0bd-9174-48bd-a52f-638164a2c4df");
        }
    }
}
