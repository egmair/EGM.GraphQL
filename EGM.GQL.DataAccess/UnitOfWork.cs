using System;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EGM.GQL.DataAccess
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly GraphyDbContext _context;

        public GraphyDbContext Context => _context;
        
        private IPersonRepository _personRepository;
        public IPersonRepository People => _personRepository ??= new PersonRepository(_context);

        private IRepository<DbSex> _sexRepository;
        public IRepository<DbSex> Sexes => _sexRepository ??= new Repository<DbSex>(_context);

        public UnitOfWork(GraphyDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public async Task SaveAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => await _context.Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => await _context.Database.CommitTransactionAsync(cancellationToken);
        
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => await _context.Database.RollbackTransactionAsync(cancellationToken);
    }
}