// See https://aka.ms/new-console-template for more information

using EGM.GQL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddDbContext<GraphyDbContext>(options =>
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            options.UseMySql(connectionString: configuration.GetConnectionString("MySql"),
                    serverVersion: serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
    })
    .Build();

await host.RunAsync();



Console.WriteLine("Hello, World!");