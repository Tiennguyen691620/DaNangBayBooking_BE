using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRoomDetails_RoomID",
                table: "BookRoomDetails");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "14d957c0-2b41-4800-9b40-704e61297e04");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "071232b2-acac-486c-9e78-e61e0fc0a640");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomDetails_RoomID",
                table: "BookRoomDetails",
                column: "RoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRoomDetails_RoomID",
                table: "BookRoomDetails");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "d8356d6a-800e-4f2b-8c16-0ae8859ff25b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "b25631da-c185-4b37-ad81-cea75d233d5f");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomDetails_RoomID",
                table: "BookRoomDetails",
                column: "RoomID",
                unique: true);
        }
    }
}
