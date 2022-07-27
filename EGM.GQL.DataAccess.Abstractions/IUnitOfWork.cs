using System.Threading;
using System.Threading.Tasks;

namespace EGM.GQL.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}