using Meetly.Api.Dtos;
using Meetly.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meetly.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<User> userManager) : ControllerBase
{
    // Identitys egen /register tar bara e-post och lösenord, så vi har en egen som även sparar namn.
    // Inloggning sköts fortfarande av Identitys /api/auth/login (med e-post och lösenord).
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(RegisterRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            UserName = request.Email,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem(ModelState);
        }

        return Created("/api/me", new { user.Id, user.FirstName, user.LastName, user.Email });
    }
}
