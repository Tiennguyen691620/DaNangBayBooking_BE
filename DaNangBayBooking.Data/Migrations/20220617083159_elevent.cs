using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaNangBayBooking.Data.Migrations
{
    public partial class elevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "f6c3369d-9584-42e9-a4b6-e34a37b71f05");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "cb39fa04-92ed-4aaf-ab37-7f31fef0b4d1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1a31c9df-861d-4e53-b076-c3081e1c2666"),
                column: "ConcurrencyStamp",
                value: "88f07c08-3581-4c76-bb4e-d1a90b4629d6");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "ffdf88ed-8367-4670-b69c-3f694cb3cf7b");
        }
    }
}
