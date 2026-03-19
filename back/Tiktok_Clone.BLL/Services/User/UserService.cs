using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.MapperProfiles.User;
using Tiktok_Clone.DAL.Entities.User;

namespace Tiktok_Clone.BLL.Services.User;

public class UserService : IUserService
{
    private UserManager<UserEntity> _userManager;
    private IMapper _userMapper;
    
    public UserService(UserManager<UserEntity> userManager, IMapper userMapper)
    {
        _userManager = userManager;
        _userMapper = userMapper;
    }


    public async Task<UserDTO> CreateUserAsync(CreateUserDTO dto)
    {
        UserEntity user = _userMapper.Map<UserEntity>(dto);
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        { 
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return _userMapper.Map<UserDTO>(user);
    }
}