using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meetly.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedTimesUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2026, 9, 10, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2026, 10, 2, 16, 30, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2026, 10, 4, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2026, 11, 5, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2026, 11, 12, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2026, 11, 14, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2026, 11, 18, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2026, 11, 21, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2026, 12, 6, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 11,
                column: "StartTime",
                value: new DateTime(2026, 12, 13, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 12,
                column: "StartTime",
                value: new DateTime(2026, 12, 17, 17, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2026, 9, 10, 18, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2026, 9, 20, 8, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 3,
                column: "StartTime",
                value: new DateTime(2026, 10, 2, 18, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 4,
                column: "StartTime",
                value: new DateTime(2026, 10, 4, 10, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 5,
                column: "StartTime",
                value: new DateTime(2026, 11, 5, 18, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 6,
                column: "StartTime",
                value: new DateTime(2026, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 7,
                column: "StartTime",
                value: new DateTime(2026, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 8,
                column: "StartTime",
                value: new DateTime(2026, 11, 18, 17, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 9,
                column: "StartTime",
                value: new DateTime(2026, 11, 21, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 10,
                column: "StartTime",
                value: new DateTime(2026, 12, 6, 13, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 11,
                column: "StartTime",
                value: new DateTime(2026, 12, 13, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 12,
                column: "StartTime",
                value: new DateTime(2026, 12, 17, 18, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
