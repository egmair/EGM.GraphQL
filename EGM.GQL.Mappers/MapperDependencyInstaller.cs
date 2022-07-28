using EGM.GQL.Abstractions.DependencyInjection;
using EGM.GQL.Mappers.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Mappers
{
    public sealed class MapperDependencyInstaller : IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAutoMapper(opts =>
            {
                opts.AddProfile<DbProfile>();
            });
        }
    }
}