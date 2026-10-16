namespace Meetly.Api.Models;

// Kopplar ihop en användare med en meetup hen är anmäld till
public class Registration
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MeetupId { get; set; }
    public Meetup Meetup { get; set; } = null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
