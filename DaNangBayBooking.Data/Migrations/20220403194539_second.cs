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
                value: "e386328d-b95f-4b80-8292-711129e77695");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cf290b2b-1145-4b57-9feb-36c43437d683", "AQAAAAEAACcQAAAAEMspfj/fJB83RRggqRxSp7X+QOscX8MI+ZGQSpJ5fYIxXBJmnjPPlrr4n+MFPJbs6g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fbc6c82-5ea2-47c8-bc7c-0d9ed0281045"),
                column: "ConcurrencyStamp",
                value: "648cbc05-6884-4059-b943-2d46a47e8a1b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("06fdb157-c52f-4e71-adf5-0f08bb0af468"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "24eda1d9-a2a4-4bcd-b18f-219e526a3059", "AQAAAAEAACcQAAAAEBwPvWem0rAQtpA+3SBjR2GXcI9SE9PWkHoRFKP/Dl4+yJaNMjSENLbZygJW8bJmjg==" });
        }
    }
}
