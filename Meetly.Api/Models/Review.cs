using System.ComponentModel.DataAnnotations;

namespace Meetly.Api.Models;

// Betyg 1–5 och en valfri kommentar på en meetup
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
