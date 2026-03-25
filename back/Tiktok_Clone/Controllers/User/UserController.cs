


using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Video;

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