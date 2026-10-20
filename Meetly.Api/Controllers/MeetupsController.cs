using Meetly.Api.Data;
using Meetly.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetupsController : ControllerBase
{
    private readonly MeetlyDbContext _db;

    public MeetupsController(MeetlyDbContext db)
    {
        _db = db;
    }

    // GET /api/meetups?search=react
    [HttpGet]
    public async Task<ActionResult<List<MeetupDto>>> GetUpcoming(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? location,
        [FromQuery] DateOnly? from,
        [FromQuery] DateOnly? to)
    {
        if (from > to)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Från-datum kan inte vara efter till-datum");
        }
        var query = _db.Meetups
            .AsNoTracking()
            .Where(m => m.StartTime >= DateTime.UtcNow);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(m =>
                m.Title.Contains(term) ||
                m.Description.Contains(term) ||
                m.Location.Contains(term) ||
                m.Host.Contains(term) ||
                m.Category.Contains(term));
        }
        if (!string.IsNullOrWhiteSpace(category))
        {
            var cat = category.Trim();
            query = query.Where(m => m.Category == cat);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            var place = location.Trim();
            query = query.Where(m => m.Location.Contains(place));
        }

        if (from is not null)
        {
            var fromUtc = SwedishDateToUtc(from.Value);
            query = query.Where(m => m.StartTime >= fromUtc);
        }

        if (to is not null)
        {
            // Till och med hela till-dagen
            var toUtc = SwedishDateToUtc(to.Value.AddDays(1));
            query = query.Where(m => m.StartTime < toUtc);
        }

        var meetups = await query
            .OrderBy(m => m.StartTime)
            .Select(m => new MeetupDto(
                m.Id,
                m.Title,
                m.Description,
                m.StartTime,
                m.Location,
                m.Host,
                m.Capacity,
                m.Category,
                m.Registrations.Count,
                m.Capacity - m.Registrations.Count))
            .ToListAsync();

        return Ok(meetups);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeetupDto>> GetById(int id)
    {
        var meetup = await _db.Meetups
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new MeetupDto(
                m.Id,
                m.Title,
                m.Description,
                m.StartTime,
                m.Location,
                m.Host,
                m.Capacity,
                m.Category,
                m.Registrations.Count,
                m.Capacity - m.Registrations.Count
                ))
            .FirstOrDefaultAsync();

        if (meetup is null)
        {
            return NotFound();
        }

        return Ok(meetup);
    }
    [HttpGet("categories")]
    public async Task<ActionResult<List<string>>> GetCategories()
    {
        var categories = await _db.Meetups
            .Select(m => m.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
        return Ok(categories);
    }

    private static readonly TimeZoneInfo SwedishTime =
        TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm");

    // Datum från frontend är svenska datum, databasen räknar i UTC
    private static DateTime SwedishDateToUtc(DateOnly date) =>
        TimeZoneInfo.ConvertTimeToUtc(date.ToDateTime(TimeOnly.MinValue), SwedishTime);

}