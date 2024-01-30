using Application.Services;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult SignIn([FromBody] AuthRequest request)
    {
        var response = _authService.SignIn(request);
        return Ok(response);
    }
}
