using System;
using EGM.GQL.Abstractions.DependencyInjection;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EGM.GQL.DataAccess
{
    internal sealed class DataAccessInstaller : IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // serviceCollection.AddDbContext<GraphyDbContext>(options =>
            // {
            //     ConfigureDbContext(configuration, options);
            // });
                
            serviceCollection.AddPooledDbContextFactory<GraphyDbContext>(options =>
            {
                ConfigureDbContext(configuration, options);
            });;

            serviceCollection//.AddTransient(typeof(IRepository<>), typeof(Repository<>))
                // .AddTransient<IPersonRepository, PersonRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureDbContext(IConfiguration configuration, DbContextOptionsBuilder options)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            options.UseMySql(connectionString: configuration.GetConnectionString("MySql"),
                    serverVersion: serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}