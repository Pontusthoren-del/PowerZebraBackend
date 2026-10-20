using Meetly.Api.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Data;

public class MeetlyDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IDataProtectionKeyContext
{
    public MeetlyDbContext(DbContextOptions<MeetlyDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Databasen sparar inte tidszon, så vi talar om att alla tider är UTC
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
    }

    public DbSet<Meetup> Meetups => Set<Meetup>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<Review> Reviews => Set<Review>();

    // Nycklar som signerar inloggningstokens. Sparas i databasen så att de
    // överlever omstart/deploy av containern.
    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Samma e-post får inte finnas två gånger
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Meetup>(meetup =>
        {
            // Index för snabbare sortering på datum
            meetup.HasIndex(m => m.StartTime);
            meetup.ToTable(t => t.HasCheckConstraint("CK_Meetup_Capacity", "[Capacity] > 0"));
        });

        modelBuilder.Entity<Registration>(registration =>
        {
            // En användare kan bara anmäla sig en gång per meetup
            registration.HasIndex(r => new { r.UserId, r.MeetupId }).IsUnique();

            registration.HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            registration.HasOne(r => r.Meetup)
                .WithMany(m => m.Registrations)
                .HasForeignKey(r => r.MeetupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>(review =>
        {
            // En användare kan bara recensera en meetup en gång
            review.HasIndex(r => new { r.UserId, r.MeetupId }).IsUnique();
            review.ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "[Rating] BETWEEN 1 AND 5"));

            review.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            review.HasOne(r => r.Meetup)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MeetupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        SeedData.Seed(modelBuilder);
    }
}
