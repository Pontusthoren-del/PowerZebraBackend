using Meetly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Data;

internal static class SeedData
{
    // Meetly123! (PasswordHasher<User>)
    private const string DemoPasswordHash =
        "AQAAAAIAAYagAAAAELQUVSI41fWgagZyE9pXUUYzvqTPEox9yqsFS6n9svrFyah6L++e8zsbjJJgzyom2g==";

    private static readonly DateTime SeedCreatedAt = new(2026, 10, 16, 8, 0, 0, DateTimeKind.Utc);

    // Identity behöver normaliserade fält och en stämpel för att inloggning ska fungera
    private static User DemoUser(int id, string firstName, string lastName, string email, string stamp) => new()
    {
        Id = id,
        FirstName = firstName,
        LastName = lastName,
        UserName = email,
        NormalizedUserName = email.ToUpperInvariant(),
        Email = email,
        NormalizedEmail = email.ToUpperInvariant(),
        EmailConfirmed = true,
        PasswordHash = DemoPasswordHash,
        SecurityStamp = stamp,
        ConcurrencyStamp = stamp,
        CreatedAt = SeedCreatedAt
    };

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            DemoUser(1, "Demo", "Användare", "demo@meetly.se", "6f1c2b7e-3a4d-4c8e-9b21-1a2b3c4d5e01"),
            DemoUser(2, "Anna", "Svensson", "anna@meetly.se", "6f1c2b7e-3a4d-4c8e-9b21-1a2b3c4d5e02"),
            DemoUser(3, "Johan", "Berg", "johan@meetly.se", "6f1c2b7e-3a4d-4c8e-9b21-1a2b3c4d5e03")
        );

        modelBuilder.Entity<Meetup>().HasData(
            // Passerade
            new Meetup
            {
                Id = 1,
                Title = "React-kväll: hooks på riktigt",
                Description = "Vi går igenom useEffect, useMemo och egna hooks med kodexempel. Ta med laptop!",
                StartTime = new DateTime(2026, 9, 10, 18, 0, 0),
                Location = "Campus Varberg, sal 2",
                Host = "Anna Svensson",
                Capacity = 30,
                Category = "Teknik"
            },
            new Meetup
            {
                Id = 2,
                Title = "Morgonlöpning längs strandpromenaden",
                Description = "Lugnt tempo, ca 6 km från Kallbadhuset till Apelviken och tillbaka. Fika efteråt.",
                StartTime = new DateTime(2026, 9, 20, 8, 0, 0),
                Location = "Kallbadhuset, Varberg",
                Host = "Johan Berg",
                Capacity = 20,
                Category = "Sport"
            },
            new Meetup
            {
                Id = 3,
                Title = "Brädspelskväll",
                Description = "Allt från Catan till snabba kortspel. Nybörjare varmt välkomna.",
                StartTime = new DateTime(2026, 10, 2, 18, 30, 0),
                Location = "Stadsbiblioteket, Varberg",
                Host = "Anna Svensson",
                Capacity = 16,
                Category = "Spel"
            },
            new Meetup
            {
                Id = 4,
                Title = "Svampplockning i Åkulla bokskogar",
                Description = "Guidad tur där vi lär oss känna igen ätliga svampar. Ta med korg och kniv.",
                StartTime = new DateTime(2026, 10, 4, 10, 0, 0),
                Location = "Åkulla bokskogar, parkeringen",
                Host = "Johan Berg",
                Capacity = 12,
                Category = "Friluftsliv"
            },

            // Kommande
            new Meetup
            {
                Id = 5,
                Title = "Intro till ASP.NET Core Web API",
                Description = "Från tomt projekt till ett API med EF Core och Swagger på en kväll.",
                StartTime = new DateTime(2026, 11, 5, 18, 0, 0),
                Location = "Campus Varberg, sal 1",
                Host = "Anna Svensson",
                Capacity = 25,
                Category = "Teknik"
            },
            new Meetup
            {
                Id = 6,
                Title = "Vinprovning för nybörjare",
                Description = "Sex viner från olika regioner och grunderna i hur man provar vin.",
                StartTime = new DateTime(2026, 11, 12, 19, 0, 0),
                Location = "Societetshuset, Varberg",
                Host = "Johan Berg",
                Capacity = 18,
                Category = "Mat & dryck"
            },
            new Meetup
            {
                Id = 7,
                Title = "Fotopromenad på fästningen",
                Description = "Vi fotograferar Varbergs fästning i skymningen. Mobilkamera räcker gott.",
                StartTime = new DateTime(2026, 11, 14, 15, 0, 0),
                Location = "Varbergs fästning, entrén",
                Host = "Anna Svensson",
                Capacity = 15,
                Category = "Kultur"
            },
            new Meetup
            {
                Id = 8,
                Title = "Padel – mixad dubbel",
                Description = "En bana, fyra spelare. Platserna går fort!",
                StartTime = new DateTime(2026, 11, 18, 17, 0, 0),
                Location = "Varbergs Padelcenter",
                Host = "Johan Berg",
                Capacity = 2,
                Category = "Sport"
            },
            new Meetup
            {
                Id = 9,
                Title = "Hackathon: bygg en app på en dag",
                Description = "Bilda lag på plats och bygg något kul. Pizza ingår.",
                StartTime = new DateTime(2026, 11, 21, 9, 0, 0),
                Location = "Kunskapshuset, Göteborg",
                Host = "Anna Svensson",
                Capacity = 40,
                Category = "Teknik"
            },
            new Meetup
            {
                Id = 10,
                Title = "Julbak – lussekatter och pepparkakor",
                Description = "Vi bakar tillsammans och tar med oss hem. Allt material finns på plats.",
                StartTime = new DateTime(2026, 12, 6, 13, 0, 0),
                Location = "Folkets Hus, Varberg",
                Host = "Johan Berg",
                Capacity = 12,
                Category = "Mat & dryck"
            },
            new Meetup
            {
                Id = 11,
                Title = "Vinterbad och bastu",
                Description = "Ett snabbt dopp i havet och sedan bastu. Handduk och badkläder behövs.",
                StartTime = new DateTime(2026, 12, 13, 9, 0, 0),
                Location = "Kallbadhuset, Varberg",
                Host = "Anna Svensson",
                Capacity = 10,
                Category = "Friluftsliv"
            },
            new Meetup
            {
                Id = 12,
                Title = "Retrospelkväll",
                Description = "NES, SNES och N64 kopplade till gamla TV-apparater. Turnering i Mario Kart.",
                StartTime = new DateTime(2026, 12, 17, 18, 0, 0),
                Location = "Halmstad bibliotek",
                Host = "Johan Berg",
                Capacity = 20,
                Category = "Spel"
            }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration { Id = 1, UserId = 1, MeetupId = 1, RegisteredAt = new DateTime(2026, 9, 1, 12, 0, 0, DateTimeKind.Utc) },
            new Registration { Id = 2, UserId = 1, MeetupId = 3, RegisteredAt = new DateTime(2026, 9, 25, 12, 0, 0, DateTimeKind.Utc) },
            new Registration { Id = 3, UserId = 1, MeetupId = 5, RegisteredAt = SeedCreatedAt },
            new Registration { Id = 4, UserId = 1, MeetupId = 7, RegisteredAt = SeedCreatedAt },

            new Registration { Id = 5, UserId = 2, MeetupId = 2, RegisteredAt = new DateTime(2026, 9, 10, 12, 0, 0, DateTimeKind.Utc) },
            new Registration { Id = 6, UserId = 3, MeetupId = 1, RegisteredAt = new DateTime(2026, 9, 2, 12, 0, 0, DateTimeKind.Utc) },
            new Registration { Id = 7, UserId = 2, MeetupId = 3, RegisteredAt = new DateTime(2026, 9, 26, 12, 0, 0, DateTimeKind.Utc) },

            // Meetup 8 blir full
            new Registration { Id = 8, UserId = 2, MeetupId = 8, RegisteredAt = SeedCreatedAt },
            new Registration { Id = 9, UserId = 3, MeetupId = 8, RegisteredAt = SeedCreatedAt }
        );

        modelBuilder.Entity<Review>().HasData(
            new Review { Id = 1, UserId = 3, MeetupId = 1, Rating = 5, Comment = "Superbra genomgång, äntligen förstår jag useEffect!", CreatedAt = new DateTime(2026, 9, 11, 9, 0, 0, DateTimeKind.Utc) },
            new Review { Id = 2, UserId = 2, MeetupId = 2, Rating = 4, Comment = "Härlig runda, lite kallt i blåsten.", CreatedAt = new DateTime(2026, 9, 21, 9, 0, 0, DateTimeKind.Utc) },
            new Review { Id = 3, UserId = 1, MeetupId = 3, Rating = 4, Comment = "Trevligt gäng och bra spel.", CreatedAt = new DateTime(2026, 10, 3, 9, 0, 0, DateTimeKind.Utc) },
            new Review { Id = 4, UserId = 2, MeetupId = 3, Rating = 3, Comment = "Kul, men lite för trångt.", CreatedAt = new DateTime(2026, 10, 3, 10, 0, 0, DateTimeKind.Utc) }
        );
    }
}
