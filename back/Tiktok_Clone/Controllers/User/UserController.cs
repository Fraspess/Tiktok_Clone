


using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Queries.User;

[ApiController]
[Route("api/users")]
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
        await _mediator.Send(command);

        return Ok(ApiResponse<object>.Success(null!, "Код для підтвердження реєстрації був надісланий на вказану почту."));
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
    {
        var tokens = await _mediator.Send(command);
        AppendRefreshTokenCookie(tokens.RefreshToken);
        return Ok(ApiResponse<object>.Success(new { accessToken = tokens.AccessToken }, "Пошта підтверджена."));
    }


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.GetUserId();

        var result = await _mediator.Send(new GetCurrentUserQuery(userId));
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
        var userId = User.GetUserId();

        await _mediator.Send(new LogOutOnAllDevicesCommand(userId));

        DeleteRefreshTokenCookie();
        return Ok(ApiResponse<object>.Success(null!, "Успішний вихід з усіх пристроїв"));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPasswordCommand)
    {
        await _mediator.Send(forgotPasswordCommand);
        return Ok(ApiResponse<object>.Success(null!, "Перевірте вашу почту"));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        await _mediator.Send(command);
        return Ok(ApiResponse<object>.Success(null!, "Пароль успішно змінено"));
    }



    private void AppendRefreshTokenCookie(string refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }


    private void DeleteRefreshTokenCookie()
    {
        Response.Cookies.Append("refreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(-1)
        });
    }
}