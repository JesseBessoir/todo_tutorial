using E3Starter.Contracts.Persistence;
using NHibernate;

namespace E3Starter.Persistence.NHIbernate.Repositories.Base;

public abstract class RepositoryBase : IRepositoryBase
{
    protected ISession Session { get; private set; }

    protected RepositoryBase(ISession session)
    {
        Session = session;
    }

    public async Task SaveAsync<T>(T t)
    {
        await Session.SaveAsync(t);
    }

    public async Task UpdateAsync<T>(T t)
    {
        await Session.UpdateAsync(t);
    }

    public async Task<T> GetAsync<T>(int id)
    {
        return await Session.GetAsync<T>(id);
    }

    public async Task<T> LoadAsync<T>(int id)
    {
        return await Session.LoadAsync<T>(id);
    }

    public async Task<T> CreateAsync<T>(T t)
    {
        await Session.SaveAsync(t);
        return t;
    }

    public async Task DeleteAsync<T>(T t)
    {
        await Session.DeleteAsync(t);
    }
}
