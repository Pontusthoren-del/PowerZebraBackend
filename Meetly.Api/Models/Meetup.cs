using System.ComponentModel.DataAnnotations;

namespace Meetly.Api.Models;

public class Meetup
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    // Sparas som svensk lokal tid (inte UTC) – räcker för en app som bara används i Sverige.
    public DateTime StartTime { get; set; }

    [Required, MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    // Värdens namn (US3: "tid, plats, beskrivning och värd")
    [Required, MaxLength(100)]
    public string Host { get; set; } = string.Empty;

    // Max antal deltagare – kollas i backend vid anmälan (US5)
    [Range(1, 10000)]
    public int Capacity { get; set; }

    // Används av filtret i VG-US10
    [Required, MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
