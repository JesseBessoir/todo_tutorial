using AutoMapper;
using E3Starter.Configuration;
using E3Starter.Contracts.Services;
using E3Starter.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ControllerBase = E3Starter.Web.Controllers.Base.ControllerBase;

namespace E3Starter.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    public UsersController(IOptions<AppSettings> options, IMapper mapper, IUserService userService) : base(options, mapper, userService)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] NewUserDto dto)
    {
        var newUser = await _userService.CreateAsync(dto);
        return Ok(newUser);
    }
}
