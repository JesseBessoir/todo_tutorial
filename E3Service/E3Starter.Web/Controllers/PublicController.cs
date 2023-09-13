using E3Starter.Configuration;
using E3Starter.Contracts.Services;
using E3Starter.Dtos;
using E3Starter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IReferenceService = E3Starter.Contracts.Services.IReferenceService;

namespace E3Starter.Web.Controllers;

[Route("api/[controller]")]
public class PublicController : Controller
{
    private readonly AppSettings _appSettings;
    private readonly IUserService _userService;
    private readonly IReferenceService _referenceService;

    public PublicController(IOptions<AppSettings> options, IUserService userService,IReferenceService referenceService)
    {
        _appSettings = options.Value;
        _userService = userService;
        _referenceService = referenceService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginAttemptDto login)
    {
        if (login.Email is null || login.Password is null) throw new Exception();
        var user = await _userService.AuthenticateAsync(login.Email, login.Password);
        if (user == null) return Unauthorized();
        var token = GenerateJWT(user);
        return Ok(new { Token = token, User = user });
    }

    private string GenerateJWT(UserDto user)
    {
        if (_appSettings.Secret is null) throw new ApplicationException();
        var claims = new List<Claim> {
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        foreach (var r in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, r.Name));
        }
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    [HttpPost("[action]")]
    public async Task<List<CategoriesDto>> GetCategoryList([FromBody] TaskSearchCriteriaDto criteria)
    {
        try
        {
            var data = await _referenceService.GetCategoryList(criteria);
            return data;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "error getting category list");
            return new List<CategoriesDto>();
        }
    }

    [HttpPost("[action]")]
    public async Task<List<TaskDto>> GetTaskList([FromBody] TaskSearchCriteriaDto criteria)
    {
        try
        {
            var data = await _referenceService.GetTaskList(criteria);
            return data;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "error getting task list");
            return new List<TaskDto>();
        }
    }
    
    [HttpPost("[action]")]
    public async Task<StatusCodeResult> SaveTask([FromBody]TaskDto newTask)
    {
        try
        {
            await _referenceService.SaveTask(newTask);
            return Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "error getting task list");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("[action]")]
    public async Task<StatusCodeResult> ToggleCompleted([FromBody]TaskDto completedTask)
    {
        try
        {
            await _referenceService.ToggleCompleted(completedTask);
            return Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "error marking complete");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost("[action]")]
    public async Task<StatusCodeResult> DeactivateTask([FromBody]TaskDto deactivatedTask)
    {
        try
        {
            await _referenceService.DeactivateTask(deactivatedTask);
            return Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "error marking complete");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }



}
