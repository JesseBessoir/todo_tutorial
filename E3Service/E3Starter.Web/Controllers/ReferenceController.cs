using AutoMapper;
using E3Starter.Configuration;
using E3Starter.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ControllerBase = E3Starter.Web.Controllers.Base.ControllerBase;

namespace E3Starter.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReferenceController : ControllerBase
{
    private readonly IReferenceService _referenceService;

    public ReferenceController(IReferenceService referenceService, IOptions<AppSettings> options, IMapper mapper, IUserService userService) : base(options, mapper, userService)
    {
        _referenceService = referenceService;
    }

    [HttpGet("getroles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _referenceService.GetRolesAsync();
        return Ok(roles);
    }
}
