using System;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Primitives;

namespace EGM.GQL.DataAccess
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly GraphyDbContextFactory _dbContextFactory;

        public UnitOfWork(GraphyDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }
        
        public async Task SaveAsync(CancellationToken cancellationToken = default)
            => await _dbContextFactory.Context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
            => await _dbContextFactory.Context.Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => await _dbContextFactory.Context.Database.CommitTransactionAsync(cancellationToken);
        
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => await _dbContextFactory.Context.Database.RollbackTransactionAsync(cancellationToken);
    }
}