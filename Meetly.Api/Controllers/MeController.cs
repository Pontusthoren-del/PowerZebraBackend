using Meetly.Api.Data;
using Meetly.Api.Dtos;
using Meetly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/me")]
[Authorize]
public class MeController(UserManager<User> userManager,MeetlyDbContext db) : ControllerBase
{
    // Returnerar den inloggade användaren. Frontend använder den för att visa namn i menyn
    // och för att kontrollera att token fortfarande gäller.
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        return Ok(new { user.Id, user.FirstName, user.LastName, user.Email });
    }

    [HttpGet("registrations")]
    public async Task<ActionResult<MyRegistrationsDto>> GetMyRegistrations()
    {
        var userId = int.Parse(userManager.GetUserId(User)!);

        var meetups = await db.Registrations
            .AsNoTracking()
            .Where(r=>r.UserId == userId)
            .Select(r=> new MeetupDto(
               r.Meetup.Id,
                r.Meetup.Title,
                r.Meetup.Description,
                r.Meetup.StartTime,
                r.Meetup.Location,
                r.Meetup.Host,
                r.Meetup.Capacity,
                r.Meetup.Category,
                r.Meetup.Registrations.Count,
                r.Meetup.Capacity - r.Meetup.Registrations.Count))
            .ToListAsync();

        var now = DateTime.UtcNow;
        var upcoming = meetups.Where(m => m.StartTime >= now).OrderBy(m => m.StartTime).ToList();
        var past = meetups.Where(m => m.StartTime < now).OrderByDescending(m => m.StartTime).ToList();

        return Ok(new MyRegistrationsDto(upcoming, past));
    }
}
