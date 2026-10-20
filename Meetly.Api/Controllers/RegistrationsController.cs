using Meetly.Api.Data;
using Meetly.Api.Dtos;
using Meetly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/meetups/{meetupId:int}/registrations")]
[Authorize]
public class RegistrationsController(MeetlyDbContext db, UserManager<User> userManager) : ControllerBase
{
    // POST /api/meetups/5/registrations
    // Anmäler den inloggade användaren till meetupen.
    [HttpPost]
    public async Task<IActionResult> Register(int meetupId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var meetup = await db.Meetups
            .AsNoTracking()
            .Where(m => m.Id == meetupId)
            .Select(m => new { m.StartTime, m.Capacity, RegisteredCount = m.Registrations.Count })
            .FirstOrDefaultAsync();

        if (meetup is null)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Meetupen finns inte.");

        if (meetup.StartTime <= DateTime.UtcNow)
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Meetupen har redan startat.",
                detail: "Du kan inte anmäla dig till en meetup som redan har startat.");

        // Kollas före "full" så att den som redan är anmäld får rätt felmeddelande
        var alreadyRegistered = await db.Registrations
            .AnyAsync(r => r.MeetupId == meetupId && r.UserId == user.Id);
        if (alreadyRegistered)
            return Problem(statusCode: StatusCodes.Status409Conflict,
                title: "Du är redan anmäld till den här meetupen.");

        // Kapacitetskontrollen görs i backend, inte bara i frontend
        if (meetup.RegisteredCount >= meetup.Capacity)
            return Problem(statusCode: StatusCodes.Status409Conflict,
                title: "Meetupen är full.",
                detail: "Det finns inga lediga platser kvar.");

        var registration = new Registration { UserId = user.Id, MeetupId = meetupId };
        db.Registrations.Add(registration);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            // Två samtidiga anrop: det unika indexet (UserId, MeetupId) stoppade dubbletten
            return Problem(statusCode: StatusCodes.Status409Conflict,
                title: "Du är redan anmäld till den här meetupen.");
        }

        var registeredCount = meetup.RegisteredCount + 1;
        return StatusCode(StatusCodes.Status201Created, new RegistrationConfirmationDto(
            meetupId,
            registration.RegisteredAt,
            registeredCount,
            meetup.Capacity - registeredCount));
    }

    // DELETE /api/meetups/5/registrations
    // Tar bort den inloggade användarens egen anmälan. Platsen blir ledig direkt,
    // eftersom AvailableSpots räknas om från antalet anmälningar vid varje läsning.
    [HttpDelete]
    public async Task<IActionResult> Unregister(int meetupId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var registration = await db.Registrations
            .FirstOrDefaultAsync(r => r.MeetupId == meetupId && r.UserId == user.Id);

        if (registration is null)
            return Problem(statusCode: StatusCodes.Status404NotFound,
                title: "Du är inte anmäld till den här meetupen.");

        db.Registrations.Remove(registration);
        await db.SaveChangesAsync();

        return NoContent();
    }
}
