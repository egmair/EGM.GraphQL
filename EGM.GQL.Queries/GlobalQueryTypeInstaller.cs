using EGM.GQL.Abstractions.DependencyInjection;
using EGM.GQL.DataAccess;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.Queries.ObjectTypes;
using EGM.GQL.Queries.QueryTypes;
using HotChocolate.Data;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGM.GQL.Queries
{
    internal sealed class GlobalQueryTypeInstaller : IQueryInstaller
    {
        public void InstallTypes(IRequestExecutorBuilder builder)
        {
            builder.AddQueryType<PersonQueryType>()
                .AddType<PersonType>()
                .AddType<SexType>()
                .RegisterDbContext<GraphyDbContext>(DbContextKind.Pooled)
                .RegisterService<IUnitOfWork>();
        }
    }
}