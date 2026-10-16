using System.ComponentModel.DataAnnotations;

namespace Meetly.Api.Models;

public class Meetup
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    // Svensk lokal tid
    public DateTime StartTime { get; set; }

    [Required, MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    // Namnet på den som håller i meetupen
    [Required, MaxLength(100)]
    public string Host { get; set; } = string.Empty;

    // Max antal deltagare
    [Range(1, 10000)]
    public int Capacity { get; set; }

    [Required, MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
