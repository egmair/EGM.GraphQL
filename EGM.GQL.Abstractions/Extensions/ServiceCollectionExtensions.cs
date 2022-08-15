using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EGM.GQL.Abstractions.DependencyInjection;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallDependenciesFromAssemblies(this IServiceCollection serviceCollection,
            IConfiguration configuration, params Assembly[] assemblies)
        {
            var filteredList = GetFilteredAssemblies(assemblies, c => c.FullName.Contains("EGM.GQL."));
            foreach (var assembly in filteredList)
            {
                var installers = ExtractTypes<IDependencyInstaller>(assembly);
                ProcessInstallers(installers,
                    installer => installer.InstallDependencies(serviceCollection, configuration));
            }
        }
        
        public static void InstallQueryTypesFromAssemblies(this IRequestExecutorBuilder builder,
            params Assembly[] assemblies)
        {
            var filteredList = GetFilteredAssemblies(assemblies, c => c.FullName.Contains("EGM.GQL."));
            foreach (var assembly in filteredList)
            {
                var installers = ExtractTypes<IQueryInstaller>(assembly);
                ProcessInstallers(installers, installer => installer.InstallTypes(builder));
            }
        }

        private static IEnumerable<Assembly> GetFilteredAssemblies(IEnumerable<Assembly> assemblies,
            Func<Assembly, bool> predicate)
        {
            return assemblies.Where(predicate);
        }
        
        private static IEnumerable<T> ExtractTypes<T>(Assembly assembly)
        {
            var installerTypes = assembly.DefinedTypes
                .Where(t =>
                    typeof(T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

            var installers = installerTypes.Select(Activator.CreateInstance)
                .Cast<T>();
            
            return installers;
        }

        private static void ProcessInstallers<T>(IEnumerable<T> installers, Action<T> action)
        {
            foreach (var installer in installers)
            {
                action.Invoke(installer);
            }
        }
    }
}