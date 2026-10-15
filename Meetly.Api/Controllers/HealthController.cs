using Microsoft.AspNetCore.Mvc;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    // GET /api/health – visar att API:et är uppe (används av frontend och efter deploy)
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "ok", timeUtc = DateTime.UtcNow });
}
