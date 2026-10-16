using Meetly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Data;

public class MeetlyDbContext : DbContext
{
    public MeetlyDbContext(DbContextOptions<MeetlyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Meetup> Meetups => Set<Meetup>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- User ---
        // Unik e-post (US1) – databasen stoppar dubbletter även om koden missar det
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // --- Meetup ---
        modelBuilder.Entity<Meetup>(meetup =>
        {
            // Listan sorteras/filtreras på datum (US3, US6, US10)
            meetup.HasIndex(m => m.StartTime);
            meetup.ToTable(t => t.HasCheckConstraint("CK_Meetup_Capacity", "[Capacity] > 0"));
        });

        // --- Registration (User <-> Meetup) ---
        modelBuilder.Entity<Registration>(registration =>
        {
            // Samma användare kan inte anmäla sig två gånger till samma meetup (US5)
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

        // --- Review (VG-US9) ---
        modelBuilder.Entity<Review>(review =>
        {
            // Max en recension per användare och meetup
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
