using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tiktok_Clone.BLL.Constants;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Services.Email;
using Tiktok_Clone.BLL.Services.ImageService;
using Tiktok_Clone.BLL.Services.Token;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.BLL.Services.User;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _userMapper;
    private readonly IJWTTokenService _jwtTokenService;
    private readonly ILogger<UserService> _logger;
    private readonly IImageService _imageService;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public UserService(UserManager<UserEntity> userManager,
        IMapper userMapper, IJWTTokenService tokenService,
        ILogger<UserService> logger,
        IImageService imageService,
        IConfiguration configuration,
        IEmailService emailService)
    {
        _userManager = userManager;
        _userMapper = userMapper;
        _jwtTokenService = tokenService;
        _logger = logger;
        _imageService = imageService;
        _configuration = configuration;
        _emailService = emailService;
    }


    private string GetHtmlTemplate(string templateName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", templateName);
        return File.ReadAllText(path);
    }

    public async Task<TokenResponseDTO> Login(LoginUserDTO dto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Login || u.Email == dto.Login);
        if (user == null)
        {
            throw new ValidationException("Невірний логін або пароль");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!checkPassword)
        {
            throw new ValidationException("Невірний логін або пароль");
        }
        if (user.EmailConfirmed == false)
        {
            throw new NotAllowedException("Підтвердіть свою електронну пошту, щоб увійти");
        }
        return await _jwtTokenService.GenerateTokensAsync(user);
    }

    public async Task Register(RegisterUserDTO dto)
    {
        var isEmailTaken = await _userManager.FindByEmailAsync(dto.Email);
        if (isEmailTaken is not null)
        {
            throw new ValidationException("Почта вже занята");
        }

        var isUsernameTaken = await _userManager.FindByNameAsync(dto.Username);
        if (isUsernameTaken is not null)
        {
            throw new ValidationException("Юзернейм вже занятий");
        }

        var user = _userMapper.Map<UserEntity>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
            if (dto.Avatar is not null)
            {
                var imageName = await _imageService.SaveImageAsync(dto.Avatar);
                user.Avatar = imageName;
                await _userManager.UpdateAsync(user);
            }
            await _userManager.AddToRoleAsync(user, RoleNames.USER_ROLE);

            // токен є 6 значним числом, настроєно в program.cs 
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var body = GetHtmlTemplate("ConfirmEmail.html");
            body = body.Replace("{confirmCode}", token);

            await _emailService.SendEmailAsync(user.Email!, "Підтвердження реєстрації", body);
        }
        else
        {
            _logger.LogWarning("Failed to create user because : {error} ", string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new ValidationException("Помилка при створенні користувача : " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

    }

    public async Task<UserDTO> GetCurrentUserAsync(Guid userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new UnauthorizedException("Користувач не знайдений");
        }
        return _userMapper.Map<UserDTO>(user);
    }

    public async Task UpdateTokenVersion(Guid userId)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId)
            ?? throw new UnauthorizedException("Користувач не знайдений");

        var currentVersion = user.RefreshTokenVersion;
        user.RefreshTokenVersion = currentVersion + 1;

        await _userManager.UpdateAsync(user);

    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UnauthorizedException("Користувач не знайдений");
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string resetLink = $"{_configuration["Frontend:Url"]}/reset-password?token={token}&email={email}";
        var body = GetHtmlTemplate("ResetPassword.html");
        body = body.Replace("{resetLink}", resetLink);

        await _emailService.SendEmailAsync(email, "Скидання пароля", body);
    }

    public async Task<TokenResponseDTO> ConfirmEmail(string email, string token)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Email == email)
            ?? throw new UnauthorizedException("Користувач не знайдений");

        if (user.EmailConfirmed == true)
        {
            throw new ValidationException("Пошта вже підтверджена");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return await _jwtTokenService.GenerateTokensAsync(user);
        }
        else
        {
            _logger.LogWarning("Не вдалося підтвердити почту {email} того що : {error} ", email, string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new ValidationException("Невірний токен підтвердження");
        }
    }

    // Скидає пароль і міняє версію токен на + 1 щоб інші токени стали недійсними
    public async Task ResetPasswordAsync(ResetPasswordDTO dto)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Email == dto.Email)
            ?? throw new UnauthorizedException("Користувач не знайдений");

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

        if (result.Succeeded)
        {
            await UpdateTokenVersion(user.Id);

        }
        else
        {
            _logger.LogWarning("Не вдалося скинути пароль для {email} того що : {error} ", dto.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new ValidationException("Невірний токен для скидання пароля");
        }
    }
}