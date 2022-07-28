using EGM.GQL.Abstractions.DependencyInjection;
using EGM.GQL.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Services
{
    public sealed class ServicesDependencyInstaller : IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IPersonService, PersonService>();
        }
    }
}