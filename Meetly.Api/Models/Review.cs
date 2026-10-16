using System.ComponentModel.DataAnnotations;

namespace Meetly.Api.Models;

// VG-US9: betyg + recension. Max en per användare och meetup (unikt index i DbContext).
// Regeln "bara om man var anmäld och meetupen har passerat" kollas i controllern.
public class Review
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MeetupId { get; set; }
    public Meetup Meetup { get; set; } = null!;
}
