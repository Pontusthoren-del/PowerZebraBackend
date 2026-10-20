using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Meetly.Api.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeedMeetups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "Capacity", "Category", "Description", "Host", "Location", "StartTime", "Title" },
                values: new object[,]
                {
                    { 13, 20, "Kultur", "Guidad promenad bland stadens skulpturer och gallerier. Avslutas med fika.", "Anna Svensson", "Falkenbergs museum, entrén", new DateTime(2026, 11, 3, 17, 0, 0, 0, DateTimeKind.Utc), "Konstpromenad i Falkenberg" },
                    { 14, 15, "Teknik", "Kort dragning om typade props och hooks, sedan fika och kodsnack.", "Johan Berg", "Kontorshotellet, Falkenberg", new DateTime(2026, 11, 4, 7, 0, 0, 0, DateTimeKind.Utc), "Frontend-fika: React och TypeScript" },
                    { 15, 3, "Sport", "Lugnt pass för alla nivåer. Ta med matta och varma kläder.", "Anna Svensson", "Skrea strand, Falkenberg", new DateTime(2026, 11, 7, 9, 0, 0, 0, DateTimeKind.Utc), "Yoga på stranden" },
                    { 16, 40, "Spel", "Lagquiz om allt från musik till geografi. Lag om max fyra personer.", "Johan Berg", "Harrys, Halmstad", new DateTime(2026, 11, 10, 18, 0, 0, 0, DateTimeKind.Utc), "Quizkväll" },
                    { 17, 25, "Friluftsliv", "Ca 8 km runt naturreservatet med fågelskådning på vägen.", "Anna Svensson", "Getteröns naturreservat, Varberg", new DateTime(2026, 11, 22, 9, 0, 0, 0, DateTimeKind.Utc), "Vandring på Getterön" },
                    { 18, 50, "Kultur", "Livejazz med lokala musiker. Biljett ingår, dryck köps på plats.", "Johan Berg", "Stadsteatern, Göteborg", new DateTime(2026, 11, 27, 18, 0, 0, 0, DateTimeKind.Utc), "Jazzkväll" },
                    { 19, 10, "Mat & dryck", "Vi startar en surdeg och bakar bröd. Du får med dig en burk surdeg hem.", "Anna Svensson", "Folkets Hus, Falkenberg", new DateTime(2026, 11, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Surdegskurs" },
                    { 20, 30, "Sport", "Två varv, ca 5 km, i ditt eget tempo. Samling vid Plikta.", "Johan Berg", "Slottsskogen, Göteborg", new DateTime(2026, 12, 5, 8, 0, 0, 0, DateTimeKind.Utc), "Löpgrupp i Slottsskogen" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "MeetupId", "RegisteredAt", "UserId" },
                values: new object[,]
                {
                    { 10, 15, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 11, 15, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
