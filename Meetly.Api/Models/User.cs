using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Meetly.Api.Models;

// E-post, användarnamn och lösenordshash kommer från IdentityUser
public class User : IdentityUser<int>
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
