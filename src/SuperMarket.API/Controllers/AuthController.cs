using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Application.Interfaces;

namespace SuperMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    ITokenService _tokenService;
    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (IsValidUser(request)) // your authentication logic
        {
            var token = _tokenService.GenerateToken(request.Username, new List<string> { "Admin" });
            return Ok(new { token });
        }

        return Unauthorized();
    }

    private bool IsValidUser(LoginRequest request)
    {
        // Very simple hardcoded validation for now
        return request.Username == "admin" && request.Password == "password";
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
