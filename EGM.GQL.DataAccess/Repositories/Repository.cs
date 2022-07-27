using System;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Primitives;
using EGM.GQL.DataAccess.Primitives.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace EGM.GQL.DataAccess.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly GraphyDbContextFactory _factory;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> Table => _dbSet ??= _factory.Context.Set<TEntity>();

        public Repository(GraphyDbContextFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public async Task InsertAsync(TEntity entity, string createdBy = "",
            CancellationToken cancellationToken = default)
        {
            if (typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditableEntity)entity).Created = DateTime.UtcNow;
                ((IAuditableEntity)entity).CreatedBy = createdBy;
            }

            await Table.AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity, string updatedBy = "")
        {
            if (typeof(IAuditableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditableEntity)entity).Updated = DateTime.UtcNow;
                ((IAuditableEntity)entity).UpdatedBy = updatedBy;
            }

            Table.Update(entity);
        }

        public void Delete(TEntity entity) => Table.Remove(entity);

        public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            var entity = await Table.FindAsync(entityId, cancellationToken);

            if (entity is null)
            {
                return;
            }

            Table.Remove(entity);
        }
    }
}