using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EGM.GQL.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        public IPersonRepository People { get; }

        public IRepository<DbSex> Sexes { get; }
        
        Task SaveAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}