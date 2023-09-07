using E3Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Persistence;

public interface IUserRepository : IRepositoryBase
{
    Task<User> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
}
