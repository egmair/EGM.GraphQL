using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.DataAccess.Abstractions.Entities;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Primitives;
using EGM.GQL.DataAccess.Primitives.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        public TEntity Find(params object[] keyValues) => Table.Find(keyValues);

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken = default, params object[] keyValues)
            => await Table.FindAsync(keyValues, cancellationToken);

        public async Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
            => await BuildQuery(null, orderBy, include, disableTracking).ToListAsync(cancellationToken);

        public async Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
            => await BuildQuery(where, orderBy, include, disableTracking).ToListAsync(cancellationToken);

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
            => await BuildQuery(where, orderBy, include, disableTracking).SingleOrDefaultAsync(cancellationToken);

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
            => await BuildQuery(where, orderBy, include, disableTracking).FirstOrDefaultAsync(cancellationToken);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default)
            => await BuildQuery(where).CountAsync(cancellationToken);

        public async Task<bool> DoesExistAsync(Expression<Func<TEntity, bool>> where = null,
            CancellationToken cancellationToken = default)
            => await BuildQuery(where).AnyAsync(cancellationToken);

        protected virtual IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = Table;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!(include is null))
            {
                query = include(query);
            }

            if (!(where is null))
            {
                query = query.Where(where);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}