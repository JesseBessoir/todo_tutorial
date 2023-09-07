using AutoMapper;
using E3Starter.Configuration;
using E3Starter.Contracts.Services;
using E3Starter.Dtos;
using Microsoft.Extensions.Options;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace E3Starter.Web.Controllers.Base
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected readonly IUserService _userService;
        protected readonly IMapper _mapper;
        protected readonly AppSettings _appSettings;

        protected ControllerBase(IOptions<AppSettings> options, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _appSettings = options.Value;
            _userService = userService;
        }

        protected async Task<UserDto?> GetCurrentUserAsync()
        {
            var userId = this.User.FindFirstValue("UserId");
            return userId == null ? null : await _userService.GetByIdAsync(int.Parse(userId));
        }
    }
}
