using HotChocolate.Execution.Configuration;

namespace EGM.GQL.Abstractions.DependencyInjection
{
    public interface IQueryInstaller
    {
        void InstallTypes(IRequestExecutorBuilder builder);
    }
}