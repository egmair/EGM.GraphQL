using System;
using System.Linq;
using System.Reflection;
using EGM.GQL.Abstractions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallDependenciesFromAssemblies(this IServiceCollection serviceCollection,
            IConfiguration configuration, params Assembly[] assemblies)
        {
            var filteredList = assemblies.Where(c => c.FullName.Contains("EGM.GQL."));
            foreach (var assembly in filteredList)
            {
                var installerTypes = assembly.DefinedTypes
                    .Where(t =>
                        typeof(IDependencyInstaller).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                var installers = installerTypes.Select(Activator.CreateInstance)
                    .Cast<IDependencyInstaller>();

                foreach (var installer in installers)
                {
                    installer.InstallDependencies(serviceCollection, configuration);
                }
            }
        }
    }
}