


using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Queries.User;

[ApiController]
[Route("api/user")]
public class UserController(IMediator _mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var tokens = await _mediator.Send(command);
        Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        return Ok(ApiResponse<string>.Success(tokens.AccessToken, "Успішний вхід"));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand command)
    {
        var tokens = await _mediator.Send(command);
        Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        return Ok(ApiResponse<string>.Success(tokens.AccessToken));
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        throw new NotImplementedException();
    }


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var result = await _mediator.Send(new GetCurrentUserQuery(userId!));
        return Ok(ApiResponse<UserDTO>.Success(result));
    }
}