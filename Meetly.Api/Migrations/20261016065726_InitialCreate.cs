using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Meetly.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetups", x => x.Id);
                    table.CheckConstraint("CK_Meetup_Capacity", "[Capacity] > 0");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MeetupId = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MeetupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.CheckConstraint("CK_Review_Rating", "[Rating] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_Reviews_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "Capacity", "Category", "Description", "Host", "Location", "StartTime", "Title" },
                values: new object[,]
                {
                    { 1, 30, "Teknik", "Vi går igenom useEffect, useMemo och egna hooks med kodexempel. Ta med laptop!", "Anna Svensson", "Campus Varberg, sal 2", new DateTime(2026, 9, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), "React-kväll: hooks på riktigt" },
                    { 2, 20, "Sport", "Lugnt tempo, ca 6 km från Kallbadhuset till Apelviken och tillbaka. Fika efteråt.", "Johan Berg", "Kallbadhuset, Varberg", new DateTime(2026, 9, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), "Morgonlöpning längs strandpromenaden" },
                    { 3, 16, "Spel", "Allt från Catan till snabba kortspel. Nybörjare varmt välkomna.", "Anna Svensson", "Stadsbiblioteket, Varberg", new DateTime(2026, 10, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), "Brädspelskväll" },
                    { 4, 12, "Friluftsliv", "Guidad tur där vi lär oss känna igen ätliga svampar. Ta med korg och kniv.", "Johan Berg", "Åkulla bokskogar, parkeringen", new DateTime(2026, 10, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "Svampplockning i Åkulla bokskogar" },
                    { 5, 25, "Teknik", "Från tomt projekt till ett API med EF Core och Swagger på en kväll.", "Anna Svensson", "Campus Varberg, sal 1", new DateTime(2026, 11, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Intro till ASP.NET Core Web API" },
                    { 6, 18, "Mat & dryck", "Sex viner från olika regioner och grunderna i hur man provar vin.", "Johan Berg", "Societetshuset, Varberg", new DateTime(2026, 11, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "Vinprovning för nybörjare" },
                    { 7, 15, "Kultur", "Vi fotograferar Varbergs fästning i skymningen. Mobilkamera räcker gott.", "Anna Svensson", "Varbergs fästning, entrén", new DateTime(2026, 11, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), "Fotopromenad på fästningen" },
                    { 8, 2, "Sport", "En bana, fyra spelare. Platserna går fort!", "Johan Berg", "Varbergs Padelcenter", new DateTime(2026, 11, 18, 17, 0, 0, 0, DateTimeKind.Unspecified), "Padel – mixad dubbel" },
                    { 9, 40, "Teknik", "Bilda lag på plats och bygg något kul. Pizza ingår.", "Anna Svensson", "Kunskapshuset, Göteborg", new DateTime(2026, 11, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), "Hackathon: bygg en app på en dag" },
                    { 10, 12, "Mat & dryck", "Vi bakar tillsammans och tar med oss hem. Allt material finns på plats.", "Johan Berg", "Folkets Hus, Varberg", new DateTime(2026, 12, 6, 13, 0, 0, 0, DateTimeKind.Unspecified), "Julbak – lussekatter och pepparkakor" },
                    { 11, 10, "Friluftsliv", "Ett snabbt dopp i havet och sedan bastu. Handduk och badkläder behövs.", "Anna Svensson", "Kallbadhuset, Varberg", new DateTime(2026, 12, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), "Vinterbad och bastu" },
                    { 12, 20, "Spel", "NES, SNES och N64 kopplade till gamla TV-apparater. Turnering i Mario Kart.", "Johan Berg", "Halmstad bibliotek", new DateTime(2026, 12, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), "Retrospelkväll" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), "demo@meetly.se", "Demo Användare", "AQAAAAIAAYagAAAAELQUVSI41fWgagZyE9pXUUYzvqTPEox9yqsFS6n9svrFyah6L++e8zsbjJJgzyom2g==" },
                    { 2, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), "anna@meetly.se", "Anna Svensson", "AQAAAAIAAYagAAAAELQUVSI41fWgagZyE9pXUUYzvqTPEox9yqsFS6n9svrFyah6L++e8zsbjJJgzyom2g==" },
                    { 3, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), "johan@meetly.se", "Johan Berg", "AQAAAAIAAYagAAAAELQUVSI41fWgagZyE9pXUUYzvqTPEox9yqsFS6n9svrFyah6L++e8zsbjJJgzyom2g==" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "MeetupId", "RegisteredAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 3, new DateTime(2026, 9, 25, 12, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, 5, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 7, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, 2, new DateTime(2026, 9, 10, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 6, 1, new DateTime(2026, 9, 2, 12, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 7, 3, new DateTime(2026, 9, 26, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 8, 8, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 9, 8, new DateTime(2026, 10, 16, 8, 0, 0, 0, DateTimeKind.Utc), 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "MeetupId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Superbra genomgång, äntligen förstår jag useEffect!", new DateTime(2026, 9, 11, 9, 0, 0, 0, DateTimeKind.Utc), 1, 5, 3 },
                    { 2, "Härlig runda, lite kallt i blåsten.", new DateTime(2026, 9, 21, 9, 0, 0, 0, DateTimeKind.Utc), 2, 4, 2 },
                    { 3, "Trevligt gäng och bra spel.", new DateTime(2026, 10, 3, 9, 0, 0, 0, DateTimeKind.Utc), 3, 4, 1 },
                    { 4, "Kul, men lite för trångt.", new DateTime(2026, 10, 3, 10, 0, 0, 0, DateTimeKind.Utc), 3, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetups_StartTime",
                table: "Meetups",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_MeetupId",
                table: "Registrations",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId_MeetupId",
                table: "Registrations",
                columns: new[] { "UserId", "MeetupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MeetupId",
                table: "Reviews",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId_MeetupId",
                table: "Reviews",
                columns: new[] { "UserId", "MeetupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Meetups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
