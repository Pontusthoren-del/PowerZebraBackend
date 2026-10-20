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
    public async Task<ActionResult<List<MeetupDto>>> GetUpcoming([FromQuery] string? search)
    {
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
}
