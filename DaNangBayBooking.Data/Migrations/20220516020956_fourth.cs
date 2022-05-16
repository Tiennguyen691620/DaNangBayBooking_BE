using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CancelReason",
                table: "BookRoomDetails",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelDate",
                table: "BookRoomDetails",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "03bd2372-d84e-4516-9e34-370f6565ba46");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "9ae49dc0-b205-49e3-866a-d6ee27cb9beb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CancelReason",
                table: "BookRoomDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2147483647,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelDate",
                table: "BookRoomDetails",
                type: "datetime2",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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
    }
}
