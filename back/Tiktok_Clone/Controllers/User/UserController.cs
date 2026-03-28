


using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Queries.User;

[ApiController]
[Route("api/user")]
public class UserController(IMediator _mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var tokens = await _mediator.Send(command);

        AppendRefreshTokenCookie(tokens.RefreshToken);
        return Ok(ApiResponse<object>.Success(new { accessToken = tokens.AccessToken }, "Успішний вхід"));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand command)
    {
        var tokens = await _mediator.Send(command);

        AppendRefreshTokenCookie(tokens.RefreshToken);

        return Ok(ApiResponse<object>.Success(new { accessToken = tokens.AccessToken }, "Успішна реєстрація"));
    }


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");

        var result = await _mediator.Send(new GetCurrentUserQuery(userId!));
        return Ok(ApiResponse<UserDTO>.Success(result));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"]
            ?? throw new UnauthorizedException("Refresh token не знайдений");

        var newTokens = await _mediator.Send(new RefreshTokensCommand(refreshToken));

        AppendRefreshTokenCookie(newTokens.RefreshToken);

        return Ok(ApiResponse<object>.Success(new { accessToken = newTokens.AccessToken }, "Токени оновлено"));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"]
            ?? throw new UnauthorizedException("Refresh token не знайдений");

        DeleteRefreshTokenCookie();

        return Ok(ApiResponse<object>.Success(null!, "Успішний вихід"));
    }

    [HttpPost("logout/all")]
    [Authorize]
    public async Task<IActionResult> LogoutAll()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedException("User ID не знайдений");

        await _mediator.Send(new LogOutOnAllDevicesCommand(userId));

        DeleteRefreshTokenCookie();
        return Ok(ApiResponse<object>.Success(null!, "Успішний вихід з усіх пристроїв"));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand forgotPasswordCommand)
    {
        await _mediator.Send(forgotPasswordCommand);
        return Ok(ApiResponse<object>.Success(null!, "Перевірте вашу почту"));
    }



    private void AppendRefreshTokenCookie(string refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }


    private void DeleteRefreshTokenCookie()
    {
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(-1)
        });
    }
}