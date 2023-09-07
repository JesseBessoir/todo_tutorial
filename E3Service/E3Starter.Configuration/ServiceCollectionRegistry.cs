using E3Starter.Contracts.Persistence;
using E3Starter.Contracts.Services;
using E3Starter.Persistence.NHIbernate.Repositories;
using E3Starter.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E3Starter.Configuration;

public static class ServiceCollectionRegistry
{
    public static void RegisterScopedServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReferenceService, ReferenceService>();
    }

    public static void RegisterScopedRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReferenceRepository, ReferenceRepository>();
    }
}
