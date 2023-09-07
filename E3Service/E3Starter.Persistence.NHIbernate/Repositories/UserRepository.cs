using E3Starter.Contracts.Persistence;
using E3Starter.Models;
using E3Starter.Persistence.NHIbernate.Repositories.Base;
using NHibernate;
using NHibernate.Linq;

namespace E3Starter.Persistence.NHIbernate.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(ISession session) : base(session)
    {
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await Session.Query<User>().ToListAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await Session.Query<User>().SingleOrDefaultAsync(u => u.Email == email);
    }
}
