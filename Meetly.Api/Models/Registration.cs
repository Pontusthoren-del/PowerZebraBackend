namespace Meetly.Api.Models;

// Kopplingstabell User <-> Meetup ("User X är anmäld till Meetup Y").
// Egen entitet i stället för ren many-to-many, så vi kan spara när man anmälde sig.
// Paret (UserId, MeetupId) är unikt – man kan inte anmäla sig två gånger (US5).
public class Registration
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MeetupId { get; set; }
    public Meetup Meetup { get; set; } = null!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
