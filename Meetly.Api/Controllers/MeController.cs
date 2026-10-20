using Meetly.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/me")]
[Authorize]
public class MeController(UserManager<User> userManager) : ControllerBase
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
}
