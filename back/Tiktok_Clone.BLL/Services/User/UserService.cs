using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tiktok_Clone.BLL.Constants;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Exceptions;
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

    public UserService(UserManager<UserEntity> userManager, IMapper userMapper, IJWTTokenService tokenService, ILogger<UserService> logger, IImageService imageService)
    {
        _userManager = userManager;
        _userMapper = userMapper;
        _jwtTokenService = tokenService;
        _logger = logger;
        _imageService = imageService;
    }


    public async Task<TokenResponseDTO> Login(LoginUserDTO dto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Login || u.Email == dto.Login);
        if (user == null)
        {
            throw new UnauthorizedException("Невірний логін або пароль");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!checkPassword)
        {
            throw new UnauthorizedException("Невірний логін або пароль");
        }
        return await _jwtTokenService.GenerateTokensAsync(user);
    }

    public async Task<TokenResponseDTO> Register(RegisterUserDTO dto)
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
            return await _jwtTokenService.GenerateTokensAsync(user);
        }
        else
        {
            _logger.LogWarning("Failed to create user because : {error} ", string.Join(", ", result.Errors.Select(e => e.Description)));
            throw new ValidationException("Помилка при створенні користувача : " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

    }

    public async Task<UserDTO> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null)
        {
            throw new UnauthorizedException("Користувач не знайдений");
        }
        return _userMapper.Map<UserDTO>(user);
    }

    public async Task UpdateTokenVersion(string userId)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id.ToString() == userId)
            ?? throw new UnauthorizedException("Користувач не знайдений");

        var currentVersion = user.RefreshTokenVersion;
        user.RefreshTokenVersion = currentVersion + 1;

        await _userManager.UpdateAsync(user);

    }
}