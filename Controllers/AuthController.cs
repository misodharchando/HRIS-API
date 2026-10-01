
using HRISWebAPI.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRISWebAPI.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthController(TokenService t, IConfiguration c) : ControllerBase
{
    [AllowAnonymous, HttpPost("token")]
    public IActionResult Token(LoginRequest r) =>
        r.ClientId == c["ApiClient:ClientId"] && r.ClientSecret == c["ApiClient:ClientSecret"]
            ? Ok(
                new
                {
                    access_token = t.Create(r.ClientId),
                    token_type = "Bearer",
                    expires_in = 3600,
                }
            )
            : Unauthorized();
}

public record LoginRequest(string ClientId, string ClientSecret);
