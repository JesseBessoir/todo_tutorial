using E3Starter.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Services;

public interface IUserService
{
    Task<UserDto?> AuthenticateAsync(string email, string password);
    Task<UserDto?> GetByIdAsync(int id);
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> CreateAsync(NewUserDto dto);
}
