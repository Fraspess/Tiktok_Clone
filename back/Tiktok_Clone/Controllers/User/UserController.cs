


using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Services.User;
using Tiktok_Clone.DAL.Entities.User;

namespace Tiktok_Clone.Controllers.User;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return Ok(ApiResponse<UserDTO>.Success(user,"Успішно створенно користувача"));
    }
}