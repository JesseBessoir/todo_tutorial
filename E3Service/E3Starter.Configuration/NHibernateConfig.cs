using E3Starter.Persistence.NHIbernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace E3Starter.Configuration;

public static class NHibernateConfig
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        
        services.AddSingleton(x => Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
                .ShowSql()
                .FormatSql()
                .ConnectionString(p => p.Is(connectionString)
                )
                .AdoNetBatchSize(20)
                .DefaultSchema("dbo"))
            .ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, "300"))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
            .BuildSessionFactory());

        services.AddScoped(x =>
        {
            var sessionFactory = x.GetService<ISessionFactory>();
            if (sessionFactory == null)
            {
                throw new Exception("Could not initialize a session factory before warming up the ORM");
            }
            return sessionFactory.OpenSession();
        });
    }
}
