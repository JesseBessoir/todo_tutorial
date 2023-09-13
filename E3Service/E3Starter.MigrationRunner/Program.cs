using E3Starter.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

long _version = 3;
bool _migrateDown = false;

if (args != null && args.Length > 0)
{
    _version = long.Parse(args[0]);
    _migrateDown = false;
}

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json");
var connectionString = builder.Build().GetConnectionString("Connection") ?? throw new ApplicationException("No connection string provided!");

var services = new ServiceCollection();

services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer2016()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(InitialSchema).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

IServiceProvider serviceProvider = services.BuildServiceProvider(false);

using var scope = serviceProvider.CreateScope();

var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

if (_migrateDown)
{
    runner.MigrateDown(_version);
    Console.WriteLine("Press any Key to Close...");
    Console.ReadKey();
}
else
{
    runner.MigrateUp();
}
