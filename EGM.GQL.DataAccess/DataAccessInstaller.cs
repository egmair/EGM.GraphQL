using System;
using EGM.GQL.Abstractions.DependencyInjection;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Primitives;
using EGM.GQL.DataAccess.Primitives.Entities;
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
            serviceCollection.AddDbContext<GraphyDbContext>(options =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                options.UseMySql(connectionString: configuration.GetConnectionString("MySql"),
                        serverVersion: serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IPersonRepository<DbPerson>, PersonRepository>()
                .AddScoped<Func<GraphyDbContext>>(provider => provider.GetService<GraphyDbContext>)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<GraphyDbContextFactory>();
        }
    }
}