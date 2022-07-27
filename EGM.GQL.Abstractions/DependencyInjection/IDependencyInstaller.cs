using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Abstractions.DependencyInjection
{
    public interface IDependencyInstaller
    {
        void InstallDependencies(IServiceCollection serviceCollection, IConfiguration configuration);

        int Order => -1;
    }
}