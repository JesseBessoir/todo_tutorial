namespace E3Starter.Contracts.Persistence;

public interface IRepositoryBase
{
    Task SaveAsync<T>(T t);
    Task UpdateAsync<T>(T t);
    Task<T> GetAsync<T>(int id);
    Task<T> LoadAsync<T>(int id);
    Task<T> CreateAsync<T>(T t);
    Task DeleteAsync<T>(T t);
}
